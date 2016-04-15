using Autofac;
using LMS.DomainModel.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DomainModel.Migrations;

namespace LMS.Web.App_Start
{
    public class DatabaseConfig
    {
        public static void Initialize(IComponentContext componentContext)
        {
            Database.SetInitializer<ApplicationDbContext>(new MigrateDatabaseToLatestVersion<ApplicationDbContext, LMS.DomainModel.Migrations.Configuration>());

            using (var dbContext = componentContext.Resolve<DbContext>())
            {
                dbContext.Database.Initialize(false);
            }
        }
    }
}
