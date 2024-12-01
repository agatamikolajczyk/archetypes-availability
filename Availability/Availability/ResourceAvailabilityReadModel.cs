namespace Availability;

public class ResourceAvailabilityReadModel
{
    public Task<Calendar> LoadAsync(ResourceId resourceId, TimeSlot timeSlot)
    {
        return Task.FromResult( Calendar.Empty(resourceId));
    }
    
    
}