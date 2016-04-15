using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using LMS.Core.Controllers;
using LMS.DomainModel.DataContext;
using LMS.DomainModel.DataRepository;
using LMS.DomainModel.Identity;
using LMS.DomainModel.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using LMS.Repository.Modules.LeaveReview;
using LMS.Repository.Modules;
using LMS.Repository.Modules.Holiday;
using LMS.Repository.Modules.LeaveStatusRepository;
using LMS.Repository.Modules.LeaveDefinition;
using LMS.Repository.Modules.EmployeeRepositoryFolder;
using LMS.Repository.Modules.ProjectRepositoryFolder;
using LMS.Util.Email;




namespace LMS.Web.App_Start
{
    public class AutofacConfig
    {
        public static IComponentContext RegisterDependancies(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // REGISTER DEPENDENCIES
            builder.RegisterType<ApplicationDbContext>().As<DbContext>();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>();
            builder.RegisterType<ApplicationUserManager>().AsSelf();

            builder.RegisterType<ApplicationRoleStore>().As<IRoleStore<IdentityRole, string>>();
            builder.RegisterType<ApplicationRoleManager>().AsSelf();

            builder.RegisterType<ApplicationSignInManager>().AsSelf();
            
            
            
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication);
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider());

            // register mvc controllers
            builder.RegisterControllers(typeof(HomeController).Assembly);
            //builder.RegisterControllers(typeof(AccountController).Assembly);


            // register webapi controller
            builder.RegisterApiControllers(typeof(LeaveRequestController).Assembly);
            

            // register repositories
            builder.RegisterGeneric(typeof(DataRepository<>)).As(typeof(IDataRepository<>));
            builder.RegisterType<LeaveRequestRepository>().As<ILeaveRequestRepository>();
            builder.RegisterType<LeaveReviewRepository>().As<ILeaveReviewRepository>();
            builder.RegisterType<HolidayRepository>().As<IHolidayRepository>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            builder.RegisterType<LeaveAllowedRepository>().As<ILeaveAllowedRepository>();
            builder.RegisterType<LeaveStatusRepository>().As<ILeaveStatusRepository>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<EmailUtil>().As<IEmailUtil>();

            var container = builder.Build();

            // replace mvc dependancy resolver with autofac
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // replace webapi dependancy resolver with autofac
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }

    }
}
