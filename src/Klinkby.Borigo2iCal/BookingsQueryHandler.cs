using Klinkby.Borigo2iCal.Domain;
using RestSharp;

namespace Klinkby.Borigo2iCal;

public class BookingsQueryHandler : IQueryHandler<BookingsQuery, BookingsResponse>
{
    private readonly string _subdomain;
    private readonly string _rememberUserToken;

    public BookingsQueryHandler(string subdomain, string rememberUserToken)
    {
        _subdomain = subdomain;
        _rememberUserToken = rememberUserToken;
    }

    public Task<BookingsResponse> ExecuteQueryAsync(BookingsQuery query, CancellationToken cancellationToken)
    {
        var client = new RestClient($"https://{_subdomain}.borigo.com");
        var req = new RestRequest("booking-engine/bookings.json", Method.Get);
        req.AddHeader("cookie", $"remember_user_token={_rememberUserToken}");
        req.AddUrlSegment("facility_id", query.FacilityId);
        req.AddUrlSegment("date", query.Date.ToString("yyyy-MM-dd"));
        req.AddUrlSegment("offset", query.Offset);
        return client.ExecuteAsync<BookingsResponse>(req, cancellationToken)
            .ContinueWith(x => x.Result.Data!, cancellationToken);
    }
}