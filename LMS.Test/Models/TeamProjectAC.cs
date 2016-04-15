using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test.Models
{
    public class TeamProjectAC
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public List<string> ListOfEmployeeId { get; set; }
        public int ProjectId { get; set; }
        public string TeamLeaderId { get; set; }
    }
}
