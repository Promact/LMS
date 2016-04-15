using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repository.Modules.LeaveDefinition
{
    public interface ILeaveAllowedRepository
    {
        IEnumerable<LeaveAllowed> GetAllLeaveTypes();
        LeaveAllowed GetLeaveTypeById(int id);
        void Add(LeaveAllowed leave);
        void Update(LeaveAllowed leave);
    }
}
