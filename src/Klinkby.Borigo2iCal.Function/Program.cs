using Klinkby.Borigo2iCal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddOptionsWithValidateOnStart<ApiClientOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("ApiClient").Bind(settings);
            }).ValidateDataAnnotations();
        services.AddTransient<BookingsQueryHandler>();
        services.AddTransient<IQueryHandler<BookingsQuery, BookingsResponse>, BookingsQueryHandler>();
    })
    .Build();

await host.RunAsync().ConfigureAwait(false);
