using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DomainModel.DataRepository;
using LMS.Repository.Modules;
using LMS.Repository.Modules.LeaveReview;
using LMS.DomainModel.Models;
using LMS.DomainModel.ApplicationClasses;

namespace LMS.Repository.Modules.LeaveReview
{
    public class LeaveReviewRepository : ILeaveReviewRepository
    {
        private readonly IDataRepository<LeaveRequest> _leaveRequestDataRepository;
        private readonly IDataRepository<LeaveStatus> _leaveStatusDataRepository;
        private readonly IDataRepository<Team> _teamDataRepository;
        private readonly IDataRepository<ApplicationUser> _employeeDataRepository;

        public LeaveReviewRepository(IDataRepository<LeaveRequest> leaveRequestDataRepository, IDataRepository<LeaveStatus> leaveStatusDataRepository, IDataRepository<Team> teamDataRepository, IDataRepository<ApplicationUser> employeeDataRepository)
        {
            _teamDataRepository = teamDataRepository;
            _leaveRequestDataRepository = leaveRequestDataRepository;
            _leaveStatusDataRepository = leaveStatusDataRepository;
            _employeeDataRepository = employeeDataRepository;
        }

        /// <summary>
        /// Repository method to get leaverequest list from database using leaverequest table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LeaveRequestAC> GetAllRequestsForReview()
        {
            var leaveRequestList = _leaveRequestDataRepository.Fetch(x => x.Condition == DomainModel.Models.Condition.Approved || x.Condition == DomainModel.Models.Condition.Rejected);
            var leaveRequestListAC = new List<LeaveRequestAC>();

            foreach (var leaves in leaveRequestList)
            {
                var leaveStatus = _leaveStatusDataRepository.Fetch(x => x.LeaveRequestId == leaves.Id);
                var employee = _employeeDataRepository.GetById(leaves.EmployeeId);
                var leaveAC = new LeaveRequestAC
                {
                    Id = leaves.Id,
                    StartDate = leaves.StartDate,
                    EndDate = leaves.EndDate,
                    Unit = leaves.Unit,
                    Reason = leaves.Reason,
                    Type = leaves.Type,
                    HolidayName = leaves.HolidayName,
                    CompensationDate = leaves.CompensationDate,
                    CompensationStatus = leaves.CompensationStatus,
                    ContactName = leaves.ContactName,
                    EmergencyContact = leaves.ContactNumber,
                    PointOfContact = leaves.PointOfContact,
                    DoctorName = leaves.DoctorName,
                    DoctorContactNumber = leaves.Number,
                    DoctorCertificate = leaves.Certificate,
                    HolidayTypeId = leaves.HolidayTypeId,
                    EmployeeId = leaves.EmployeeId,
                    Condition = ((DomainModel.Models.Condition)leaves.Condition),
                    Days = Convert.ToInt32(leaves.EndDate.Value.Subtract(leaves.StartDate).TotalDays)+1,
                    //Days = (leaves.EndDate.Value.Day - leaves.StartDate.Day) + 1,
                    EmployeeName = employee.Name,
                };
                List<Condition> conditionList = new List<Condition>();
                List<string> teamLeaderList = new List<string>();
                List<string> requestReasonList = new List<string>();
                foreach(var leave in leaveStatus)
                {
                    List<string> list = new List<string>();
                    var employees = _employeeDataRepository.FirstOrDefault(x => x.Id == leave.TeamLeaderId);
                    teamLeaderList.Add(employees.Name);
                    requestReasonList.Add(leave.StatusReason);
                    conditionList.Add(leave.Condition);
                }
                leaveAC.TeamLeader = teamLeaderList;
                leaveAC.ResultReason = requestReasonList;
                leaveAC.ConditionList = conditionList;
                leaveRequestListAC.Add(leaveAC);
            }
            return leaveRequestListAC;
        }

