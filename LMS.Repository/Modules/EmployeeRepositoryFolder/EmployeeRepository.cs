using LMS.DomainModel.DataContext;
using LMS.DomainModel.DataRepository;
using LMS.DomainModel.Models;
using LMS.Repository.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DomainModel.ApplicationClasses;
using LMS.DomainModel.Identity;
using Microsoft.AspNet.Identity;

namespace LMS.Repository.Modules.EmployeeRepositoryFolder
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private IDataRepository<ApplicationUser> _applicationUserDataRepository;
        private ApplicationUserManager _userManager;
        private readonly IDataRepository<LeaveAllowed> _leaveAllowedDataRepository;
        private readonly IDataRepository<Team> _teamDataRepository;

        public EmployeeRepository(IDataRepository<ApplicationUser> applicationUserDataRepository, ApplicationUserManager userManager, IDataRepository<LeaveAllowed> leaveAllowedDataRepository, IDataRepository<Team> teamDataRepository)
        {
            this._applicationUserDataRepository = applicationUserDataRepository;
            _userManager = userManager;
            _leaveAllowedDataRepository = leaveAllowedDataRepository;
            _teamDataRepository = teamDataRepository;
        }

        public IEnumerable<EmployeeViewModel> GetAllEmployees()
        {
            List<ApplicationUser> users= new List<ApplicationUser>();
            List<ApplicationUser> user = _userManager.Users.ToList();
           
            List<ApplicationUser> applicationUsers = _userManager.Users.ToList();
            foreach (var item in applicationUsers)
            {
                if (item.Designation != LMS.DomainModel.Models.Designation.Admin && item.IsSoftDeleted == false)
                {
                    users.Add(item);
                }
            }
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            foreach(var applicationUser in users)
            {
               EmployeeViewModel employee = new EmployeeViewModel{Id=applicationUser.Id,Name = applicationUser.Name,Designation =applicationUser.Designation,PhoneNumber=applicationUser.PhoneNumber,DateOfJoining=applicationUser.DateOfJoining,Email=applicationUser.Email,Password=applicationUser.PasswordHash};
               employees.Add(employee);
            }

            return employees;
        }
        public EmployeeViewModel GetEmployeeById(string employeeId)
        {
            var user = new ApplicationUser();
            user = _userManager.FindById(employeeId);
            if (user != null)
            {
                EmployeeViewModel employee = new EmployeeViewModel { Id = user.Id, Name = user.Name, PhoneNumber = user.PhoneNumber, Email = user.Email, DateOfJoining = user.DateOfJoining, Designation = user.Designation, Password = user.PasswordHash };
                return employee;
            }
            return null;
        }
        public EmployeeViewModel AddEmployee(EmployeeViewModel employee)
        {
                ApplicationUser appUser = new ApplicationUser { UserName = employee.Email, Designation = employee.Designation, PhoneNumber = employee.PhoneNumber, Name = employee.Name, Email = employee.Email, DateOfJoining = employee.DateOfJoining, CasualLeaveAllowed = CalculateAllowedLeaves(employee.DateOfJoining).CasualLeave, SickLeaveAllowed = CalculateAllowedLeaves(employee.DateOfJoining).SickLeave };
                _userManager.Create(appUser, employee.Password);
                string designation = appUser.Designation.ToString();
                var result = _userManager.AddToRole(appUser.Id, designation);
                return GetEmployeeById(appUser.Id);
           
         }
        public EmployeeViewModel UpdateEmployee(EmployeeViewModel employee)
        {

            var user = _userManager.FindById(employee.Id);
            string previousDesignation = user.Designation.ToString();
            user.CasualLeaveAllowed = CalculateAllowedLeaves(employee.DateOfJoining).CasualLeave;
            user.SickLeaveAllowed = CalculateAllowedLeaves(employee.DateOfJoining).SickLeave;
            user.Name = employee.Name;
            user.Designation = employee.Designation;
            user.Email = employee.Email;
            user.PhoneNumber = employee.PhoneNumber;
            user.DateOfJoining = employee.DateOfJoining;
            _userManager.RemoveFromRole(employee.Id, previousDesignation);
            string designation = user.Designation.ToString();
            _userManager.AddToRole(employee.Id, designation);
            var result = _userManager.Update(user);
            return GetEmployeeById(employee.Id);

        }
        //public void DeleteEmployee(string employeeId)
        //{
        //    var employee = _userManager.FindById(employeeId);

        //    var employeeList = _teamDataRepository.Fetch(x => x.EmployeeId == employeeId).ToList();
        //    employeeList.Reverse();

        //    foreach(var item in employeeList)
        //    {
        //        var id = item.Id;
        //       _teamDataRepository.Delete(id);

        //    }
           
        //    _userManager.Delete(employee);
        //}

        public void DeleteEmployee(string employeeId)
        {
            var employee = _userManager.FindById(employeeId);

            //var employeeList = _teamDataRepository.Fetch(x => x.EmployeeId == employeeId).ToList();
            //employeeList.Reverse();

            //foreach (var item in employeeList)
            //{
            //    var id = item.Id;
            //    _teamDataRepository.Delete(id);

            //}
             
            //_userManager.Delete(employee);

            employee.IsSoftDeleted = true;
            _userManager.Update(employee);
        }



        public IdentityResult ChangePassword(ChangePasswordAC changePassword)
        {
             IdentityResult result= new IdentityResult() ;
             
            if (changePassword.ConfirmPassword == changePassword.NewPassword)
            {
                result = _userManager.ChangePassword(changePassword.EmployeeId, changePassword.OldPassword, changePassword.NewPassword);

            }
            return (result);

        }

        /// <summary>
        /// this is used to calculate the allowed leaves to an employee from there dateOfJoining
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public LeaveCalculator CalculateAllowedLeaves(DateTime dateTime)
        {
            double casualAllowed = 0;
            double sickAllowed = 0;
            var day = dateTime.Day;
            var month = dateTime.Month;
            var year = dateTime.Year;
            var casual = _leaveAllowedDataRepository.FirstOrDefault(x => x.LeaveType == LMS.DomainModel.Models.Type.Casual);
            double casualAllow = casual.AllowedDays;
            var sick = _leaveAllowedDataRepository.FirstOrDefault(x => x.LeaveType == LMS.DomainModel.Models.Type.Sick);
            double sickAllow = sick.AllowedDays;
            if (year - DateTime.Now.Year>365)
            {
                month = 4;
                day = 1;
            }
            if (month >= 4)
            {
                if (day <= 15)
                {
                    casualAllowed = (casualAllow / 12) * (12 - (month - 4));
                    sickAllowed = (sickAllow / 12) * (12 - (month - 4));
                }
                else
                {
                    casualAllowed = (casualAllow / 12) * (12 - (month - 3));
                    sickAllowed = (sickAllow / 12) * (12 - (month - 3));
                }
            }
            else
            {
                if (day <= 15)
                {
                    casualAllowed = (casualAllow / 12) * (12 - (month + 8));
                    sickAllowed = (sickAllow / 12) * (12 - (month + 8));
                }
                else
                {
                    casualAllowed = (casualAllow / 12) * (12 - (month + 9));
                    sickAllowed = (sickAllow / 12) * (12 - (month + 9));
                }
            }
            LeaveCalculator calculate = new LeaveCalculator
            {
                CasualLeave = Convert.ToInt32(casualAllowed),
                SickLeave = Convert.ToInt32(sickAllowed)
            };
            return calculate;
        }
    }
}
