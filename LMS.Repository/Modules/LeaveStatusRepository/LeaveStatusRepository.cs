using LMS.DomainModel.DataRepository;
using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using LMS.DomainModel.ApplicationClasses;
using LMS.Util.Email_Templates;

namespace LMS.Repository.Modules.LeaveStatusRepository
{
    public class LeaveStatusRepository : ILeaveStatusRepository
    {
        private readonly IDataRepository<LeaveStatus> _leaveStatusDataRepository;
        private readonly IDataRepository<LeaveRequest> _leaveRequestDataRepository;
        private readonly IDataRepository<ApplicationUser> _employeeDataRepository;

        public LeaveStatusRepository(IDataRepository<LeaveStatus> leaveStatus, IDataRepository<LeaveRequest> leaveRequest, IDataRepository<ApplicationUser> employeeDataRepository)
        {
            _employeeDataRepository = employeeDataRepository;
            _leaveRequestDataRepository = leaveRequest;
            _leaveStatusDataRepository = leaveStatus;
        }

        /// <summary>
        /// To get the list of Status table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LeaveStatus> GetAllLeavesStatus()
        {
            return _leaveStatusDataRepository.List();
        }

        /// <summary>
        /// To get detail of a particular status from status table
        /// </summary>
        /// <param name="leaveStatusId"></param>
        /// <returns></returns>
        public LeaveStatus GetByLeaveStatusId(int? leaveStatusId)
        {
            return _leaveStatusDataRepository.GetById(leaveStatusId);
        }

        /// <summary>
        /// Method to approved ,rejected, pending, escalted the status of a leaveRequest
        /// </summary>
        /// <param name="status"></param>
        /// <param name="leaveRequestId"></param>
        /// <param name="teamLeaderId"></param>
        public string InsertLeaveStatus(LeaveRequestStatusResultAC leaveRequestStatusResult)
        {
            leaveRequestStatusResult.AllowedDays = Convert.ToDouble(leaveRequestStatusResult.Days);
            var leaveStatusList = _leaveStatusDataRepository.FirstOrDefault(x => x.LeaveRequestId == leaveRequestStatusResult.LeaveRequestId && x.TeamLeaderId == leaveRequestStatusResult.TeamLeaderId);
            if (leaveStatusList.Condition == LMS.DomainModel.Models.Condition.Pending || leaveStatusList.Condition == LMS.DomainModel.Models.Condition.Escalated)
            {
                if (leaveRequestStatusResult.Condition == Condition.Escalated)
                {
                    leaveRequestStatusResult.TeamLeaderId = _employeeDataRepository.FirstOrDefault(x => x.Email == "admin@promactinfo.com").Id;
                }
                leaveStatusList.AllowedDays = leaveRequestStatusResult.AllowedDays;
                leaveStatusList.StatusReason = leaveRequestStatusResult.ResultReason;
                leaveStatusList.Condition = leaveRequestStatusResult.Condition;
                leaveStatusList.ApprovedDate = DateTime.Now;
                leaveStatusList.TeamLeaderId = leaveRequestStatusResult.TeamLeaderId;
                _leaveStatusDataRepository.Update(leaveStatusList);
                _leaveStatusDataRepository.Save();
            }

            var leaveStatus = _leaveStatusDataRepository.Any(x => x.LeaveRequestId == leaveRequestStatusResult.LeaveRequestId && x.Condition == Condition.Rejected);
            if (leaveStatus == true)
            {
                var leaveRequestList = _leaveRequestDataRepository.FirstOrDefault(x => x.Id == leaveRequestStatusResult.LeaveRequestId);
                leaveRequestList.Condition = LMS.DomainModel.Models.Condition.Rejected;
                _leaveRequestDataRepository.Update(leaveRequestList);
                _leaveRequestDataRepository.Save();
            }
            var leaveStatusResultList = _leaveStatusDataRepository.Fetch(x => x.LeaveRequestId == leaveRequestStatusResult.LeaveRequestId);
            var requestList = _leaveRequestDataRepository.FirstOrDefault(x => x.Id == leaveRequestStatusResult.LeaveRequestId);
            var leaveStatusCheck = leaveStatusResultList.All(x => x.LeaveRequestId == leaveRequestStatusResult.LeaveRequestId && x.Condition == LMS.DomainModel.Models.Condition.Approved);
            if (leaveStatusCheck == true)
            {

                var statusList = _leaveStatusDataRepository.Fetch(x => x.LeaveRequestId == leaveRequestStatusResult.LeaveRequestId);
                double allowed = (requestList.EndDate.Value.Day - requestList.StartDate.Day) + 1;
                foreach (var item in statusList)
                {
                    if (allowed > item.AllowedDays)
                    {
                        allowed = item.AllowedDays;
                    }
                }
                var leaveRequestList = _leaveRequestDataRepository.FirstOrDefault(x => x.Id == leaveRequestStatusResult.LeaveRequestId);
                leaveRequestList.Condition = LMS.DomainModel.Models.Condition.Approved;
                var dd = leaveRequestList.StartDate.Day + allowed;
                leaveRequestList.EndDate = leaveRequestList.StartDate.AddDays(allowed);
                _leaveRequestDataRepository.Update(leaveRequestList);
                _leaveRequestDataRepository.Save();
            }
            var employee = _employeeDataRepository.GetById(requestList.EmployeeId);
            ApproveAndReject test = new ApproveAndReject();
            test.Session = new Dictionary<string, object> { 
            {"Name", employee.Name},
            {"Designation", employee.Designation.ToString()},
            {"SiteUrl", employee.Name},
            {"SiteLogo", employee.Designation.ToString()},
            {"ContactNumber", requestList.Number},
            {"Type", requestList.Type.ToString()},
            {"Unit", requestList.Unit.ToString()},
            {"StartDate", requestList.StartDate.ToString("dd,MM,yyyy")},
            {"EndDate", requestList.EndDate.Value.ToString("dd,MM,yyyy")},
            {"CreatedOn", requestList.CreatedOn.ToString("dd,MM,yyyy")},
            {"Status", leaveRequestStatusResult.Condition.ToString()},
            };
            test.Initialize();
            var body = test.TransformText();
            return body;
        }

