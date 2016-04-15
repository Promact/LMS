using LMS.DomainModel.Models;
using LMS.Repository.Modules.LeaveDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LMS.Core.Controllers
{
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/LeaveAllowed")]
    public class LeaveAllowedController : ApiController
    {
        private readonly ILeaveAllowedRepository _leaveAllowed;
        public LeaveAllowedController(ILeaveAllowedRepository leaveAllowed)
        {
            _leaveAllowed = leaveAllowed;
        }

        [HttpGet]
        public IHttpActionResult GetAllHoliday()
        {
            return Ok(_leaveAllowed.GetAllLeaveTypes());
        }

        
        [HttpGet]
        [Route("Details/{Id}")]
        public IHttpActionResult GetHolidayById(int Id)
        {
            var leave = _leaveAllowed.GetLeaveTypeById(Id);
            if (leave == null)
            {
                return NotFound();
            }
            return Ok(leave);
        }

        [HttpPost]
        public IHttpActionResult AddLeaveAllowedByLeaveType(LeaveAllowed leaveAllowed)
        {
            _leaveAllowed.Add(leaveAllowed);
            return Ok(leaveAllowed);
        }

        
        [HttpPut]
        [Route("Edit")]
        public IHttpActionResult UpdateLeaveAllowedByLeaveType(LeaveAllowed leaveAllowed)
        {
            _leaveAllowed.Update(leaveAllowed);
            return Ok(leaveAllowed);
        }
    }
}
