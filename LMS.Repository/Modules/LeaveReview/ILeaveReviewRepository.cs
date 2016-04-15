using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DomainModel.Models;
using LMS.DomainModel.ApplicationClasses;

namespace LMS.Repository.Modules.LeaveReview
{
    public interface ILeaveReviewRepository
    {
        IEnumerable<LeaveRequestAC> GetAllRequestsForReview();
        IEnumerable<LeaveRequestAC> GetAllRequestsForReview(string employeeId);
        IEnumerable<LeaveRequestAC> GetAllRequestsPending();
        IEnumerable<LeaveStatusAC> LeaveRequest(LMS.DomainModel.Models.Condition status);
        IEnumerable<LeaveStatusAC> LeaveRequest(LMS.DomainModel.Models.Condition status, string employeeId);
        IEnumerable<LeaveRequestAC> LeaveRequestUnderTeamLeader(string teamLeaderId);
        IEnumerable<LeaveStatusAC> LeaveRequestUnderTeamLeader(string teamLeaderId, LMS.DomainModel.Models.Condition status);
        IEnumerable<LeaveRequestAC> TeamCalendarByProjectName(int? projectId, string teamLeaderId);
        LeaveDetailsAC GetLeaveDetail(string employeeId);
        IEnumerable<LeaveRequestAC> LeaveRequestByType(LMS.DomainModel.Models.Type type, string employeeId);
        IEnumerable<LeaveRequestAC> LeaveRequestUnderTeamLeaderPending(string teamLeaderId);
        IEnumerable<LeaveRequestAC> SickLeaveList();
        IEnumerable<LeaveRequestAC> CompensationLeaveList(string user);
    }
}
