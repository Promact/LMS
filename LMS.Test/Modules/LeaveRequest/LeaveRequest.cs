using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test.Modules.LeaveRequest
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Unit Unit { get; set; }
        public string Reason { get; set; }
        public LMS.DomainModel.Models.Type Type { get; set; }
        public string HolidayName { get; set; }
        public DateTime? CompensationDate { get; set; }
        public string CompensationStatus { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string PointOfContact { get; set; }
        public string DoctorName { get; set; }
        public string Number { get; set; }
        public string Certificate { get; set; }

        [Required]
        public string EmployeeId { get; set; }
        public int? HolidayTypeId { get; set; }

        public Condition Condition { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
