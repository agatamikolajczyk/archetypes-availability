namespace Availability;

public class ResourceGroupedAvailability(IList<ResourceAvailability> resourceAvailabilities)
{
    public IList<ResourceAvailability> Availabilities { get; } = resourceAvailabilities;

    public static ResourceGroupedAvailability Of(ResourceId resourceId, TimeSlot timeslot)
    {
        var resourceAvailabilities = Segments
            .Split(timeslot, SegmentInDays.DefaultSegment())
            .Select(segment => new ResourceAvailability(ResourceAvailabilityId.NewOne(), resourceId, segment))
            .ToList();
        return new ResourceGroupedAvailability(resourceAvailabilities);
    }
    

}