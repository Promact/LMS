using LMS.DomainModel.ApplicationClasses;
using LMS.DomainModel.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LMS.Repository.Modules.EmployeeRepositoryFolder
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeViewModel> GetAllEmployees();
        EmployeeViewModel GetEmployeeById(string employeeId);
        EmployeeViewModel AddEmployee(EmployeeViewModel employee);
        EmployeeViewModel UpdateEmployee(EmployeeViewModel employee);
        void DeleteEmployee(string employeeId);
        IdentityResult ChangePassword(ChangePasswordAC changePassword);
        LeaveCalculator CalculateAllowedLeaves(DateTime dateTime);
    }
}
