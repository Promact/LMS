using LMS.DomainModel.ApplicationClasses;
using LMS.DomainModel.Models;
using LMS.Repository.Modules;
using LMS.Repository.Modules.ProjectRepositoryFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace LMS.Core.Controllers
{
    [Authorize(Roles="Admin, TeamLeader")]
    public class ProjectController : ApiController
    {
        private IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        
        [Route("api/Project")]
        [HttpGet]
        public IHttpActionResult GetAllTeamProjects()
        {
            try { return Ok(_projectRepository.GetAllTeamProjects()); }
            catch { return BadRequest(); }
        }
       
        [Route("api/Project/{id}")]
        [HttpGet]
        public IHttpActionResult GetTeamProjectDetailsById(int? id)
        {
           
          try
          {
              TeamProjectAC team = _projectRepository.GetTeamProjectById(id);

              if (team == null)
              {
                  return NotFound();
              }
              return Ok(team);
          }
            catch(Exception)
          {
              return BadRequest();
          }
        }
        //[Authorize(Roles = "TeamLeader")]

        [Route("api/Project/TeamLeaderId")]
        [HttpGet]
        public IHttpActionResult getAllProjectByTeamLeaderId()
        {

            try
            {
                var teamLeaderId = User.Identity.GetUserId();
                var team = _projectRepository.GetTeamProjects(teamLeaderId);

                if (team == null)
                {
                    return NotFound();
                }
                return Ok(team);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        
        [Route("api/Project")]
        [HttpPost]
        public IHttpActionResult AddTeamProject(TeamProjectAC teamProject)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    _projectRepository.InsertTeamProject(teamProject);

                    return Ok();
                }
                return BadRequest();
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        
        [Route("api/Project")]
        [HttpPut]
        public IHttpActionResult UpdateTeamProject(TeamProjectAC teamProject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  _projectRepository.UpdateTeamProject(teamProject);
                   
                    return Ok();
                }
                return BadRequest();
            }
            catch(Exception)
            { return BadRequest(); }
        }
       
        [Route("api/Project/Archive/")]
        [HttpPut]
        public IHttpActionResult ArchiveTeamProject(TeamProjectAC teamProject)
        {
            try
            {
                _projectRepository.ArchiveTeamProject(teamProject.ProjectId);
                return Ok();
            }
            catch(Exception)
            { return BadRequest(); }
        }
        [Route("api/Project/Employee")]
        [HttpGet]
        public IHttpActionResult GetAllEmployee()
        {
            try
            {
                return Ok(_projectRepository.GetEmployees());
            }
            catch(Exception)
            { return BadRequest(); }
        }
        [Route("api/Project/TeamLeader")]
        [HttpGet]
        public IHttpActionResult GetAllTeamLeader()
        {
            try
            {
                return Ok(_projectRepository.GetTeamLeaders());
            }
            catch { return BadRequest(); }
        }
        [Route("api/Project/Projects")]
        [HttpGet]
        public IHttpActionResult GetAllProjects()
        {
            try
            {
                return Ok(_projectRepository.GetAllProjects());
            }
            catch { return BadRequest(); }
        }


    }
}
