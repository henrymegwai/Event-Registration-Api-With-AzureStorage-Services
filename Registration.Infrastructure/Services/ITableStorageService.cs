using Registration.Domain;

namespace Registration.Infrastructure.Services
{
    public interface ITableStorageService
    {
        Task DeleteAttendee(string industry, string id);
        Task<AttendeeEntity> GetAttendee(string id, string industry);
        Task<List<AttendeeEntity>> GetAttendees();
        Task UpsertAttendee(AttendeeEntity attendeeEntity);
    }
}