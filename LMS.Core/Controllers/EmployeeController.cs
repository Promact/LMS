using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using LMS.DomainModel.Models;
using LMS.Repository.Modules;
using LMS.DomainModel.DataContext;
using Microsoft.AspNet.Identity;
using LMS.DomainModel.Identity;
using LMS.DomainModel.ApplicationClasses;
using LMS.Repository.Modules.EmployeeRepositoryFolder;



namespace LMS.Core.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _employeeRepository;
        private ApplicationUserManager _userManager;

        public EmployeeController(IEmployeeRepository employeeRepository, ApplicationUserManager userManager)
        {
            this._employeeRepository = employeeRepository;
            _userManager = userManager;
        }


        [Route("api/Employee")]
        [HttpGet]
        public IHttpActionResult GetAllEmployees()
        {
            return Ok(_employeeRepository.GetAllEmployees());
        }


        [AllowAnonymous]
        [Route("api/Employee/{id}")]
        [HttpGet]
        public IHttpActionResult GetEmployeeById(string id)
        {

            EmployeeViewModel user = _employeeRepository.GetEmployeeById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [Route("api/Employee")]
        [HttpPost]
        public IHttpActionResult AddEmployee(EmployeeViewModel user)
        {
            try
            {
                if(user == null)
                {
                    return BadRequest();
                }
                EmployeeViewModel createdUser = null ;
                
                if (ModelState.IsValid)
                {
                    createdUser = _employeeRepository.AddEmployee(user);
                    return Ok(createdUser);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [AllowAnonymous]
        [Route("api/Employee")]
        [HttpPut]
        public IHttpActionResult UpdateEmployee(EmployeeViewModel user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            EmployeeViewModel updatedUser = null;
            try
            {
                if (ModelState.IsValid)
                {
                    updatedUser = _employeeRepository.UpdateEmployee(user);
                    return Ok(updatedUser);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                
                return BadRequest();
            }
           
        }



        [Route("api/Employee/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(string id)
        {
            var employee = _userManager.FindById(id);

            if (employee != null)
            {
                _employeeRepository.DeleteEmployee(id);
                return Ok();
            }
            return NotFound();
        }

        [AllowAnonymous]
        [Route("api/Employee/Update")]
        [HttpPut]
        public IHttpActionResult ChangePassword(ChangePasswordAC changePassword)
        {

            var result = _employeeRepository.ChangePassword(changePassword);

            if (result.Succeeded)
            {
                return Ok();

            }
            return BadRequest();
        }

    }

}
