using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Attendance.Application.Models
{
    public class EmailMessage
    {
        public string EmailAddress { get; set; }         
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
    }
}
