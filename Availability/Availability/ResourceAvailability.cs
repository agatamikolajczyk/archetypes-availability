namespace Availability;

public class ResourceAvailability
{
    public ResourceAvailabilityId Id { get; }
    public ResourceId ResourceId { get; }
    public ResourceId ResourceParentId { get; }
    public TimeSlot Segment { get; }
    public Blockade Blockade { get; private set; }
    public int Version { get; private set; }
    
    public ResourceAvailability(
        ResourceAvailabilityId availabilityId, 
        ResourceId resourceId,
        TimeSlot segment)
    {
        Id = availabilityId;
        ResourceId = resourceId;
        ResourceParentId = ResourceId.None();
        Segment = segment;
        Blockade = Blockade.None();
    }
}