        public IEnumerable<LeaveRequestAC> GetAllRequestsPending()
        {
            var leaveRequestList = _leaveRequestDataRepository.Fetch(x => x.Condition == DomainModel.Models.Condition.Pending && x.Type != LMS.DomainModel.Models.Type.Sick || x.Condition == Condition.Escalated);
            var leaveRequestListAC = new List<LeaveRequestAC>();

            foreach (var leaves in leaveRequestList)
            {
                var leaveStatus = _leaveStatusDataRepository.Fetch(x => x.LeaveRequestId == leaves.Id);
                var employee = _employeeDataRepository.GetById(leaves.EmployeeId);
                var leaveAC = new LeaveRequestAC
                {
                    Id = leaves.Id,
                    StartDate = leaves.StartDate,
                    EndDate = leaves.EndDate,
                    Unit = leaves.Unit,
                    Reason = leaves.Reason,
                    Type = leaves.Type,
                    HolidayName = leaves.HolidayName,
                    CompensationDate = leaves.CompensationDate,
                    CompensationStatus = leaves.CompensationStatus,
                    ContactName = leaves.ContactName,
                    EmergencyContact = leaves.ContactNumber,
                    PointOfContact = leaves.PointOfContact,
                    DoctorName = leaves.DoctorName,
                    DoctorContactNumber = leaves.Number,
                    DoctorCertificate = leaves.Certificate,
                    HolidayTypeId = leaves.HolidayTypeId,
                    EmployeeId = leaves.EmployeeId,
                    Condition = ((DomainModel.Models.Condition)leaves.Condition),
                    //Days = (leaves.EndDate.Value.Day - leaves.StartDate.Day),
                    Days = Convert.ToInt32(leaves.EndDate.Value.Subtract(leaves.StartDate).TotalDays)+1,
                    EmployeeName = employee.Name,
                };
                List<Condition> conditionList = new List<Condition>();
                List<string> teamLeaderList = new List<string>();
                List<string> requestReasonList = new List<string>();
                foreach (var leave in leaveStatus)
                {
                    List<string> list = new List<string>();
                    var employees = _employeeDataRepository.FirstOrDefault(x => x.Id == leave.TeamLeaderId);
                    teamLeaderList.Add(employees.Name);
                    requestReasonList.Add(leave.StatusReason);
                    conditionList.Add(leave.Condition);
                }
                leaveAC.TeamLeader = teamLeaderList;
                leaveAC.ResultReason = requestReasonList;
                leaveAC.ConditionList = conditionList;
                leaveRequestListAC.Add(leaveAC);
            }
            return leaveRequestListAC;
        }


        /// <summary>
        /// Repository method to get leaverequest list depend upon employeeId from database using leaverequest table
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IEnumerable<LeaveRequestAC> GetAllRequestsForReview(string employeeId)
        {
            var leaveRequestListAC = new List<LeaveRequestAC>();
            var leaveRequestList = _leaveRequestDataRepository.Fetch(x => x.EmployeeId == employeeId);
            foreach (var leaves in leaveRequestList)
            {
                var leaveAC = new LeaveRequestAC
                {
                    Id = leaves.Id,
                    StartDate = leaves.StartDate,
                    EndDate = leaves.EndDate,
                    Unit = leaves.Unit,
                    Reason = leaves.Reason,
                    Type = leaves.Type,
                    HolidayName = leaves.HolidayName,
                    CompensationDate = leaves.CompensationDate,
                    CompensationStatus = leaves.CompensationStatus,
                    ContactName = leaves.ContactName,
                    EmergencyContact = leaves.ContactNumber,
                    PointOfContact = leaves.PointOfContact,
                    DoctorName = leaves.DoctorName,
                    DoctorContactNumber = leaves.Number,
                    DoctorCertificate = leaves.Certificate,
                    HolidayTypeId = leaves.HolidayTypeId,
                    EmployeeId = leaves.EmployeeId
                };

                leaveRequestListAC.Add(leaveAC);
            }
            return leaveRequestListAC;
        }

        /// <summary>
        /// Repository method to get leaverequest list depend upon employeeId and status of request from database using leavestatus table
        /// </summary>
        /// <param name="status"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IEnumerable<LeaveStatusAC> LeaveRequest(LMS.DomainModel.Models.Condition status, string employeeId)
        {
            var leaveStatusListAC = new List<LeaveStatusAC>();
            var leaveStatusList = _leaveStatusDataRepository.Fetch(x => x.Condition.Equals(status) && x.EmployeeId == employeeId);
            foreach (var leaves in leaveStatusList)
            {
                var leaveAC = new LeaveStatusAC
                {
                    Id = leaves.Id,
                    Condition = leaves.Condition,
                    ApprovedDate = leaves.ApprovedDate,
                    LeaveRequestId = leaves.LeaveRequestId,
                    TeamLeaderId = leaves.TeamLeaderId,
                    EmployeeId = leaves.EmployeeId,
                };
                leaveStatusListAC.Add(leaveAC);
            }
            return leaveStatusListAC;
        }

