using Klinkby.Borigo2iCal.Domain;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Klinkby.Borigo2iCal;

public class BookingsQueryHandler : IQueryHandler<BookingsQuery, BookingsResponse>
{
    private readonly ILogger _log;
    private readonly string _rememberUserToken;
    private readonly string _subdomain;

    public BookingsQueryHandler(string subdomain, string rememberUserToken, ILogger log)
    {
        _subdomain = subdomain;
        _rememberUserToken = rememberUserToken;
        _log = log;
    }

    public Task<BookingsResponse> ExecuteQueryAsync(BookingsQuery query, CancellationToken cancellationToken)
    {
        var client = new RestClient($"https://{_subdomain}.borigo.com");
        var req = new RestRequest("booking-engine/bookings.json");
        req.AddHeader("cookie", $"remember_user_token={_rememberUserToken}");
        req.AddParameter("facility_id", query.FacilityId, ParameterType.QueryString);
        req.AddParameter("date", query.Date.ToString("yyyy-MM-dd"), ParameterType.QueryString);
        req.AddParameter("offset", query.Offset, ParameterType.QueryString);
        req.OnBeforeRequest += request =>
        {
            _log.LogInformation("Request {url}", request.RequestUri!.AbsoluteUri);
            return ValueTask.CompletedTask;
        };
        return client.ExecuteAsync<BookingsResponse>(req, cancellationToken)
            .ContinueWith(x =>
            {
                _log.LogInformation("Status {statusCode}", x.Result.StatusCode);
                return x.Result.ResponseStatus == ResponseStatus.Error
                    ? x.IsFaulted ? throw x.Exception! : throw x.Result.ErrorException!
                    : x.Result.Data!;
            }, cancellationToken);
    }
}