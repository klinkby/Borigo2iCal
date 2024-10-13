using System.Text.Json;
using Klinkby.Borigo2iCal;
using Klinkby.Borigo2iCal.Func;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((builder, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.Configure<ApiClientOptions>(builder.Configuration.GetSection("ApiClient"));
        services.Configure<JsonSerializerOptions>(options =>
            options.TypeInfoResolver = new FunctionSerializerContext());
        services.AddHandlers();
    })
    .Build();

await host.RunAsync().ConfigureAwait(false);
