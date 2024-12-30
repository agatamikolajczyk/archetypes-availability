using Availability;

namespace AvailabilityApi;

public static class AvailabilityConfiguration
{
    public static IServiceCollection AddAvailability(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAvailabilityFacade, AvailabilityFacade>();
        return serviceCollection;
    }
}