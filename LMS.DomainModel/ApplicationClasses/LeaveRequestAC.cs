using LMS.DomainModel.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.ApplicationClasses
{
    public class LeaveRequestAC
    {
        public int Id { get; set; }
       [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

         [JsonConverter(typeof(StringEnumConverter))]
        public LMS.DomainModel.Models.Unit Unit { get; set; }
        public string Reason { get; set; }

         [JsonConverter(typeof(StringEnumConverter))]
        public LMS.DomainModel.Models.Type Type { get; set; }
        public string HolidayName { get; set; }
        public DateTime? CompensationDate { get; set; }
        public Condition? CompensationStatus { get; set; }
        public string EmergencyContact { get; set; }
        public string ContactName { get; set; }
        public string PointOfContact { get; set; }
        public string DoctorName { get; set; }
        public string DoctorContactNumber { get; set; }
        public string DoctorCertificate { get; set; }
        public string EmployeeId { get; set; }
        public int? HolidayTypeId { get; set; }
        public int? Days { get; set; }
        public Condition Condition { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public Designation Designation { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Condition? TeamLeaderStatus { get; set; }
        public string TeamLeaderName { get; set; }
        public List<string> ResultReason { get; set; }
        public List<string> TeamLeader { get; set; }
        public List<Condition> ConditionList { get; set; }
    }
}
