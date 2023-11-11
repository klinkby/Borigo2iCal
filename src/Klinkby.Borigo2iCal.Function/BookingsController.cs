using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Klinkby.Borigo2iCal.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Klinkby.Borigo2iCal.Function;

public class BookingsController
{
    private readonly IQueryHandler<BookingsQuery, BookingsResponse> _handler;
    private readonly ILogger<BookingsController> _log;
    private readonly ApiClientOptions _options;

    public BookingsController(
        IQueryHandler<BookingsQuery, BookingsResponse> handler,
        IOptions<ApiClientOptions> options,
        ILogger<BookingsController> log)
    {
        _handler = handler;
        _log = log;
        _options = options.Value;
    }

    [FunctionName("Bookings")]
    public Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] 
        GetBookingsParameters parameters,
        CancellationToken cancellationToken
    )
    {
        _log.LogInformation("Request starting");
        return _handler.ExecuteQueryAsync(
                new BookingsQuery(_options.FacilityId, parameters.Date), 
                cancellationToken)
            .ContinueWith(MapQueryResult);
    }

    private IActionResult MapQueryResult(Task<BookingsResponse> t)
    {
        if (t.IsFaulted)
        {
            var e = t.Exception!;
            _log.LogError(e, e.Message);
            return new InternalServerErrorResult();
        }

        _log.LogInformation("Returning bookings");
        return new ContentResult
        {
            ContentType = "text/calendar",
            StatusCode = StatusCodes.Status200OK,
            Content = t.Result
                       .ToVCalendar()
                       .ToString()
        };
    }
}