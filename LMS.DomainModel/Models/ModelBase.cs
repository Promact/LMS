using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.Models
{
    public abstract class ModelBase
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
