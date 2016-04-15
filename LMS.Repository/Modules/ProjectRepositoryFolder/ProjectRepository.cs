using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DomainModel.DataRepository;
using LMS.DomainModel.Models;
using LMS.DomainModel.ApplicationClasses;
using LMS.Repository.Modules.EmployeeRepositoryFolder;
using LMS.DomainModel.ApplicationClasses;
namespace LMS.Repository.Modules.ProjectRepositoryFolder
{
    public class ProjectRepository : IProjectRepository
    {
        private IDataRepository<Team> _teamDataRepository;
        private IDataRepository<Project> _projectDataRepository;
        private IDataRepository<ApplicationUser> _employeeDataRepository;
        public ProjectRepository(IDataRepository<Team> _teamDataRepository, IDataRepository<Project> _projectDataRepository, IDataRepository<ApplicationUser> _employeeDataRepository)
        {
            this._teamDataRepository = _teamDataRepository;
            this._projectDataRepository = _projectDataRepository;
            this._employeeDataRepository = _employeeDataRepository;
        }
        public List<EmployeeViewModel> GetTeamLeaders()
        {
            var list = _employeeDataRepository.List();
            var teamLeaders = _employeeDataRepository.Fetch(x => x.Designation.Equals(LMS.DomainModel.Models.Designation.TeamLeader)&& x.IsSoftDeleted.Equals(false)).ToList();
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            foreach (var applicationUser in teamLeaders)
            {
                EmployeeViewModel employee = new EmployeeViewModel { Id = applicationUser.Id, Name = applicationUser.Name, Designation = applicationUser.Designation, PhoneNumber = applicationUser.PhoneNumber, DateOfJoining = applicationUser.DateOfJoining, Email = applicationUser.Email, Password = applicationUser.PasswordHash };
                employees.Add(employee);
            }
        
            return employees;
            
        }
        public List<EmployeeViewModel> GetEmployees()
        {
            var employeeList = _employeeDataRepository.Fetch(x => x.Designation.Equals(LMS.DomainModel.Models.Designation.User) && x.IsSoftDeleted.Equals(false)).ToList();
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            foreach (var applicationUser in employeeList)
        {
                EmployeeViewModel employee = new EmployeeViewModel { Id = applicationUser.Id, Name = applicationUser.Name, Designation = applicationUser.Designation, PhoneNumber = applicationUser.PhoneNumber, DateOfJoining = applicationUser.DateOfJoining, Email = applicationUser.Email, Password = applicationUser.PasswordHash };
                employees.Add(employee);
            }

            return employees;
        }

        public void  InsertTeamProject(TeamProjectAC teamProject)
        {

            Team team = new Team();
            Project project = new Project();
            project.ProjectName = teamProject.ProjectName;
            project.IsProjectArchived = false;
            project.CreatedOn = DateTime.Now;
            _projectDataRepository.Insert(project);
            team.ProjectId = project.Id;
            team.TeamLeaderId = teamProject.TeamLeader.Id;

            team.CreatedOn = DateTime.Now;
            foreach (var teams in teamProject.ListOfEmployee)
            {
                team.EmployeeId = teams.Id;
                _teamDataRepository.Insert(team);

            }    
        }

        public void ArchiveTeamProject(int? projectId)
        {
            Project project=_projectDataRepository.FirstOrDefault(x => x.Id == projectId);
            
            if(project!=null)
            {
                project.IsProjectArchived=true;
                _projectDataRepository.Update(project);

            }
            project = _projectDataRepository.FirstOrDefault(x => x.Id == projectId);
            project = null;
        }


