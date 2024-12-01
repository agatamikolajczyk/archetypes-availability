namespace Availability;


public interface IAvailabilityFacade
{
    Task CreateResourceSlots(ResourceId resourceId, TimeSlot timeslot);

    Task CreateResourceSlots(ResourceId resourceId, ResourceId parentId,
        TimeSlot timeslot);

    Task<bool> Block(ResourceId resourceId, TimeSlot timeSlot, Owner requester);
    Task<bool> Release(ResourceId resourceId, TimeSlot timeSlot, Owner requester);
    Task<bool> Disable(ResourceId resourceId, TimeSlot timeSlot, Owner requester);
    Task<ResourceId?> BlockRandomAvailable(ISet<ResourceId> resourceIds, TimeSlot within, Owner owner);
    //Task<ResourceGroupedAvailability> FindGrouped(ResourceId resourceId, TimeSlot within);
    Task<Calendar> LoadCalendar(ResourceId resourceId, TimeSlot within);
    /*Task<Calendars> LoadCalendars(ISet<ResourceId> resources, TimeSlot within);
    Task<ResourceGroupedAvailability> Find(ResourceId resourceId, TimeSlot within);
    Task<ResourceGroupedAvailability> FindByParentId(ResourceId parentId, TimeSlot within);*/
}

public class AvailabilityFacade : IAvailabilityFacade
{
 
    private readonly ResourceAvailabilityRepository _availabilityRepository;
    private readonly ResourceAvailabilityReadModel _availabilityReadModel;

    public AvailabilityFacade(ResourceAvailabilityRepository availabilityRepository, ResourceAvailabilityReadModel availabilityReadModel)
    {
        _availabilityRepository = availabilityRepository;
        _availabilityReadModel = availabilityReadModel;
    }

    public async Task CreateResourceSlots(ResourceId resourceId, TimeSlot timeslot)
    {
        var groupedAvailability = ResourceGroupedAvailability.Of(resourceId, timeslot);
        await _availabilityRepository.SaveNewAsync(groupedAvailability);
    }

    public Task CreateResourceSlots(ResourceId resourceId, ResourceId parentId, TimeSlot timeslot)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Block(ResourceId resourceId, TimeSlot timeSlot, Owner requester)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Release(ResourceId resourceId, TimeSlot timeSlot, Owner requester)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Disable(ResourceId resourceId, TimeSlot timeSlot, Owner requester)
    {
        throw new NotImplementedException();
    }

    public Task<ResourceId?> BlockRandomAvailable(ISet<ResourceId> resourceIds, TimeSlot within, Owner owner)
    {
        throw new NotImplementedException();
    }

    public Task<Calendar> LoadCalendar(ResourceId resourceId, TimeSlot within)
    {
        var normalized = Segments.NormalizeToSegmentBoundaries(within, SegmentInDays.DefaultSegment());
        return _availabilityReadModel.LoadAsync(resourceId, normalized);
    }
}