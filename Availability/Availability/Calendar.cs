namespace Availability;

public record Calendar(ResourceId ResourceId, IDictionary<Owner, IList<TimeSlot>> CalendarEntries)
{
    public static Calendar Empty(ResourceId resourceId)
    {
        return new Calendar(resourceId, new Dictionary<Owner, IList<TimeSlot>>());
    }
    
    public static Calendar WithAvailableSlots(ResourceId resourceId, params TimeSlot[] availableSlots)
    {
        return new Calendar(resourceId,
            new Dictionary<Owner, IList<TimeSlot>> { { Owner.None(), new List<TimeSlot>(availableSlots) } });
    }
}