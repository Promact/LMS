using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public LMS.DomainModel.Models.Designation Designation { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }
        public double CasualLeaveAllowed { get; set; }
        public double SickLeaveAllowed { get; set; }

        public bool IsSoftDeleted { get; set; }



        public virtual ICollection<Team> TeamAsEmployee { get; set; }                       //many to  many with table Team
        public virtual ICollection<Team> TeamAsTL { get; set; }                             //many to  many with table Team

        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }                //many to  many with table LeaveRequest


        public virtual ICollection<LeaveStatus> LeaveStatusOfEmployee { get; set; }
        public virtual ICollection<LeaveStatus> LeaveStatusCheckedByTeamLeader { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
