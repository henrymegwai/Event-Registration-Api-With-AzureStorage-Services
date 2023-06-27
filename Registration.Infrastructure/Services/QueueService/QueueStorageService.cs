using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Registration.Attendance.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Registration.Infrastructure.Services.QueueService
{
    public class QueueStorageService : IQueueStorageService
    {
        private readonly IConfiguration _configuration;
        private string queuename = "attendance";
        public QueueStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessage(string emailAdress, string queueName = "")
        {
            queuename = string.IsNullOrEmpty(queueName) ? queuename : queueName;
            var queueClient = new QueueClient(_configuration["StorageConnectionString"], queueName, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });
            await queueClient.CreateIfNotExistsAsync();
            var emailMessage = new EmailMessage
            {
                EmailAddress = emailAdress,
                TimeStamp = DateTime.UtcNow,
                Message = $"Hello, thank you for registering for the event. More information coming your wait shortly.",
            };
            var message = JsonConvert.SerializeObject(emailMessage);
            await queueClient.SendMessageAsync(message);
        }
    }
}
