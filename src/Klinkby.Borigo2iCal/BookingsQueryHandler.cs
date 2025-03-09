using System.Diagnostics.CodeAnalysis;
using Klinkby.VCard;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Klinkby.Borigo2iCal;

[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Late bound")]
internal sealed partial class BookingsQueryHandler(
    IOptions<ApiClientOptions> options,
    ILogger<BookingsQueryHandler> logger)
    : IQueryHandler<BookingsQuery, VCalendar>
{
    private readonly ApiClientOptions _options = options.Value;

    [SuppressMessage("Performance", "CA1849:Call async methods when in an async method", Justification = "tasks are awaited")]
    public async Task<VCalendar> ExecuteQueryAsync(BookingsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);
        var (tBookings, tUsers) = (FetchBookingsAsync(query, cancellationToken), FetchBookingsAsync(cancellationToken));
        await Task.WhenAll(tBookings, tUsers).ConfigureAwait(false);
        
        LogReturningBookings(logger, tBookings.Result.Orders.Length);
        return tBookings.Result.ToVCalendar(tUsers.Result);
    }

    private async Task<BookingsResponse> FetchBookingsAsync(BookingsQuery query, CancellationToken cancellationToken)
    {
        var req = new RestRequest("api/members/bookings/items/{id}");
        req.AddUrlSegment("id", query.FacilityId);
        var res = await FetchResourceAsync<BookingsResponse>(req, cancellationToken).ConfigureAwait(false);
        return res.Data ?? new BookingsResponse();
    }
    
    private async Task<UsersResponse> FetchBookingsAsync(CancellationToken cancellationToken)
    {
        var req = new RestRequest("api/admin/users");
        var res = await FetchResourceAsync<UsersResponse>(req, cancellationToken).ConfigureAwait(false);
        return res.Data ?? new UsersResponse();
    }

    private async Task<RestResponse<T>> FetchResourceAsync<T>(RestRequest req, CancellationToken cancellationToken)
    {
        using var client = new RestClient($"https://{_options.Subdomain}.spaces.heynabo.com");
        req.AddHeader("Accept", "application/json");
        req.AddHeader("Authorization", "Bearer " + _options.RememberUserToken);
        req.Interceptors = [new LoggingInterceptor(logger)];
        var res = await client.ExecuteAsync<T>(req, cancellationToken).ConfigureAwait(false);
        if (!res.IsSuccessful)
        {
            throw res.ErrorException ?? new InvalidOperationException("Unexpected");
        }

        return res;
    }

    [LoggerMessage(2, LogLevel.Information, "Returning {Count} bookings")]
    private static partial void LogReturningBookings(ILogger logger, int count);
}
