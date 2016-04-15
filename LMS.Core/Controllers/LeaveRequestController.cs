using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LMS.DomainModel.DataRepository;
using LMS.Repository.Modules;
using LMS.Repository.Modules.LeaveReview;
using LMS.Repository.Modules.LeaveStatusRepository;
using System.Web;
using Microsoft.AspNet.Identity;
using LMS.DomainModel.ApplicationClasses;
using System.Data.Entity;
using LMS.DomainModel.DataContext;
using LMS.Repository.Modules.ProjectRepositoryFolder;
using LMS.Repository.Modules.EmployeeRepositoryFolder;
using LMS.DomainModel.Identity;
using LMS.Util.Email;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;

namespace LMS.Core.Controllers
{
    public class LeaveRequestController : ApiController
    {
        private readonly ILeaveReviewRepository _leaveReviewRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveStatusRepository _leaveStatusRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ApplicationUserManager _userManager;
        private readonly IEmailUtil _emailUtil;
        public LeaveRequestController(ILeaveRequestRepository leaveRequestRepository, ILeaveReviewRepository leaveReviewRepository, ILeaveStatusRepository leaveStatusepository, IProjectRepository projectRepository, IEmployeeRepository employeeRepository, ApplicationUserManager userManager, IEmailUtil emailUtil)
        {
            _emailUtil = emailUtil;
            _userManager = userManager;
            _projectRepository = projectRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveReviewRepository = leaveReviewRepository;
            _leaveStatusRepository = leaveStatusepository;
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// To get all leaves details expect pending
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/Leaves")]
        public IHttpActionResult GetAllLeavesDetails()
        {
            var list = _leaveReviewRepository.GetAllRequestsForReview();
            return Ok(list);
        }

        /// <summary>
        /// To get all leaves details only pending
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, TeamLeader")]
        [HttpGet]
        [Route("api/Leaves/Pending")]
        public IHttpActionResult GetAllPendingLeaves()
        {
            var list = _leaveReviewRepository.GetAllRequestsPending();
            return Ok(list);
        }

        /// <summary>
        /// To get leave detail of a particular employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Admin, TeamLeader")]
        //[HttpGet]
        //[Route("api/Employee/{employeeId}/Leaves")]
        //public IHttpActionResult GetLeavesDetailsForEmployee(string employeeId)
        //{
        //    if (_employeeRepository.GetEmployeeById(employeeId) == null)
        //    {
        //        return NotFound();
        //    }
        //    var list = _leaveReviewRepository.GetAllRequestsForReview(employeeId);
        //    return Ok(list);
        //}

        /// <summary>
        /// To get leave detail of a particular employee by leaveRequestId
        /// </summary>
        /// <param name="leaveRequestId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, TeamLeader, User")]
        [HttpGet]
        [Route("api/Employee/{leaveRequestId}/Leaves")]
        public IHttpActionResult GetLeavesDetailsForEmployee(int? leaveRequestId)
        {
            if (leaveRequestId == null)
            {
                return NotFound();
            }
            var list = _leaveRequestRepository.GetLeaveRequestById(leaveRequestId);
            return Ok(list);
        }

        /// <summary>
        /// To get leave detail depend on status FOR ADMIN
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/Leaves/{status}")]
        public IHttpActionResult GetAllLeavesDetailsByStatus(LMS.DomainModel.Models.Condition status)
        {
            var list = _leaveReviewRepository.LeaveRequest(status);
            return Ok(list);
        }

        /// <summary>
        /// To get leave details depend on status and employeeId
        /// </summary>
        /// <param name="status"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, TeamLeader")]
        [HttpGet]
        [Route("{employeeId}/EmployeeLeavesStatus/{status}")]
        public IHttpActionResult GetLeavesDetailsForEmployeeByStatus(LMS.DomainModel.Models.Condition status, string employeeId)
        {
            if (_employeeRepository.GetEmployeeById(employeeId) == null)
            {
                return NotFound();
            }
            var list = _leaveReviewRepository.LeaveRequest(status, employeeId);
            return Ok(list);
        }

        /// <summary>
        /// To cancel a leave request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "TeamLeader, User")]
        [HttpDelete]
        [Route("api/LeaveRequest/{id}")]
        public IHttpActionResult CancelLeaveRequest(int? id)
        {
            if (_leaveRequestRepository.GetLeaveRequestById(id) == null)
            {
                return NotFound();
            }
            var employeeId = User.Identity.GetUserId();
            bool result = _leaveRequestRepository.DeleteLeaveRequest(id, employeeId);
            try
            {
                if (!result)
                {
                    throw new Exception();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// To apply for a leave
        /// </summary>
        /// <param name="leaveRequest"></param>
        /// <returns></returns>
        [Authorize(Roles = "User,TeamLeader")]
        [HttpPost]
        [Route("api/LeaveRequest")]
        public IHttpActionResult CreateLeaveRequest(LeaveRequestAC leaveRequest)
        {
            if (leaveRequest.Type == DomainModel.Models.Type.Compensation)
            {
                leaveRequest.CompensationDate = leaveRequest.EndDate;
                leaveRequest.EndDate = leaveRequest.StartDate;
                leaveRequest.CompensationStatus = Condition.Pending;
            }
            leaveRequest.EmployeeId = User.Identity.GetUserId();
            if (leaveRequest.EmployeeId == null)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var body = _leaveRequestRepository.ApplyLeaveRequest(leaveRequest);
                    _leaveRequestRepository.Save();
                    _emailUtil.ApplyLeave(_employeeRepository.GetEmployeeById(leaveRequest.EmployeeId).Email,body,leaveRequest.TeamLeader);
                }
                return Ok(leaveRequest);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// TeamLeader and Admin can change request Status{Pending, Escalated, Approved, Rejected}
        /// </summary>
        /// <param name="status"></param>
        /// <param name="leaveRequest"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, TeamLeader")]
        [HttpPut]
        [Route("api/LeaveRequest")]
        public IHttpActionResult LeaveRequestResult(LeaveRequestStatusResultAC leaveRequestStatusResult)
        {
            if (leaveRequestStatusResult == null)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    leaveRequestStatusResult.TeamLeaderId = User.Identity.GetUserId();
                    if (_userManager.IsInRole(leaveRequestStatusResult.TeamLeaderId, "Admin"))
                    {
                        var body = _leaveStatusRepository.InsertLeaveStatusByAdmin(leaveRequestStatusResult);
                        _leaveRequestRepository.Save();
                        _emailUtil.ApproveAndReject(_employeeRepository.GetEmployeeById(leaveRequestStatusResult.TeamLeaderId).Email, body, _employeeRepository.GetEmployeeById(_leaveRequestRepository.GetLeaveRequestById(leaveRequestStatusResult.LeaveRequestId).EmployeeId).Email);
                    }
                    else
                    {
                        var body = _leaveStatusRepository.InsertLeaveStatus(leaveRequestStatusResult);
                        _leaveRequestRepository.Save();
                        _emailUtil.ApproveAndReject(_employeeRepository.GetEmployeeById(leaveRequestStatusResult.TeamLeaderId).Email,body,_employeeRepository.GetEmployeeById(_leaveRequestRepository.GetLeaveRequestById(leaveRequestStatusResult.LeaveRequestId).EmployeeId).Email);
                    }
                }
                return Ok(leaveRequestStatusResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// To get list of leaverequest under a teamLeader using TeamLeaderId
        /// </summary>
        /// <param name="teamLeaderId"></param>
        /// <returns></returns>
        [Authorize(Roles = "TeamLeader")]
        [HttpGet]
        [Route("api/TeamLeaves")]
        public IHttpActionResult GetAllLeaveRequestListForTeam()
        {
            var teamLeaderId = User.Identity.GetUserId();
            var list = _leaveReviewRepository.LeaveRequestUnderTeamLeader(teamLeaderId);
            return Ok(list);
        }

        /// <summary>
        /// Authorized. Admin creates a sick leave for a particular employee and provides a startDate
        /// </summary>
        /// <param name="sickLeaveRequest"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/SickLeaveRequest")]
        public IHttpActionResult ApplyForSickLeave(LeaveRequest sickLeaveRequest)
        {
            if (sickLeaveRequest.EmployeeId == null)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _leaveRequestRepository.ApplySickLeave(sickLeaveRequest);
                }
                return Ok(sickLeaveRequest);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Unauthorized. Employee edits his/her sick leave status and uploads medical details and endDate
        /// </summary>
        /// <param name="sickLeaveUpdateRequest"></param>
        /// <returns></returns>
        [Authorize(Roles = "TeamLeader, User")]
        [HttpPut]
        [Route("api/SickLeaveUpdate")]
        public IHttpActionResult UpdateSickLeave()
        {
            var postedFile = HttpContext.Current.Request.Files["file"];
            var root = HttpContext.Current.Server.MapPath("~/DoctorCertificate/");
            Directory.CreateDirectory(root);
            //Directory.CreateDirectory("~/App_Data/Temp/");
            var newFileName = postedFile.FileName;
            postedFile.SaveAs(root+newFileName);
            var sickLeaveUpdateRequest = JsonConvert.DeserializeObject<LeaveRequest>(HttpContext.Current.Request.Form["data"]);
            var employeeId = User.Identity.GetUserId();
            sickLeaveUpdateRequest.Certificate = "DoctorCertificate/" + newFileName;
            if (sickLeaveUpdateRequest.EmployeeId != employeeId)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _leaveRequestRepository.UpdateSickLeaveRequest(sickLeaveUpdateRequest);
                    _leaveRequestRepository.Save();
                }
                return Ok(sickLeaveUpdateRequest);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// To get list of leaveRequest under a teamLeader using TeamLeaderId via status
        /// </summary>
        /// <param name="teamLeaderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Authorize(Roles = "TeamLeader")]
        [HttpGet]
        [Route("api/TeamLeaves/{status}")]
        public IHttpActionResult GetLeaveRequestListForTeamByStatus(LMS.DomainModel.Models.Condition status)
        {
            var teamLeaderId = User.Identity.GetUserId();
            var list = _leaveReviewRepository.LeaveRequestUnderTeamLeader(teamLeaderId, status);
            return Ok(list);
        }

        /// <summary>
        /// To get List of employee got approved, project Wise
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        //[Authorize(Roles = "TeamLeader")]
        [HttpGet]
        [Route("api/TeamCalendar/{projectName}")]
        public IHttpActionResult TeamCalendar(string projectName)
        {
            var projectId = _projectRepository.GetProjectByName(projectName);
            if (_projectRepository.GetTeamProjectById(projectId) == null)
            {
                return NotFound();
            }
            var teamLeaderId = User.Identity.GetUserId();
            try
            {
                var list = _leaveReviewRepository.TeamCalendarByProjectName(projectId, teamLeaderId);
                return Ok(list);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// To get List of employee got approved, project Wise
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [Authorize(Roles = "User, TeamLeader")]
        [HttpGet]
        [Route("api/employeeLeaveDetails")]
        public IHttpActionResult EmployeeLeaveDetails()
        {
            var employeeId = User.Identity.GetUserId();
            if (employeeId == null)
            {
                return NotFound();
            }
            try
            {
                var leavesDetails = _leaveReviewRepository.GetLeaveDetail(employeeId);
                return Ok(leavesDetails);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "User, TeamLeader")]
        [HttpGet]
        [Route("api/LeaveRequestByType/{leaveType}")]
        public IHttpActionResult LeaveRequestByType(LMS.DomainModel.Models.Type leaveType)
        {
            var employeeId = User.Identity.GetUserId();
            if (employeeId == null)
            {
                return NotFound();
            }
            try
            {
                var leavesDetails = _leaveReviewRepository.LeaveRequestByType(leaveType, employeeId);
                return Ok(leavesDetails);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// To get leave Request depend on Team and status pending
        /// </summary>
        /// <returns></returns>
        [Route("api/Team/Pending")]
        [HttpGet]
        public IHttpActionResult LeaveRequestUnderTeamLeaderPending()
        {
            var teamLeaderId = User.Identity.GetUserId();
            if (teamLeaderId == null)
            {
                return NotFound();
            }
            try
            {
                var list = _leaveReviewRepository.LeaveRequestUnderTeamLeaderPending(teamLeaderId);
                return Ok(list);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Sick Leave Approve and Reject by Admin
        /// </summary>
        /// <param name="leaveStatus"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [Route("api/SickLeaveStatus")]
        [HttpPut]
        public IHttpActionResult SickLeaveResultStatus(LeaveStatusAC leaveStatus)
        {
            if (leaveStatus == null)
            {
                return NotFound();
            }
            try
            {
                _leaveStatusRepository.SickLeaveResultStatus(leaveStatus);
                return Ok(leaveStatus);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Sick Leave List
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [Route("api/SickLeavePending")]
        [HttpGet]
        public IHttpActionResult SickLeaveList()
        {
            var list = _leaveReviewRepository.SickLeaveList();
            return Ok(list);
        }

        /// <summary>
        /// Compensation Leave Approve and Reject by Admin and TL
        /// </summary>
        /// <param name="leaveStatus"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, TeamLeader")]
        [Route("api/CompensationLeaveStatus")]
        [HttpPut]
        public IHttpActionResult CompensationLeaveResultStatus(LeaveStatusAC leaveStatus)
        {
            if (leaveStatus == null)
            {
                return NotFound();
            }
            try
            {
                _leaveStatusRepository.CompensationLeaveResultStatus(leaveStatus);
                return Ok(leaveStatus);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Compensation Leave List
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,TeamLeader")]
        [Route("api/CompensationLeavePendingTL")]
        [HttpGet]
        public IHttpActionResult CompensationLeaveList()
        {
            var list = _leaveReviewRepository.CompensationLeaveList(User.Identity.GetUserId());
            return Ok(list);
        }


        //[Route("api/FilesUpload")]
        //[HttpPost]
        //public async Task<IHttpActionResult> FileAdd()
        //{
        //    if(Request.Content.IsMimeMultipartContent("form-data"))
        //    {
        //        return BadRequest("Unsupported media type");
        //    }
        //    try
        //    {
        //        var provider = new CustomMultipartFormDataStreamProvider(workingFolder);
        //        await Request.Content.ReadAsMultipartAsync(provider);
        //        var photo = provider.FileData.Select(file=> new FileInfo(file.LocalFileName)).Select(fileInfo=>new )
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}
    }
}
