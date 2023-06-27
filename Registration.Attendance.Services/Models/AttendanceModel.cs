using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Attendance.Application.Models
{
    public class AttendanceModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string EmailAddress { get; set; }
        //partition key
        public string Industry { get; set; }
    }
}
