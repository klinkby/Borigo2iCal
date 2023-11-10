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

namespace Klinkby.Borigo2iCal.Function;

public static class Bookings
{
    [FunctionName("Bookings")]
    public static Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get")]
        HttpRequest req,
        ILogger log,
        [FromQuery] DateTimeOffset? date = default,
        CancellationToken cancellationToken = default
    )
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        BookingsQuery query;
        BookingsQueryHandler handler;
        try
        {
            var rememberUserToken = Environment.GetEnvironmentVariable("REMEMBER_USER_TOKEN")
                                    ?? throw new InvalidOperationException("REMEMBER_USER_TOKEN not set");
            var subdomain = Environment.GetEnvironmentVariable("SUBDOMAIN")
                            ?? throw new InvalidOperationException("SUBDOMAIN not set");
            handler = new BookingsQueryHandler(subdomain, rememberUserToken);
            if (!int.TryParse(Environment.GetEnvironmentVariable("FACILITY_ID") ?? null, out var facilityId))
                throw new InvalidOperationException("FACILITY_ID not set");
            date ??= DateTimeOffset.Now;
            query = new BookingsQuery(facilityId, date.Value);
        }
        catch (InvalidOperationException e)
        {
            log.LogWarning(e, e.Message);
            return Task.FromResult((IActionResult)new BadRequestObjectResult(e.Message));
        }

        return handler.ExecuteQueryAsync(query, cancellationToken)
            .ContinueWith(t => MapQueryResult(log, t), cancellationToken);
    }

    private static IActionResult MapQueryResult(ILogger log, Task<BookingsResponse> t)
    {
        if (t.IsFaulted)
        {
            var e = t.Exception!;
            log.LogError(e, e.Message);
            return new InternalServerErrorResult();
        }

        log.LogInformation("Returning bookings");
        return new OkObjectResult(t.Result);
    }
}