        public TeamProjectAC GetTeamProjectById(int? projectId)
        {
            var team = _teamDataRepository.Fetch(x => x.ProjectId == projectId, "Employee,TeamLeader").ToList();
            
            if(team.Count==0)
            {
                TeamProjectAC teamProjectNull = null;
                return teamProjectNull;
            }
            TeamProjectAC teamProject = new TeamProjectAC();
            teamProject.ListOfEmployee = new List<EmployeeViewModel>();
            foreach (var employee in team)
            {
                teamProject.ProjectName = employee.Project.ProjectName;
                teamProject.IsProjectArchived = employee.Project.IsProjectArchived;
                teamProject.Id = employee.Id;
                teamProject.TeamLeader = new EmployeeViewModel { Id = employee.TeamLeader.Id, Name = employee.TeamLeader.Name, Email = employee.TeamLeader.Email, DateOfJoining = employee.TeamLeader.DateOfJoining };


                teamProject.ProjectId = employee.ProjectId;
                teamProject.ListOfEmployee.Add(new EmployeeViewModel { Id = employee.Employee.Id, Name = employee.Employee.Name, Email = employee.Employee.Email, DateOfJoining = employee.Employee.DateOfJoining });
            }

            return (teamProject);
        }

        public string GetProjectNameById(int? projectId)
        {
            Project project = _projectDataRepository.GetById(projectId);
            if(project!=null)
            {
                return project.ProjectName;
            }
            return null;
        }

        public int GetProjectByName(string projectName)
        {
            Project project = _projectDataRepository.FirstOrDefault(x => x.ProjectName == projectName);
            if (project != null)
            {
                return project.Id;
            }
            return 0;
        }
        public List<Project> GetAllProjects()
        {
            return (_projectDataRepository.List().ToList());
        }


        public void UpdateTeamProject(TeamProjectAC teamProject)
        {
            var team = _teamDataRepository.Fetch(x => x.ProjectId == teamProject.ProjectId);
            if(team.Count()>0)
            {
               var teamList=team.ToList();
                 teamList.Reverse();
                   foreach (var teams in teamList)
                    {
                        _teamDataRepository.Delete(teams.Id);
                    }
                _projectDataRepository.Delete(teamProject.ProjectId);
                    InsertTeamProject(teamProject);
            }
            
              
         }

        public IEnumerable<Project> GetTeamProjects(string teamLeaderId)
        {
            var list = _teamDataRepository.Fetch(x => x.TeamLeaderId == teamLeaderId);
            int temporary=-1;
            List<Project> team = new List<Project>();
            foreach (var item in list)
            {
                var aaaa = _projectDataRepository.GetById(item.ProjectId);
                if(temporary!=item.ProjectId && aaaa.IsProjectArchived==false)
                {
                    team.Add(aaaa);
                }
                 temporary = item.ProjectId;
            }
            return team;
        }

        


        public IEnumerable<TeamProjectAC> GetAllTeamProjects()
        {
            List<Project> projectList = _projectDataRepository.List().ToList();
            var teamList = _teamDataRepository.List().GroupBy(x => x.ProjectId).ToList();
            List<TeamProjectAC> teamProjectList = new List<TeamProjectAC>();
            foreach (var team in teamList)
            {
                TeamProjectAC teamProject = new TeamProjectAC();
                teamProject.ListOfEmployee = new List<EmployeeViewModel>();
                foreach (var check in team)
                {
                    teamProject.ProjectName = check.Project.ProjectName;
                    teamProject.IsProjectArchived = check.Project.IsProjectArchived;
                    teamProject.Id = check.Id;
                    teamProject.TeamLeader = new EmployeeViewModel { Id = check.TeamLeader.Id, Name = check.TeamLeader.Name, Email = check.TeamLeader.Email, DateOfJoining = check.TeamLeader.DateOfJoining };
                    
                    teamProject.ProjectId = check.ProjectId;
                    teamProject.ListOfEmployee.Add(new EmployeeViewModel { Id = check.Employee.Id , Name=check.Employee.Name,Email=check.Employee.Email,DateOfJoining=check.Employee.DateOfJoining});
                }
                teamProjectList.Add(teamProject);
            } return teamProjectList;
        }


    }
}

