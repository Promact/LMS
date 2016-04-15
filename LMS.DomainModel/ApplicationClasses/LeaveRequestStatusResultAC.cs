using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.ApplicationClasses
{
    public class LeaveRequestStatusResultAC
    {
        public int Id { get; set; }
        public LMS.DomainModel.Models.Condition Condition { get; set; }
        public int LeaveRequestId { get; set; }
        public string TeamLeaderId { get; set; }
        public string  ResultReason { get; set; }
        public double AllowedDays { get; set; }
        public int? Days { get; set; }
    }
}
