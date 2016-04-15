using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DomainModel.Models;

namespace LMS.DomainModel.ApplicationClasses
{
    public class LeaveStatusAC
    {
        public int Id { get; set; }
        public LMS.DomainModel.Models.Condition Condition { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int LeaveRequestId { get; set; }
        public string TeamLeaderId { get; set; }
        public string EmployeeId { get; set; }

    }

    public class LeaveCalculator
    {
        public double CasualLeave { get; set; }
        public double SickLeave { get; set; }
    }
}
