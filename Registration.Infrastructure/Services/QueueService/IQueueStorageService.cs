using Registration.Attendance.Application.Models;

namespace Registration.Infrastructure.Services.QueueService
{
    public interface IQueueStorageService
    {
        Task SendMessage(string emailAddress, string queueName = "");
    }
}