using System.Text.Json;
using Klinkby.Borigo2iCal;
using Klinkby.Borigo2iCal.Func;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((builder, services) =>
    {
        services.Configure<JsonSerializerOptions>(options => options.TypeInfoResolver = new FunctionSerializerContext());
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.Configure<ApiClientOptions>(builder.Configuration.GetSection("ApiClient"));
        services.AddTransient<BookingsQueryHandler>();
        services.AddTransient<IQueryHandler<BookingsQuery, BookingsResponse>, BookingsQueryHandler>();
    })
    .Build();

await host.RunAsync().ConfigureAwait(false);
