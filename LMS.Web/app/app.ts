angular.module("app", [
    // Angular modules 
    "ngRoute" ,// routing
    "ngTagsInput",
   
    // Custom modules 

    // 3rd Party Modules
    'ngMaterial', 'md.data.table', 'ui.calendar','ngFileUpload',
    'angularUtils.directives.dirPagination'
]);

angular.module("app").config(['$routeProvider', ($routeProvider: ng.route.IRouteProvider) => {
    $routeProvider.
        when("/", {
            templateUrl: '/templates/home.html',
            controller: 'homeController'
        }).
        when("/employee/create", {
            templateUrl: '/templates/employeeCreate.html',
            controller: 'employeeController'
        }).
        when("/employee/edit/:Id", {
            templateUrl: '/templates/employeeEdit.html',
            controller: 'employeeController'
        }).
        when("/employee/delete/:Id", {
            templateUrl: '/templates/employeeDelete.html',
            controller: 'employeeController'
        }).
        when("/employee/editProfile/:Id", {
            templateUrl: '/templates/editProfile.html',
            controller: 'employeeController'
        }).

        when("/employee/allEmployees", {
            templateUrl: '/templates/allEmployees.html',
            controller: 'employeeController'
        }).
        when("/employeeById/:Id", {
            templateUrl: '/templates/employee.html',
            controller: 'employeeController'
        }).
        when("/employee/Details/:Id", {
            templateUrl: '/templates/employeeDetailsFromAdmin.html',
            controller: 'employeeController'
        }).
        when("/employee/changePassword/:Id", {
            templateUrl: '/templates/employeeChangePassword.html',
            controller: 'employeeController'
        }).
        when("/LeaveHistory", {
            templateUrl: '/templates/employeeLeaveHistory.html',
            controller: 'leaveRequestController'
        }).
        when("/employee/LeaveHistory/Sick", {
            templateUrl: '/templates/sickLeaveHistory.html',
            controller: 'leaveRequestController'
        }).
        when("/employee/LeaveHistory/Casual", {
            templateUrl: '/templates/casualLeaveHistory.html',
            controller: 'leaveRequestController'
        }).
        when("/employee/LeaveHistory/Compensation", {
            templateUrl: '/templates/compensationLeaveHistory.html',
            controller: 'leaveRequestController'
        }).
        when("/employee/LeaveHistory/Sick/Update/:Id", {
            templateUrl: '/templates/updateSickLeave.html',
            controller: 'leaveRequestController'
        }).
        when("/HolidayType",
            {
                templateUrl: '/templates/adminHolidayList.html',
                controller: 'holidayController'
            }).
        when("/Holidays",
            {
                templateUrl: '/templates/userHolidayList.html',
                controller: 'holidayController'
            }).
        when("/HolidayType/:Id/Details",
            {
                templateUrl: '/templates/holidayDetails.html',
                controller: 'holidayController'
            }).
        when("/HolidayType/Create",
            {
                templateUrl: '/templates/holidayAdd.html',
                controller: 'holidayController'
            }).
        when("/HolidayType/:Id/Edit",
            {
                templateUrl: '/templates/holidayEdit.html',
                controller: 'holidayController'
            }).
        when("/LeaveType",
            {
                templateUrl: '/templates/leaveList.html',
                controller: 'leaveAllowedController'
        }).
        when("/LeaveType/Create",
            {
                templateUrl: '/templates/leaveAdd.html',
                controller: 'leaveAllowedController'
        }).
        when("/LeaveType/:Id/Edit",
            {
                templateUrl: '/templates/leaveUpdate.html',
                controller: 'leaveAllowedController'
            }).
        when("/Project/DisplayTeam", {
            templateUrl: '/templates/teamProject.html',
            controller: 'projectController'

        }).
        when("/TeamCalendar", {
            templateUrl: '/templates/teamCalendar.html',
            controller: 'projectController'

        }).
       

        when("/employee/Project/UpdateTeam/:ProjectId", {
            templateUrl: '/templates/teamProjectUpdate.html',
            controller: 'projectController'
        }).
        when("/employee/Project/ViewTeamCalendar/:Id", {
            templateUrl: '/templates/teamCalendar.html',
            controller: 'projectController'
        }).
        when("/employee/Project/CreateEmployees", {
            templateUrl: '/templates/teamProjectAllEmployees.html',
            controller: 'projectController'
        }).
        when("/employee/Project/DetailsTeamProject/:ProjectId", {
            templateUrl: '/templates/ProjectDetails.html',
            controller: 'projectController'
        }).
        when("/LeaveRequest/All", {
            templateUrl: '/templates/allLeaveRequest.html',
            controller: 'leaveRequestController'
        }).
        when("/employeeLeaveRequest/:Id", {
            templateUrl: '/templates/employeesAllLeaveRequest.html',
            controller: 'leaveRequestController'
        }).

        when("/LeaveRequest/pending", {
            templateUrl: '/templates/pendingLeaveRequest.html',
            controller: 'leaveRequestController'
        }).
        when("/employee/ApplyLeaveRequest", {
            templateUrl: '/templates/employeeLeaveApply.html',
            controller: 'leaveRequestController'
        }).
        when("/team/LeaveRequest", {
            templateUrl: '/templates/teamLeaveHistory.html',
            controller: 'leaveRequestController'
        }).
        when("/employee/SickLeave/:Id", {
            templateUrl: '/templates/employeeSick.html',
            controller: 'leaveRequestController'
        }).
        when("/employeeLeaveDetails/:Id", {
            templateUrl: '/templates/employeeLeaveDetails.html',
            controller: 'leaveRequestController'
        }).
        when("/team/PendingLeaveRequest", {
            templateUrl: '/templates/teamPendingHistory.html',
            controller: 'leaveRequestController'
        }).
        when("/LeaveRequest/sick", {
            templateUrl: '/templates/sickLeavePending.html',
            controller: 'leaveRequestController'
        }).
        when("/LeaveRequest/compensation", {
            templateUrl: '/templates/compensationLeaveHistoryAdmin.html',
            controller: 'leaveRequestController'
        }).
        when("/team/compensationLeaveHistory", {
            templateUrl: '/templates/compensationLeaveHistoryTeamLeader.html',
            controller: 'leaveRequestController'
        }).
        when("/employeeSickLeaveRequest/:Id", {
            templateUrl: '/templates/sickLeaveDetail.html',
            controller: 'leaveRequestController'
        })
}]);