        /// <summary>
        /// Repository method to get leaverequest list depend upon status from database using leavestatus table
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public IEnumerable<LeaveStatusAC> LeaveRequest(LMS.DomainModel.Models.Condition status)
        {
            var leaveStatusListAC = new List<LeaveStatusAC>();
            var leaveStatusList = _leaveStatusDataRepository.Fetch(x => x.Condition.Equals(status));

            foreach (var leaves in leaveStatusList)
            {
                var leaveAC = new LeaveStatusAC
                {
                    Id = leaves.Id,
                    Condition = leaves.Condition,
                    ApprovedDate = leaves.ApprovedDate,
                    LeaveRequestId = leaves.LeaveRequestId,
                    TeamLeaderId = leaves.TeamLeaderId,
                    EmployeeId = leaves.EmployeeId,
                };
                leaveStatusListAC.Add(leaveAC);
            }
            return leaveStatusListAC;
        }

        /// <summary>
        /// Repository method to get leaverequest list depend upon teamLeaderId and status Approved and rejected
        /// </summary>
        /// <param name="teamLeaderId"></param>
        /// <returns></returns>
        public IEnumerable<LeaveRequestAC> LeaveRequestUnderTeamLeader(string teamLeaderId)
        {
            var team = _teamDataRepository.Fetch(x => x.TeamLeaderId == teamLeaderId);
            var leaveStatusListAC = new List<LeaveRequestAC>();
            List<string> employeeList = new List<string>();
            foreach (var item in team)
            {
                var leaveStatusList = _leaveRequestDataRepository.Fetch(x => x.EmployeeId == item.EmployeeId && x.Condition != Condition.Pending && x.Condition != Condition.Escalated);
                //var leaveStatusList = _leaveStatusDataRepository.Fetch(x => x.EmployeeId == item.EmployeeId && x.Condition != Condition.Pending && x.Condition != Condition.Escalated);
                foreach (var leave in leaveStatusList)
                {
                    var employee = _employeeDataRepository.GetById(leave.EmployeeId);
                    //var leave = _leaveRequestDataRepository.GetById(leaves.LeaveRequestId);
                    var leaveAC = new LeaveRequestAC
                    {
                        Id = leave.Id,
                        StartDate = leave.StartDate,
                        EndDate = leave.EndDate,
                        Unit = leave.Unit,
                        Reason = leave.Reason,
                        Type = leave.Type,
                        HolidayName = leave.HolidayName,
                        CompensationDate = leave.CompensationDate,
                        CompensationStatus = leave.CompensationStatus,
                        ContactName = leave.ContactName,
                        EmergencyContact = leave.ContactNumber,
                        PointOfContact = leave.PointOfContact,
                        DoctorName = leave.DoctorName,
                        DoctorContactNumber = leave.Number,
                        DoctorCertificate = leave.Certificate,
                        HolidayTypeId = leave.HolidayTypeId,
                        EmployeeId = leave.EmployeeId,
                        EmployeeName = employee.Name,
                        Condition = leave.Condition,
                        Designation = employee.Designation,
                        Email = employee.Email,
                        //Days = (leave.EndDate.Value.Day - leave.StartDate.Day) + 1
                        Days = Convert.ToInt32(leave.EndDate.Value.Subtract(leave.StartDate).TotalDays)+1
                    };
                    leaveStatusListAC.Add(leaveAC);
                }
            }
            return leaveStatusListAC;
        }

