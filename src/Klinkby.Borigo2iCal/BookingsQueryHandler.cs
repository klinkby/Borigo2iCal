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

    public async Task<VCalendar> ExecuteQueryAsync(BookingsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        using var client = new RestClient($"https://{_options.Subdomain}.spaces.heynabo.com");
        var req = new RestRequest("api/members/bookings/items/{id}");
        req.AddUrlSegment("id", query.FacilityId);
        req.AddHeader("Accept", "application/json");
        req.AddHeader("Authorization", "Bearer " + _options.RememberUserToken);
        req.Interceptors = [new LoggingInterceptor(logger)];
        var res = await client.ExecuteAsync<BookingsResponse>(req, cancellationToken).ConfigureAwait(false);
        if (!res.IsSuccessful)
        {
            throw res.ErrorException ?? new InvalidOperationException("Unexpected");
        }

        LogReturningBookings(logger, res.Data?.Orders.Length ?? 0);
        return res.IsSuccessful
            ? (res.Data ?? new BookingsResponse()).ToVCalendar()
            : throw res.ErrorException ?? new InvalidOperationException("Unexpected");
    }

    [LoggerMessage(2, LogLevel.Information, "Returning {Count} bookings")]
    private static partial void LogReturningBookings(ILogger logger, int count);
}
