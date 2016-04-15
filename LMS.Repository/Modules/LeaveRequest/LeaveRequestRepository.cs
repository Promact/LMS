using LMS.DomainModel.DataContext;
using LMS.DomainModel.Models;
using LMS.DomainModel.DataRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DomainModel.ApplicationClasses;
using LMS.DomainModel.Identity;
using LMS.Util.Email_Templates;


namespace LMS.Repository.Modules
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private IDataRepository<LeaveRequest> _leaveRequestDataRepository;
        private IDataRepository<LeaveStatus> _leaveStatusDataRepository;
        private IDataRepository<Team> _teamDataRepository;
        private readonly IDataRepository<ApplicationUser> _employeeRepository;
        private readonly ApplicationUserManager _userManager;
        private readonly IDataRepository<Project> _projectRepository;

        public LeaveRequestRepository(IDataRepository<LeaveRequest> leaveRequestDataRepository, IDataRepository<LeaveStatus> leaveStatusRepository, IDataRepository<Team> teamRepository, IDataRepository<ApplicationUser> employeeRepository, ApplicationUserManager userManager, IDataRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
            _employeeRepository = employeeRepository;
            _teamDataRepository = teamRepository;
            _leaveStatusDataRepository = leaveStatusRepository;
            _leaveRequestDataRepository = leaveRequestDataRepository;

        }
        /// <summary>
        /// Creates a new Leave request and adds to the table 'LeaveRequest'
        /// </summary>
        /// <param name="leaveRequest"></param>
        public string ApplyLeaveRequest(LeaveRequestAC leaveRequest)
        {
            LeaveRequest leave = new LeaveRequest()
            {
                Id = leaveRequest.Id,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Unit = leaveRequest.Unit,
                Reason = leaveRequest.Reason,
                Type = leaveRequest.Type,
                HolidayName = leaveRequest.HolidayName,
                CompensationDate = leaveRequest.CompensationDate,
                CompensationStatus = leaveRequest.CompensationStatus,
                ContactName = leaveRequest.ContactName,
                ContactNumber = leaveRequest.EmergencyContact,
                PointOfContact = leaveRequest.PointOfContact,
                DoctorName = leaveRequest.DoctorName,
                Number = leaveRequest.DoctorContactNumber,
                Certificate = leaveRequest.DoctorCertificate,
                HolidayTypeId = leaveRequest.HolidayTypeId,
                EmployeeId = leaveRequest.EmployeeId,
                CreatedOn = DateTime.Now,
                Condition = Condition.Pending
                
            };
            LeaveStatus leaveStatus = new LeaveStatus();
            _leaveRequestDataRepository.Insert(leave);
            _leaveRequestDataRepository.Save();
            List<string> teamLeader = new List<string>();
            //if (_employeeRepository.FirstOrDefault(x => x.Id == leave.EmployeeId).Designation == Designation.User)
            //{
                var list = _teamDataRepository.Fetch(x => x.EmployeeId == leaveRequest.EmployeeId);
                if (list.Count() == 0)
                {
                    leaveStatus.TeamLeaderId = _employeeRepository.FirstOrDefault(x => x.Email == "admin@promactinfo.com").Id;
                    leaveStatus.Condition = Condition.Pending;
                    leaveStatus.CreatedOn = DateTime.Now;
                    leaveStatus.EmployeeId = leaveRequest.EmployeeId;
                    leaveStatus.LeaveRequestId = leave.Id;
                    _leaveStatusDataRepository.Insert(leaveStatus);
                    leaveRequest.EmployeeName = _employeeRepository.GetById(leaveRequest.EmployeeId).Name;
                    leaveRequest.Designation = _employeeRepository.GetById(leaveRequest.EmployeeId).Designation;
                    leaveRequest.CreatedOn = leaveStatus.CreatedOn;
                }
                else
                {
                    foreach (var item in list)
                    {
                        if (_projectRepository.GetById(item.ProjectId).IsProjectArchived == false)
                        {
                            teamLeader.Add(_employeeRepository.GetById(item.TeamLeaderId).Email);
                            leaveStatus.Condition = LMS.DomainModel.Models.Condition.Pending;
                            leaveStatus.CreatedOn = DateTime.Now;
                            leaveStatus.EmployeeId = leaveRequest.EmployeeId;
                            leaveStatus.LeaveRequestId = leave.Id;
                            var employee = _employeeRepository.GetById(leaveRequest.EmployeeId);
                            leaveStatus.TeamLeaderId = item.TeamLeaderId;
                            leaveRequest.EmployeeName = employee.Name;
                            leaveRequest.Designation = employee.Designation;
                            leaveRequest.CreatedOn = leaveStatus.CreatedOn;
                            _leaveStatusDataRepository.Insert(leaveStatus);
                        }
                    }
                }
                teamLeader.Add("admin@promactinfo.com");
                leaveRequest.TeamLeader = teamLeader;
         
            //else
            //{
            //    var admin = _employeeRepository.FirstOrDefault(x => x.Email == "admin@promactinfo.com");
            //    leaveStatus.Condition = Condition.Pending;
            //    leaveStatus.CreatedOn = DateTime.Now;
            //    leaveStatus.EmployeeId = leaveRequest.EmployeeId;
            //    leaveStatus.LeaveRequestId = leave.Id;
            //    leaveStatus.TeamLeaderId = admin.Id;
            //    _leaveStatusDataRepository.Insert(leaveStatus);
            //}
            _leaveStatusDataRepository.Save();
            _leaveRequestDataRepository.Save();
            LeaveApply test = new LeaveApply();
            test.Session = new Dictionary<string, object> { 
            {"Name", leaveRequest.EmployeeName},
            {"Designation", leaveRequest.Designation.ToString()},
            {"SiteUrl", leaveRequest.EmployeeName},
            {"SiteLogo", leaveRequest.Designation.ToString()},
            {"ContactNumber", leaveRequest.EmergencyContact},
            {"Type", leaveRequest.Type.ToString()},
            {"Unit", leaveRequest.Unit.ToString()},
            {"StartDate", leaveRequest.StartDate.ToString("dd,MM,yyyy")},
            {"EndDate", leaveRequest.EndDate.Value.ToString("dd,MM,yyyy")},
            {"CreatedOn", leaveRequest.CreatedOn.Value.ToString("dd,MM,yyyy")},
            };
            test.Initialize();
            var body = test.TransformText();
            return body;
        }


        /// <summary>
        /// Fetches a leave request by its id from table 'LeaveRequest'
        /// </summary>
        /// <param name="id"></param>
        /// <returns>LeaveRequest type entity</returns>
        public LeaveRequestAC GetLeaveRequestById(int? id)
        {
            var leave = _leaveRequestDataRepository.GetById(id);
            var employee = _employeeRepository.GetById(leave.EmployeeId);
            LeaveRequestAC leaves = new LeaveRequestAC()
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
                EmergencyContact = leave.ContactNumber,
                ContactName = leave.ContactName,
                PointOfContact = leave.PointOfContact,
                DoctorName = leave.DoctorName,
                HolidayTypeId = leave.HolidayTypeId,
                EmployeeId = leave.EmployeeId,
                CreatedOn = leave.CreatedOn,
                Condition = leave.Condition,
                EmployeeName = employee.Name,
                Email = employee.Email,
                Designation = employee.Designation,
                DoctorCertificate = leave.Certificate,
                DoctorContactNumber = leave.ContactNumber,
               
            };
            return leaves;
        }

        /// <summary>
        /// Deletes a particular leave request by its id
        /// </summary>
        /// <param name="id"></param>
        public bool DeleteLeaveRequest(int? id, string employeeId)
        {
            var leave = _leaveRequestDataRepository.GetById(id);
            if (leave.EndDate.Value.Date>DateTime.Now)
            {
                if (leave.EmployeeId == employeeId && leave.Condition != Condition.Rejected)
                {
                    leave.Condition = Condition.Cancel;
                    _leaveRequestDataRepository.Update(leave);
                    _leaveRequestDataRepository.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else { return false; }
        }

        /// <summary>
        /// Authorized. Only Admin will provide sickLeave details for a particular employee.
        /// </summary>
        /// <param name="sickLeaveRequest"></param>
        public void ApplySickLeave(LeaveRequest sickLeaveRequest)
        {
            sickLeaveRequest.CreatedOn = DateTime.Now;
            _leaveRequestDataRepository.Insert(sickLeaveRequest);
        }

        // <summary>
        /// Employee can add their endDate of sick leave
        /// </summary>
        /// <param name="sickLeaveUpdateRequest"></param>
        public void UpdateSickLeaveRequest(LeaveRequest sickLeaveUpdateRequest)
        {
            _leaveRequestDataRepository.Update(sickLeaveUpdateRequest);
            _leaveRequestDataRepository.Save();
        }

        public void Save()
        {
            _leaveRequestDataRepository.Save();
        }
    }
}