        /// <summary>
        /// Leave to be approved by admin
        /// </summary>
        /// <param name="leaveRequestStatusResult"></param>
        public string InsertLeaveStatusByAdmin(LeaveRequestStatusResultAC leaveRequestStatusResult)
        {
            var leaveStatusList = _leaveStatusDataRepository.Fetch(x => x.LeaveRequestId == leaveRequestStatusResult.LeaveRequestId).ToList();
            foreach (var leave in leaveStatusList)
            {
                if (leave.Condition == LMS.DomainModel.Models.Condition.Pending || leave.Condition == LMS.DomainModel.Models.Condition.Escalated)
                {
                    leave.AllowedDays = leaveRequestStatusResult.AllowedDays;
                    leave.StatusReason = leaveRequestStatusResult.ResultReason;
                    leave.Condition = leaveRequestStatusResult.Condition;
                    leave.TeamLeaderId = leaveRequestStatusResult.TeamLeaderId;
                    leave.ApprovedDate = DateTime.Now;
                    _leaveStatusDataRepository.Update(leave);
                    _leaveStatusDataRepository.Save();
                }
            }
            var leaveRequest = _leaveRequestDataRepository.FirstOrDefault(x => x.Id == leaveRequestStatusResult.LeaveRequestId);
            var statusList = _leaveStatusDataRepository.Fetch(x => x.LeaveRequestId == leaveRequestStatusResult.LeaveRequestId);
            double allowed = (leaveRequest.EndDate.Value.Day - leaveRequest.StartDate.Day) + 1;
            foreach (var item in statusList)
            {
                if (allowed > item.AllowedDays)
                {
                    allowed = item.AllowedDays;
                }
            }
            if (leaveRequest.Condition == LMS.DomainModel.Models.Condition.Pending || leaveRequest.Condition != LMS.DomainModel.Models.Condition.Rejected) ;
            {
                var dd = leaveRequest.EndDate.Value.Day + allowed;
                leaveRequest.EndDate = leaveRequest.EndDate.Value.AddDays(dd);
                leaveRequest.Condition = leaveRequestStatusResult.Condition;
                _leaveRequestDataRepository.Update(leaveRequest);
                _leaveRequestDataRepository.Save();
            }
            var employee = _employeeDataRepository.GetById(leaveRequest.EmployeeId);
            ApproveAndReject test = new ApproveAndReject();
            test.Session = new Dictionary<string, object> { 
            {"Name", employee.Name},
            {"Designation", employee.Designation.ToString()},
            {"SiteUrl", employee.Name},
            {"SiteLogo", employee.Designation.ToString()},
            {"ContactNumber", leaveRequest.ContactNumber},
            {"Type", leaveRequest.Type.ToString()},
            {"Unit", leaveRequest.Unit.ToString()},
            {"StartDate", leaveRequest.StartDate.ToString("dd,MM,yyyy")},
            {"EndDate", leaveRequest.EndDate.Value.ToString("dd,MM,yyyy")},
            {"CreatedOn", leaveRequest.CreatedOn.ToString("dd,MM,yyyy")},
            {"Status", leaveRequestStatusResult.Condition.ToString()},
            {"Status", leaveRequestStatusResult.ResultReason},
            };
            test.Initialize();
            var body = test.TransformText();
            return body;
        }

        /// <summary>
        /// Sick Leave Approval and rejection by admin
        /// </summary>
        /// <param name="leaveStatus"></param>
        public void SickLeaveResultStatus(LeaveStatusAC leaveStatus)
        {
            var leave = _leaveRequestDataRepository.FirstOrDefault(x => x.Id == leaveStatus.LeaveRequestId && x.Type == DomainModel.Models.Type.Sick);
            if (leaveStatus.Condition == Condition.Approved)
            {
                leave.Condition = leaveStatus.Condition;
            }
            if (leave.Condition == Condition.Pending)
            {
                if (leaveStatus.Condition == Condition.Rejected)
                {
                    leave.Condition = Condition.HalfRejected;
                }
            }
            else
            {
                if (leaveStatus.Condition == Condition.Rejected)
                {
                    leave.Condition = Condition.Approved;
                    leave.Type = DomainModel.Models.Type.Casual;
                }
            }
            _leaveRequestDataRepository.Update(leave);
            _leaveRequestDataRepository.Save();
        }

        /// <summary>
        /// Sick Leave Approval and rejection by admin
        /// </summary>
        /// <param name="leaveStatus"></param>
        public void CompensationLeaveResultStatus(LeaveStatusAC leaveStatus)
        {
            var leave = _leaveRequestDataRepository.FirstOrDefault(x => x.Id == leaveStatus.LeaveRequestId && x.Type == DomainModel.Models.Type.Compensation);
            leave.CompensationStatus = leaveStatus.Condition;
            if (leaveStatus.Condition == Condition.Rejected)
            {
                leave.Type = DomainModel.Models.Type.Casual;
            }
            _leaveRequestDataRepository.Update(leave);
            _leaveRequestDataRepository.Save();
        }
    }
}
