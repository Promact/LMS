using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.Models
{
    public class HolidayType : ModelBase
    {
        //public int Id { get; set; }

        public DateTime Date { get; set; }
        [Required]
        public string Name { get; set; }

        public TypeOfHoliday TypeOfHoliday { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }                  //1 to many with LeaveRequest

    }
}
