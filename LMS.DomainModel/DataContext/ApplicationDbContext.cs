using LMS.DomainModel.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
        public DbSet<Team> Teams { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveStatus> LeaveStatus { get; set; }
        public DbSet<HolidayType> HolidayTypes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<LeaveAllowed> LeaveAlloweds { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasRequired(t => t.Employee)
                .WithMany(e => e.TeamAsEmployee)
                .HasForeignKey(t => t.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasRequired(t => t.TeamLeader)
                .WithMany(e => e.TeamAsTL)
                .HasForeignKey(t => t.TeamLeaderId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<LeaveStatus>()
                .HasRequired(e => e.Employee)
                .WithMany(s => s.LeaveStatusOfEmployee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<LeaveStatus>()
                .HasRequired(t => t.TeamLeader)
                .WithMany(s => s.LeaveStatusCheckedByTeamLeader)
                .HasForeignKey(t => t.TeamLeaderId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
