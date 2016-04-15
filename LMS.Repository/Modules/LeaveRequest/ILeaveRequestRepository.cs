using LMS.DomainModel.ApplicationClasses;
using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repository.Modules
{
    public interface ILeaveRequestRepository
    {
        string ApplyLeaveRequest(LeaveRequestAC leaveRequest);

        void ApplySickLeave(LeaveRequest sickLeaveRequest);
        void UpdateSickLeaveRequest(LeaveRequest sickLeaveUpdateRequest);

        LeaveRequestAC GetLeaveRequestById(int? id);

        bool DeleteLeaveRequest(int? id, string employeeId);

        void Save();


    }
}