        /// <summary>
        /// Repository method to get leaverequest list depend upon teamLeaderId and status Approved and rejected
        /// </summary>
        /// <param name="teamLeaderId"></param>
        /// <returns></returns>
        public IEnumerable<LeaveRequestAC> LeaveRequestUnderTeamLeaderPending(string teamLeaderId)
        {
            var team = _teamDataRepository.Fetch(x => x.TeamLeaderId == teamLeaderId);
            var leaveStatusListAC = new List<LeaveRequestAC>();
            List<string> employeeList = new List<string>();
            foreach (var item in team)
            {
                var leaveRequestList = _leaveRequestDataRepository.Fetch(x => x.EmployeeId == item.EmployeeId && x.Condition == Condition.Pending && x.Type != DomainModel.Models.Type.Sick);
                foreach (var leave in leaveRequestList)
                {
                    var leaveStatus = _leaveStatusDataRepository.FirstOrDefault(x => x.EmployeeId == leave.EmployeeId && x.LeaveRequestId == leave.Id && x.TeamLeaderId == item.TeamLeaderId);
                    var employee = _employeeDataRepository.GetById(leave.EmployeeId);
                    var leaveAC = new LeaveRequestAC
                    {
                        Id = leave.Id,
                        StartDate = leave.StartDate,
                        EndDate = leave.EndDate,
                        Unit = leave.Unit,
                        Reason = leave.Reason,
                        Type = leave.Type,
                        HolidayName = leave.HolidayName,
                        CompensationDate = leave.CompensationDate,
                        CompensationStatus = leave.CompensationStatus,
                        ContactName = leave.ContactName,
                        EmergencyContact = leave.ContactNumber,
                        PointOfContact = leave.PointOfContact,
                        DoctorName = leave.DoctorName,
                        DoctorContactNumber = leave.Number,
                        DoctorCertificate = leave.Certificate,
                        HolidayTypeId = leave.HolidayTypeId,
                        EmployeeId = leave.EmployeeId,
                        EmployeeName = employee.Name,
                        Condition = leave.Condition,
                        Designation = employee.Designation,
                        Email = employee.Email,
                        //Days = (leave.EndDate.Value.Day - leave.StartDate.Day) + 1,
                        Days = Convert.ToInt32(leave.EndDate.Value.Subtract(leave.StartDate).TotalDays)+1,
                        TeamLeaderStatus = leaveStatus.Condition
                    };
                    if (item.CreatedOn > leave.CreatedOn)
                    {
                        leaveAC.TeamLeaderStatus = Condition.Escalated;
                    }
                    //if (leaveStatus.Condition!=Condition.Pending)
                    //{
                    //    leaveAC.TeamLeaderStatus = Condition.Escalated;
                    //}
                    leaveStatusListAC.Add(leaveAC);
                }
            }
            return leaveStatusListAC;
        }

        /// <summary>
        /// Repository method to get leaverequest list depend upon status and teamLeaderId from database using leavestatus table
        /// </summary>
        /// <param name="teamLeaderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IEnumerable<LeaveStatusAC> LeaveRequestUnderTeamLeader(string teamLeaderId, LMS.DomainModel.Models.Condition status)
        {
            var leaveStatusList = _leaveStatusDataRepository.Fetch(x => x.TeamLeaderId == teamLeaderId && x.Condition == status);
            var leaveStatusListAC = new List<LeaveStatusAC>();

            foreach (var leaves in leaveStatusList)
            {
                var leaveAC = new LeaveStatusAC
            {
                Id = leaves.Id,
                Condition = leaves.Condition,
                ApprovedDate = leaves.ApprovedDate,
                LeaveRequestId = leaves.LeaveRequestId,
                TeamLeaderId = leaves.TeamLeaderId,
                EmployeeId = leaves.EmployeeId,
            };

                leaveStatusListAC.Add(leaveAC);
            }
            return leaveStatusListAC;
        }

        /// <summary>
        /// To get list of employee - team wise, who has got approved for leaveRequest. TeamLeader Calendar part
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public IEnumerable<LeaveRequestAC> TeamCalendarByProjectName(int? projectId, string teamLeaderId)
        {
            var teamList = _teamDataRepository.Fetch(x => x.ProjectId == projectId && x.TeamLeaderId == teamLeaderId);
            if (teamList.Count() == 0)
            {
                throw new Exception();
            }
            var teamListAC = new List<LeaveRequest>();
            var teamListResult = new List<LeaveRequestAC>();
            foreach (var leaves in teamList)
            {
                var requestList = _leaveRequestDataRepository.Fetch(x => x.EmployeeId == leaves.EmployeeId && x.Condition == LMS.DomainModel.Models.Condition.Approved).ToList();
                teamListAC.AddRange(requestList);
            }
            foreach (var leaves in teamListAC)
            {
                var employee = _employeeDataRepository.GetById(leaves.EmployeeId);
                var team = new LeaveRequestAC()
                {
                    Id = leaves.Id,
                    StartDate = leaves.StartDate,
                    EndDate = leaves.EndDate,
                    Unit = leaves.Unit,
                    Reason = leaves.Reason,
                    Type = leaves.Type,
                    HolidayName = leaves.HolidayName,
                    CompensationDate = leaves.CompensationDate,
                    CompensationStatus = leaves.CompensationStatus,
                    ContactName = leaves.ContactName,
                    EmergencyContact = leaves.ContactNumber,
                    PointOfContact = leaves.PointOfContact,
                    DoctorName = leaves.DoctorName,
                    DoctorContactNumber = leaves.Number,
                    DoctorCertificate = leaves.Certificate,
                    HolidayTypeId = leaves.HolidayTypeId,
                    EmployeeId = leaves.EmployeeId,
                    EmployeeName = employee.Name
                };
                teamListResult.Add(team);
            }
            return teamListResult;
        }

