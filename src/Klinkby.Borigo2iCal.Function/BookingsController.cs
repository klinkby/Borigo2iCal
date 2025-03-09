using System.Diagnostics.CodeAnalysis;
using Klinkby.VCard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Klinkby.Borigo2iCal.Func;

[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Late bound")]
internal sealed partial class BookingsController(
    IQueryHandler<BookingsQuery, VCalendar> handler,
    ILogger<BookingsController> logger)
{
    [Function("Bookings")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "bookings/{id}")]
        HttpRequest req,
        int id,
        CancellationToken cancellationToken
    )
    {
        LogRequestStarting(logger, id);
        var res = await handler.ExecuteQueryAsync(
            new BookingsQuery(id),
            cancellationToken).ConfigureAwait(false);
        return Map(res);
    }

    private static ContentResult Map(VCalendar t)
    {
        return new ContentResult
        {
            ContentType = "text/calendar", StatusCode = StatusCodes.Status200OK, Content = t.ToString()
        };
    }

    [LoggerMessage(3, LogLevel.Information, "Request starting {Id}")]
    private static partial void LogRequestStarting(ILogger logger, int id);
}
