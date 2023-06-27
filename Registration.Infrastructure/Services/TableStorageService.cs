using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Registration.Domain;

namespace Registration.Infrastructure.Services
{
    public class TableStorageService : ITableStorageService
    {
        private const string TableName = "Attendees";
        private readonly IConfiguration _configuration;

        public TableStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<AttendeeEntity> GetAttendee(string industry, string id)
        {
            var tableClient = await GetTableClient();
            //requires partitionkey and rowkey
            return await tableClient.GetEntityAsync<AttendeeEntity>(industry, id);
        }
        public async Task<List<AttendeeEntity>> GetAttendees()
        {
            var tableClient = await GetTableClient();
            Pageable<AttendeeEntity> attendeeEntities = tableClient.Query<AttendeeEntity>();
            return attendeeEntities.ToList();
        }
        public async Task UpsertAttendee(AttendeeEntity attendeeEntity)
        {
            var tableClient = await GetTableClient();
            // the upsert is responsible for updating and also creating the Attendance entity if it does not exist in the table
            attendeeEntity.PartitionKey = attendeeEntity.Industry;
            await tableClient.UpsertEntityAsync(attendeeEntity);
        }

        public async Task DeleteAttendee(string industry, string id)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(industry, id);
        }

        private async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_configuration["StorageConnectionString"]);

            var tableClient = serviceClient.GetTableClient(TableName);

            await tableClient.CreateIfNotExistsAsync();

            return tableClient;

        }

    }
}
