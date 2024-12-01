namespace Availability;

public record SegmentInDays(int Value)
{
    public static SegmentInDays Of(int days, int slotDurationInDays)
    {
        if (days <= 0)
        {
            throw new ArgumentException("SegmentInDaysDuration must be positive");
        }

        if (days < slotDurationInDays)
        {
            throw new ArgumentException($"SegmentInDaysDuration must be at least {slotDurationInDays} days");
        }

        if (days % slotDurationInDays != 0)
        {
            throw new ArgumentException($"SegmentInDaysDuration must be a multiple of {slotDurationInDays} days");
        }

        return new SegmentInDays(days);
    }
    public static SegmentInDays Of(int days)
    {
        return Of(days, Segments.DefaultSegmentDurationInDays);
    }
    
    public static SegmentInDays DefaultSegment()
    {
        return Of(Segments.DefaultSegmentDurationInDays);
    }
};