        /// <summary>
        /// Method used for getting detail of Leave Day allowed and used of a particular employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public LeaveDetailsAC GetLeaveDetail(string employeeId)
        {
            LeaveDetailsAC leaveDetails = new LeaveDetailsAC();
            var employee = _employeeDataRepository.GetById(employeeId);
            var CasualDays = 0.0;
            var SickDays = 0.0;
            var compensation = _leaveRequestDataRepository.Fetch(x => x.EmployeeId == employeeId && x.Type == LMS.DomainModel.Models.Type.Compensation && x.Condition==Condition.Approved);
            leaveDetails.Compensation = compensation.Count();
            var CasualLeaves = _leaveRequestDataRepository.Fetch(x => x.EmployeeId == employeeId && x.Type == LMS.DomainModel.Models.Type.Casual && x.Condition == Condition.Approved);
            foreach (var item in CasualLeaves)
            {
                CasualDays = CasualDays + (item.EndDate.Value.Subtract(item.StartDate).TotalDays)+1;
            }
            var SickLeaves = _leaveRequestDataRepository.Fetch(x => x.EmployeeId == employeeId && x.Type == LMS.DomainModel.Models.Type.Sick && x.Condition == Condition.Approved);
            foreach (var item in SickLeaves)
            {
                if (item.EndDate != null)
                {
                    SickDays = SickDays + (item.EndDate.Value.Subtract(item.StartDate).TotalDays)+1;
                }
                else
                {
                    SickDays = SickDays + (DateTime.Now.Subtract(item.StartDate).TotalDays)+1;
                }
            }
            leaveDetails.CasualLeaveLeft = (employee.CasualLeaveAllowed - CasualDays);
            leaveDetails.CasualLeaveTaken = CasualDays;
            leaveDetails.SickLeaveLeft = (employee.SickLeaveAllowed - SickDays);
            leaveDetails.SickLeaveTaken = SickDays;
            return leaveDetails;
        }

        /// <summary>
        /// To get list of Leave Request depend upon leave type of a particular employee
        /// </summary>
        /// <param name="type"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IEnumerable<LeaveRequestAC> LeaveRequestByType(LMS.DomainModel.Models.Type type, string employeeId)
        {
            var leaveRequestList = _leaveRequestDataRepository.Fetch(x => x.Type == type && x.EmployeeId == employeeId);
            var leaveRequestListAC = new List<LeaveRequestAC>();
            foreach (var leaves in leaveRequestList)
            {
                var leaveStatus = _leaveStatusDataRepository.Fetch(x => x.LeaveRequestId == leaves.Id);
                var employee = _employeeDataRepository.GetById(leaves.EmployeeId);
                var leaveAC = new LeaveRequestAC
                {
                    Id = leaves.Id,
                    StartDate = leaves.StartDate,
                    EndDate = leaves.EndDate,
                    Unit = leaves.Unit,
                    Reason = leaves.Reason,
                    Type = leaves.Type,
                    HolidayName = leaves.HolidayName,
                    CompensationDate = leaves.CompensationDate,
                    CompensationStatus = leaves.CompensationStatus,
                    ContactName = leaves.ContactName,
                    EmergencyContact = leaves.ContactNumber,
                    PointOfContact = leaves.PointOfContact,
                    DoctorName = leaves.DoctorName,
                    DoctorContactNumber = leaves.Number,
                    DoctorCertificate = leaves.Certificate,
                    HolidayTypeId = leaves.HolidayTypeId,
                    EmployeeId = leaves.EmployeeId,
                    Condition = ((DomainModel.Models.Condition)leaves.Condition),
                    EmployeeName = employee.Name
                };
                List<Condition> conditionList = new List<Condition>();
                List<string> teamLeaderList = new List<string>();
                List<string> requestReasonList = new List<string>();
                foreach (var leave in leaveStatus)
                {
                    List<string> list = new List<string>();
                    var employees = _employeeDataRepository.FirstOrDefault(x => x.Id == leave.TeamLeaderId);
                    teamLeaderList.Add(employees.Name);
                    requestReasonList.Add(leave.StatusReason);
                    conditionList.Add(leave.Condition);
                }
                leaveAC.TeamLeader = teamLeaderList;
                leaveAC.ResultReason = requestReasonList;
                leaveAC.ConditionList = conditionList;
                leaveRequestListAC.Add(leaveAC);
            }
            return leaveRequestListAC;
        }

