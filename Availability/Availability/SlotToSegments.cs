namespace Availability;

public class SlotToSegments
{
    public static IList<TimeSlot> Apply(TimeSlot timeSlot, SegmentInDays duration)
    {
        var minimalSegment = new TimeSlot(timeSlot.From, timeSlot.From.AddHours(duration.Value));

        if (timeSlot.Within(minimalSegment))
        {
            return new List<TimeSlot> { minimalSegment };
        }

        var segmentInHoursDuration = duration.Value;
        var numberOfSegments = CalculateNumberOfSegments(timeSlot, segmentInHoursDuration);

        var segments = new List<TimeSlot>();

        for (long i = 0; i < numberOfSegments; i++)
        {
            var currentStart = timeSlot.From.AddMinutes(i * segmentInHoursDuration);
            segments.Add(new TimeSlot(currentStart,
                CalculateEnd(segmentInHoursDuration, currentStart, timeSlot.To)));
        }

        return segments;
    }
    
    private static long CalculateNumberOfSegments(TimeSlot timeSlot, int segmentInHoursDuration)
    {
        return (long)Math.Ceiling((timeSlot.To - timeSlot.From).TotalHours / segmentInHoursDuration);
    }
    
    private static DateTime CalculateEnd(int segmentInHoursDuration, DateTime currentStart, DateTime initialEnd)
    {
        var segmentEnd = currentStart.AddHours(segmentInHoursDuration);

        if (initialEnd < segmentEnd)
        {
            return initialEnd;
        }

        return segmentEnd;
    }
}