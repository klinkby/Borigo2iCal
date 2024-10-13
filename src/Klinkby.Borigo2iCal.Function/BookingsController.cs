using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Klinkby.Borigo2iCal.Func;

public partial class BookingsController(
    IQueryHandler<BookingsQuery, BookingsResponse> handler,
    ILogger<BookingsController> logger
)
{
    private readonly ILogger<BookingsController> _logger = logger;

    [Function("Bookings")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "bookings/{id}")]
        HttpRequest req,
        int id,
        CancellationToken cancellationToken
    )
    {
        LogRequestStarting(id);
        var res = await handler.ExecuteQueryAsync(
            new BookingsQuery(id),
            cancellationToken).ConfigureAwait(false);
        return MapQueryResult(res);
    }

    private ContentResult MapQueryResult(BookingsResponse t)
    {
        LogReturningBookings(t.Orders.Length);
        return new ContentResult
        {
            ContentType = "text/calendar",
            StatusCode = StatusCodes.Status200OK,
            Content = t
                .ToVCalendar()
                .ToString()
        };
    }

    [LoggerMessage(2, LogLevel.Information, "Returning {Count} bookings")]
    private partial void LogReturningBookings(int count);

    [LoggerMessage(3, LogLevel.Information, "Request starting {Id}")]
    private partial void LogRequestStarting(int id);
}
