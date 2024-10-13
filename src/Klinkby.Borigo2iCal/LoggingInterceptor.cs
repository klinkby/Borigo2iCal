using Microsoft.Extensions.Logging;
using RestSharp.Interceptors;

namespace Klinkby.Borigo2iCal;

internal sealed partial class LoggingInterceptor(ILogger logger) : Interceptor
{
    public override ValueTask BeforeHttpRequest(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
    {
        LogRequest(logger, requestMessage.RequestUri?.AbsoluteUri ?? string.Empty);
        return base.BeforeHttpRequest(requestMessage, cancellationToken);
    }

    [LoggerMessage(1, LogLevel.Information, "Fetch {Url}")]
    private static partial void LogRequest(ILogger logger, string url);
}
