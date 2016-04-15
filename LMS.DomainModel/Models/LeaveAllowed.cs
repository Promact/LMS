using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.Models
{
    public class LeaveAllowed : ModelBase
    {
        public Type LeaveType { get; set; }
        [Required]
        public double AllowedDays { get; set; }
    }
}
