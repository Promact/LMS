using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test.Modules.LeaveRequest
{
    public class LeaveStatus
    {
        public Condition Condition { get; set; }
        public int LeaveRequestId { get; set; }
        public string TeamLeaderId { get; set; }
    }
}
