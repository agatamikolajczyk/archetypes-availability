using Availability;

namespace AvailabilityTests;

public class AvailabilityTests
{
  
    private readonly IAvailabilityFacade _availabilityFacade;
 
    public AvailabilityTests()
    {
        _availabilityFacade=new AvailabilityFacade(new ResourceAvailabilityRepository())
    }
    
    [Fact]
    public async Task CanCreateAvailabilitySlots()
    {
        //given
        var resourceId = ResourceId.NewOne();
        var oneDay = TimeSlot.CreateDailyTimeSlotAtUtc(2021, 1, 1);

        //when
        await _availabilityFacade.CreateResourceSlots(resourceId, oneDay);

        //then
        var entireMonth = TimeSlot.CreateMonthlyTimeSlotAtUtc(2021, 1);
        var monthlyCalendar = await _availabilityFacade.LoadCalendar(resourceId, entireMonth);
        Assert.Equal(Calendar.WithAvailableSlots(resourceId, oneDay), monthlyCalendar);
    }
    
}