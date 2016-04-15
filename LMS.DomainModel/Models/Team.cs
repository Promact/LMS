using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.Models
{
    public class Team : ModelBase
    {
        //public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual ApplicationUser Employee { get; set; }


        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }


        [Required]
        public string TeamLeaderId { get; set; }
        [ForeignKey("TeamLeaderId")]
        public virtual ApplicationUser TeamLeader { get; set; }


    }
}
