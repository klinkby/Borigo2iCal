using Klinkby.Borigo2iCal;
using Klinkby.VCard;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        return services.AddTransient<IQueryHandler<BookingsQuery, VCalendar>, BookingsQueryHandler>();
    }
}
