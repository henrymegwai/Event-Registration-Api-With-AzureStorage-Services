using Azure;
using Azure.Data.Tables;

namespace Registration.Domain
{
    public class AttendeeEntity : ITableEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string EmailAddress { get; set; }
        //partition key
        public string Industry { get; set; }
        public string ImageName { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
