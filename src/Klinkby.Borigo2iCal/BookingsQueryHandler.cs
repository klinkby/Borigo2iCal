using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Interceptors;

namespace Klinkby.Borigo2iCal;

public class BookingsQueryHandler(
    IOptions<ApiClientOptions> options,
    ILogger<BookingsQueryHandler> logger)
    : IQueryHandler<BookingsQuery, BookingsResponse>
{
    private readonly ApiClientOptions _options = options.Value;

    public async Task<BookingsResponse> ExecuteQueryAsync(BookingsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        using var client = new RestClient($"https://{_options.Subdomain}.spaces.heynabo.com");
        var req = new RestRequest("api/members/bookings/items/{id}");
        req.AddUrlSegment("id", query.FacilityId);
        req.AddHeader("Accept", "application/json");
        req.AddHeader("Authorization", "Bearer " + _options.RememberUserToken);
        req.Interceptors = [new LoggingInterceptor(logger)];
        var res = await client.ExecuteAsync<BookingsResponse>(req, cancellationToken).ConfigureAwait(false);
        return res.IsSuccessful
            ? res.Data ?? new BookingsResponse()
            : throw res.ErrorException ?? new InvalidOperationException("Unexpected");
    }
}

public partial class LoggingInterceptor(ILogger logger) : Interceptor
{
    private readonly ILogger _logger = logger;

    public override ValueTask BeforeHttpRequest(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
    {
        LogRequest(requestMessage?.RequestUri?.AbsoluteUri ?? string.Empty);
        return base.BeforeHttpRequest(requestMessage!, cancellationToken);
    }

    [LoggerMessage(1, LogLevel.Information, "Fetch {url}")]
    private partial void LogRequest(string url);
}
