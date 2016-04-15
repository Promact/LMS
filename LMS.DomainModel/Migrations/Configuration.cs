using LMS.DomainModel.DataContext;
using LMS.DomainModel.Identity;
using LMS.DomainModel.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using System.Web;
using System.Web.Mvc;

namespace LMS.DomainModel.Migrations
{
    public class Configuration : DbMigrationsConfiguration<LMS.DomainModel.DataContext.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LMS.DomainModel.DataContext.ApplicationDbContext context)
        {
            var manager = DependencyResolver.Current.GetService<ApplicationUserManager>();
            var roleManager = DependencyResolver.Current.GetService<ApplicationRoleManager>();

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "TeamLeader" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            if (manager.Users.Count() == 0)
            {
                var user = new ApplicationUser()
                {
                    Name = "Admin",
                    UserName = "admin@promactinfo.com",
                    Email = "admin@promactinfo.com",
                    DateOfJoining = DateTime.Now,
                    Designation = LMS.DomainModel.Models.Designation.Admin
                };

                manager.Create(user, "admin@123");

                var adminUser = manager.FindByName("admin@promactinfo.com");

                manager.AddToRoles(adminUser.Id, new string[] { "Admin" });
            }



            if (context.LeaveAlloweds.Count() == 0)
            {
                context.LeaveAlloweds.AddOrUpdate(l => l.Id,
                    new LeaveAllowed { Id = 1, LeaveType = DomainModel.Models.Type.Sick, AllowedDays = 7, CreatedOn = DateTime.Now },
                    new LeaveAllowed { Id = 2, LeaveType = DomainModel.Models.Type.Casual, AllowedDays = 14, CreatedOn = DateTime.Now }
                );
            }
        }
    }
}
