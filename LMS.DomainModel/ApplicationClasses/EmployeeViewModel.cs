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
   public class EmployeeViewModel
    {
            public string Id { get; set; }
            public string Name { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public Designation Designation { get; set; }
            public string PhoneNumber { get; set; }
            [DataType(DataType.Date)]
            public DateTime DateOfJoining { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }
            public bool IsSoftDeleted { get; set; }
        
    }
}
