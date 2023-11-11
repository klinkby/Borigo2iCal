using Klinkby.Borigo2iCal.Domain;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Klinkby.Borigo2iCal.Function.Startup))]

namespace Klinkby.Borigo2iCal.Function;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddOptions<ApiClientOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("ApiClient").Bind(settings);
            });
        builder.Services.AddTransient<BookingsQueryHandler>();
        builder.Services.AddTransient<IQueryHandler<BookingsQuery, BookingsResponse>, BookingsQueryHandler>();
    }
}