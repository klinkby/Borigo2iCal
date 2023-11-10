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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] //HttpRequest req,
        GetBookingsParameters parameters,
        ILogger log,
        CancellationToken cancellationToken
    )
    {
        log.LogInformation("Request starting");
        BookingsQuery query;
        BookingsQueryHandler handler;
        try
        {
            var rememberUserToken = GetRequiredEnv("REMEMBER_USER_TOKEN");
            var subdomain = GetRequiredEnv("SUBDOMAIN");
            handler = new BookingsQueryHandler(subdomain, rememberUserToken, log);
            if (!int.TryParse(GetRequiredEnv("FACILITY_ID"), out var facilityId))
                throw new InvalidOperationException("FACILITY_ID not a number");
            query = new BookingsQuery(facilityId, parameters.Date);
        }
        catch (InvalidOperationException e)
        {
            log.LogWarning(e, e.Message);
            return Task.FromResult((IActionResult)new BadRequestObjectResult(e.Message));
        }

        return handler.ExecuteQueryAsync(query, cancellationToken)
            .ContinueWith(t => MapQueryResult(log, t), cancellationToken);
    }

    private static string GetRequiredEnv(string environmentVariableName)
    {
        var rememberUserToken = Environment.GetEnvironmentVariable(environmentVariableName)
                                ?? throw new InvalidOperationException($"{environmentVariableName} not set");
        return rememberUserToken;
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
        return new ContentResult
        {
            Content = t.Result.ToVCalendar().ToString(),
            ContentType = "text/calendar",
            StatusCode = StatusCodes.Status200OK
        };
    }
}