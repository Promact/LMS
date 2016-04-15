using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test.Models
{
    public class ApplicationUser
    {
        public string Name { get; set; }
        public Designation Designation { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }
        public double CasualLeaveAllowed { get; set; }
        public double SickLeaveAllowed { get; set; }
    }
}
