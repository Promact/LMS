using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test.Models
{
    public class LeaveAllowed
    {
        public int Id { get; set; }
        public LMS.DomainModel.Models.Type LeaveType { get; set; }
        [Required]
        public double AllowedDays { get; set; }
    }
}