        /// <summary>
        /// To get list of sick leave list 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LeaveRequestAC> SickLeaveList()
        {
            var leaveRequestList = _leaveRequestDataRepository.Fetch(x => x.Type == DomainModel.Models.Type.Sick);

            var leaveRequestListAC = new List<LeaveRequestAC>();

            foreach (var leaves in leaveRequestList)
            {
                var employee = _employeeDataRepository.GetById(leaves.EmployeeId);
                var leaveAC = new LeaveRequestAC
                {
                    Id = leaves.Id,
                    StartDate = leaves.StartDate,
                    EndDate = leaves.EndDate,
                    Unit = leaves.Unit,
                    Reason = leaves.Reason,
                    Type = leaves.Type,
                    HolidayName = leaves.HolidayName,
                    CompensationDate = leaves.CompensationDate,
                    CompensationStatus = leaves.CompensationStatus,
                    ContactName = leaves.ContactName,
                    EmergencyContact = leaves.ContactNumber,
                    PointOfContact = leaves.PointOfContact,
                    DoctorName = leaves.DoctorName,
                    DoctorContactNumber = leaves.Number,
                    DoctorCertificate = leaves.Certificate,
                    HolidayTypeId = leaves.HolidayTypeId,
                    EmployeeId = leaves.EmployeeId,
                    Condition = leaves.Condition,
                    EmployeeName = employee.Name
                };
                if (leaveAC.Condition == DomainModel.Models.Condition.HalfRejected || leaveAC.Condition == DomainModel.Models.Condition.Pending)
                {
                    leaveRequestListAC.Add(leaveAC);
                }
            }
            return leaveRequestListAC;
        }

        /// <summary>
        /// To get Compensation Leave list of employee
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LeaveRequestAC> CompensationLeaveList(string user)
        {
            var roles = _employeeDataRepository.FirstOrDefault(x => x.Id == user).Designation;
            var team = _teamDataRepository.Fetch(x => x.TeamLeaderId == user);
            var leaveRequestList = _leaveRequestDataRepository.Fetch(x => x.Type == DomainModel.Models.Type.Compensation);
            var leaveRequestListAC = new List<LeaveRequestAC>();
            foreach (var leaves in leaveRequestList)
            {
                var employee = _employeeDataRepository.GetById(leaves.EmployeeId);
                var leaveAC = new LeaveRequestAC
                {
                    Id = leaves.Id,
                    StartDate = leaves.StartDate,
                    EndDate = leaves.EndDate,
                    Unit = leaves.Unit,
                    Reason = leaves.Reason,
                    Type = leaves.Type,
                    HolidayName = leaves.HolidayName,
                    CompensationDate = leaves.CompensationDate,
                    CompensationStatus = leaves.CompensationStatus,
                    ContactName = leaves.ContactName,
                    EmergencyContact = leaves.ContactNumber,
                    PointOfContact = leaves.PointOfContact,
                    DoctorName = leaves.DoctorName,
                    DoctorContactNumber = leaves.Number,
                    DoctorCertificate = leaves.Certificate,
                    HolidayTypeId = leaves.HolidayTypeId,
                    EmployeeId = leaves.EmployeeId,
                    Condition = leaves.Condition,
                    EmployeeName = employee.Name,
                    Designation = employee.Designation,
                    //Days = (leaves.EndDate.Value.Day - leaves.StartDate.Day) + 1
                    Days = Convert.ToInt32(leaves.EndDate.Value.Subtract(leaves.StartDate).TotalDays)+1
                };
                if (roles == Designation.Admin)
                {
                    if (leaveAC.CompensationStatus == DomainModel.Models.Condition.Pending && leaveAC.Designation == Designation.TeamLeader)
                    {
                        leaveRequestListAC.Add(leaveAC);
                    }
                }
                else
                {
                    if (leaveAC.CompensationStatus == DomainModel.Models.Condition.Pending && leaveAC.Designation == Designation.User)
                    {
                        foreach (var item in team)
                        {
                            if (item.EmployeeId == leaveAC.EmployeeId)
                            {
                                leaveRequestListAC.Add(leaveAC);
                            }
                        }
                    }
                }
            }
            return leaveRequestListAC;
        }
    }
}

