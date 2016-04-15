using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.Models
{
    public class LeaveStatus : ModelBase
    {
        //public int Id { get; set; }

        public LMS.DomainModel.Models.Condition Condition { get; set; }
        public DateTime? ApprovedDate { get; set; }



        public int LeaveRequestId { get; set; }
        public virtual LeaveRequest LeaveRequest { get; set; }                        //1 to many with LeaveRequest table


        [Required]
        public string TeamLeaderId { get; set; }
        public virtual ApplicationUser TeamLeader { get; set; }                      //TeamLeader id from table ApplicationUser


        [Required]
        public string EmployeeId { get; set; }
        public virtual ApplicationUser Employee { get; set; }                       //Employee id from table Application user
        public string StatusReason { get; set; }
        public double AllowedDays { get; set; }
    }
}
