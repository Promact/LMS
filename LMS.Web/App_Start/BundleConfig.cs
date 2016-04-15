using System.Web;
using System.Web.Optimization;

namespace LMS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/dist/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      
                      "~/Scripts/angular-material/angular-material.css",
                      "~/Scripts/angular-material-data-table/dist/md-data-table.min.css",
                      "~/Scripts/fullcalendar/dist/fullcalendar.css",
                      "~/Scripts/ng-tags-input/ng-tags-input.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                    "~/Scripts/jquery/dist/jquery.js",
                    "~/Scripts/moment/moment.js",
                    "~/Scripts/angular/angular.js",
                    "~/Scripts/angular-ui-calendar/src/calendar.js",
                    "~/Scripts/fullcalendar/dist/fullcalendar.js",
                    "~/Scripts/fullcalendar/dist/gcal.js",
                    "~/Scripts/angular-aria/angular-aria.js",
                    "~/Scripts/angular-animate/angular-animate.js",
                    "~/Scripts/angular-messages/angular-messages.js",
                    "~/Scripts/angular-material/angular-material.js",
                    "~/Scripts/angular-route/angular-route.js",
                    "~/Scripts/ng-tags-input/ng-tags-input.js",
                    "~/Scripts/angular-material-data-table/dist/md-data-table.min.js",
                    "~/Scripts/angular-material/modules/js/toast/toast.min.js",
                    "~/Scripts/ng-file-upload/ng-file-upload.js",
                    "~/Scripts/angularUtils-pagination/dirPagination.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                    "~/app/app.js",
                    "~/app/Models/leaveRequestType.js",
                    "~/app/Models/LeaveRequestCondition.js",
                    "~/app/Models/EmployeeDesignation.js",
                    "~/app/Models/LeaveRequestUnit.js",
                    "~/app/Models/TypeOfHoliday.js",
                    "~/app/Models/TeamProjectAC.js",
                    "~/app/Models/Designation.js",
                    "~/app/Models/EmployeeViewModel.js",
                    "~/app/services/employeeService.js",
                    "~/app/services/holidayService.js",
                    "~/app/services/leaveService.js",
                    "~/app/services/leaveRequestService.js",
                    "~/app/services/projectService.js",
                    "~/app/services/homeService.js",            //
                    "~/app/controllers/projectController.js",
                    "~/app/controllers/employeeController.js",
                    "~/app/controllers/leaveRequestController.js",
                    "~/app/controllers/employeeController.js",
                    "~/app/controllers/holidayController.js",
                    "~/app/controllers/leaveAllowedController.js",
                    "~/app/controllers/homeController.js",      //
                    "~/app/directives/activeClass.js"
                ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
