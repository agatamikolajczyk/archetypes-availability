using System.Data;

namespace Availability;

public class ResourceAvailabilityRepository()//(IDbConnection dbConnection)
{
    public async Task SaveNewAsync(ResourceGroupedAvailability resourceAvailability)
    {
        await SaveNewAsync(resourceAvailability.Availabilities);
    }
    public async Task SaveNewAsync(ResourceAvailability resourceAvailability)
    {
        await SaveNewAsync(new List<ResourceAvailability>() { resourceAvailability });
    }
    
    private Task SaveNewAsync(IList<ResourceAvailability> availabilities)
    {
        return Task.CompletedTask;
    }
}