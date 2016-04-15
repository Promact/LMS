using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.Models
{
    public class Project : ModelBase
    {
        //public int Id { get; set; } 

        public string ProjectName { get; set; }
        public bool IsProjectArchived { get; set; }
    }
}
