using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.Models
{
    public class LeaveType : ModelBase
    {
        //public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }        //1 to many with LeaveRequest table

    }
}
