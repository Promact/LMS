using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.ApplicationClasses
{
    public class TeamProjectAC
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public List<EmployeeViewModel> ListOfEmployee { get; set; }
        public int ProjectId { get; set; }
        public EmployeeViewModel TeamLeader { get; set; }
        public bool IsProjectArchived { get; set; }
    }
}
