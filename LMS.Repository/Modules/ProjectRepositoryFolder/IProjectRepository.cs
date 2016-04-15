using LMS.DomainModel.ApplicationClasses;
using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repository.Modules.ProjectRepositoryFolder
{
    public interface IProjectRepository
    {
        List<EmployeeViewModel> GetTeamLeaders();
        List<EmployeeViewModel> GetEmployees();
        IEnumerable<TeamProjectAC> GetAllTeamProjects();
        string GetProjectNameById(int? projectId);
        int GetProjectByName(string projectName);
        TeamProjectAC GetTeamProjectById(int? projectId);
        void InsertTeamProject(TeamProjectAC teamProject);
        void ArchiveTeamProject(int? projectId);
        void UpdateTeamProject(TeamProjectAC teamProject);
        List<Project> GetAllProjects();
        IEnumerable<Project> GetTeamProjects(string teamLeaderId);

      
    }
}
