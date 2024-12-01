namespace Availability;

public class Segments
{
    public const int DefaultSegmentDurationInDays = 1;

    public static IList<TimeSlot> Split(TimeSlot timeSlot, SegmentInDays unit)
    {
        var normalizedSlot = NormalizeToSegmentBoundaries(timeSlot, unit);
        return SlotToSegments.Apply(normalizedSlot, unit);
    }

    public static TimeSlot NormalizeToSegmentBoundaries(TimeSlot timeSlot, SegmentInDays unit)
    {
        return SlotToNormalizedSlot.Apply(timeSlot, unit);
    }
}