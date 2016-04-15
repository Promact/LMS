using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test.Models
{
    class EmployeeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfJoining { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Designation Designation { get; set; }

        public string PhoneNumber { get; set; }
    }
}
