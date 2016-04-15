using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test.Models
{
    public class HolidayType
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
