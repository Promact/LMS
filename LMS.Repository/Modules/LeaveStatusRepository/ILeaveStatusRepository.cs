using LMS.DomainModel.ApplicationClasses;
using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repository.Modules.LeaveStatusRepository
{
    public interface ILeaveStatusRepository
    {
        IEnumerable<LeaveStatus> GetAllLeavesStatus();
        LeaveStatus GetByLeaveStatusId(int? leaveStatusId);
        string InsertLeaveStatus(LeaveRequestStatusResultAC leaveRequestStatusResult);
        string InsertLeaveStatusByAdmin(LeaveRequestStatusResultAC leaveRequestStatusResult);
        void SickLeaveResultStatus(LeaveStatusAC leaveStatus);
        void CompensationLeaveResultStatus(LeaveStatusAC leaveStatus);
    }
}
