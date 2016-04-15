using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.ApplicationClasses
{
    public class TeamCalendarAC
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
    }
}
