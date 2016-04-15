using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.Models
{
    public class LeaveRequest : ModelBase
    {
        [DisplayFormat(DataFormatString = "{0:MM dd, yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public LMS.DomainModel.Models.Unit Unit { get; set; }
        public string Reason { get; set; }
        public LMS.DomainModel.Models.Type Type { get; set; }
        public string HolidayName { get; set; }
         [DataType(DataType.Date)]
        public DateTime? CompensationDate { get; set; }
        public Condition? CompensationStatus { get; set; }
         [Column("EmergencyContactName")]
        public string ContactName { get; set; }
         [Column("EmergencyContactNumber")]
        public string ContactNumber { get; set; }
        public string PointOfContact { get; set; }
        [Column("DoctorName")]
        public string DoctorName { get; set; }
         [Column("DoctorContactNumber")]
        public string Number { get; set; }
         [Column("DoctorCertificate")]
        public string Certificate { get; set; }


        [Required]
        public string EmployeeId { get; set; }
        public virtual ApplicationUser Employee { get; set; }                               //1 to many with Employee table

        public virtual ICollection<LeaveStatus> LeaveStatus { get; set; }             //many to 1 with LeaveStatus table

        public int? HolidayTypeId { get; set; }
        public virtual HolidayType HolidayType { get; set; }                         //1 to many with HolidayType table


        public LMS.DomainModel.Models.Condition Condition { get; set; }

    }
}
