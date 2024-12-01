namespace Availability;

public static class SlotToNormalizedSlot
{
    public static TimeSlot Apply(TimeSlot timeSlot, SegmentInDays segmentInDays)
    {
        var segmentInMinutesDuration = segmentInDays.Value;
        var segmentStart = NormalizeStart(timeSlot.From, segmentInMinutesDuration);
        var segmentEnd = NormalizeEnd(timeSlot.To, segmentInMinutesDuration);
        var normalized = new TimeSlot(segmentStart, segmentEnd);
        var minimalSegment = new TimeSlot(segmentStart, segmentStart.AddMinutes(segmentInDays.Value));

        if (normalized.Within(minimalSegment))
        {
            return minimalSegment;
        }

        return normalized;
    }

    private static DateTime NormalizeEnd(DateTime initialEnd, int segmentInMinutesDuration)
    {
        var closestSegmentEnd = new DateTime(initialEnd.Year, initialEnd.Month, initialEnd.Day, initialEnd.Hour, 0, 0,
            initialEnd.Kind);

        while (initialEnd > closestSegmentEnd)
        {
            closestSegmentEnd = closestSegmentEnd.AddMinutes(segmentInMinutesDuration);
        }

        return closestSegmentEnd;
    }

    private static DateTime NormalizeStart(DateTime initialStart, int segmentInMinutesDuration)
    {
        var closestSegmentStart = new DateTime(initialStart.Year, initialStart.Month, initialStart.Day,
            initialStart.Hour, 0, 0, initialStart.Kind);

        if (closestSegmentStart.AddMinutes(segmentInMinutesDuration) > initialStart)
        {
            return closestSegmentStart;
        }

        while (closestSegmentStart < initialStart)
        {
            closestSegmentStart = closestSegmentStart.AddMinutes(segmentInMinutesDuration);
        }

        return closestSegmentStart;
    }
}