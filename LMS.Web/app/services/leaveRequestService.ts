/// <reference path="../../scripts/typings/tsd.d.ts" />
 
interface IleaveRequestService {
    allLeaveRequest: () => ng.IPromise<any>;
    allLeaveRequestPending: () => ng.IPromise<any>;
    leaveRequestForEmployee: (employeeId: string) => ng.IPromise<any>;
    leaveRequestViaStatus: (status: string) => ng.IPromise<any>;
    leaveRequestForEmployeeViaStatus: (employeeId: string, status: string) => ng.IPromise<any>;
    leaveRequestApply: (leaveRequest: any) => ng.IPromise<any>;
    leaveRequestResult: (leaveRequest: any) => ng.IPromise<any>;
    leaveRequestCancel: (leaveRequestId: number) => ng.IPromise<any>;
    allLeaveRequestTeam: () => ng.IPromise<any>;
    allLeaveRequestTeamViaStatus: (status: string) => ng.IPromise<any>;
    teamCalendar: (projectId: number) => ng.IPromise<any>;
    applySickLeave: (leaveRequest: any) => ng.IPromise<any>;
    updateSickLeave: (leaveRequest: any) => ng.IPromise<any>;
    leaveDetails: () => ng.IPromise<any>;
    leaveRequestByType: (leaveType: any) => ng.IPromise<any>;
    leaveById: (leaveRequestId: number) => ng.IPromise<any>;
    getEmployeeById: (Id: string) => ng.IPromise<any>;
    leaveRequestUnderTeamLeaderPending: () => ng.IPromise<any>;
    sickLeavePending: () => ng.IPromise<any>;
    sickLeaveRequestResult: (leaveRequest: any) => ng.IPromise<any>;
    compensationLeaveHistoryAdmin: () => ng.IPromise<any>;
    compensationLeaveResult: (request:any) => ng.IPromise<any>;
}

class leaveRequestService implements IleaveRequestService {
    static $inject: string[] = ["$http", "$q"];
    constructor(private $http: ng.IHttpService) {
    }
    allLeaveRequest() {
        return this.$http.get('api/Leaves');
    }
    allLeaveRequestPending() {
        return this.$http.get('api/Leaves/Pending');
    }
    leaveRequestForEmployee(employeeId) {
        return this.$http.get('api/Employee/' + employeeId + '/Leaves');
    }
    leaveRequestViaStatus(status: string) {
        return this.$http.get('api/Leaves/' + status);
    }
    leaveRequestForEmployeeViaStatus(employeeId: string, status: string) {
        return this.$http.get(employeeId + '/EmployeeLeavesStatus/' + status);
    }
    leaveRequestApply(leaveRequest: any) {
        return this.$http.post("api/LeaveRequest", leaveRequest);
    }
    leaveRequestResult(leaveRequest: any) {
        return this.$http.put("api/LeaveRequest", leaveRequest);
    }
    leaveRequestCancel(leaveRequestId: number) {
        return this.$http.delete("api/LeaveRequest/" + leaveRequestId);
    }
    allLeaveRequestTeam() {
        return this.$http.get("api/TeamLeaves");
    }
    allLeaveRequestTeamViaStatus(status: string) {
        return this.$http.get("api/TeamLeaves/" + status);
    }
    teamCalendar(projectId: number) {
        return this.$http.get("api/TeamCalendar/" + projectId);
    }
    applySickLeave(leaveRequest: any) {
        return this.$http.post("api/SickLeaveRequest", leaveRequest);
    }
    updateSickLeave(leaveRequest: any) {
        return this.$http.put("api/SickLeaveUpdate", leaveRequest);
    }
    leaveDetails() {
        return this.$http.get("api/employeeLeaveDetails");
    }
    leaveRequestByType(leaveType) {
        return this.$http.get("api/LeaveRequestByType/" + leaveType);
    }
    leaveById(leaveRequestId) {
        return this.$http.get("api/Employee/" + leaveRequestId + "/Leaves");
    }
    getEmployeeById(Id) {
        return this.$http.get('api/Employee/' + Id);
    }
    leaveRequestUnderTeamLeaderPending() {
        return this.$http.get('api/Team/Pending');
    }
    sickLeavePending() {
        return this.$http.get('api/SickLeavePending');
    }
    sickLeaveRequestResult(leaveRequest) {
        return this.$http.put('api/SickLeaveStatus', leaveRequest);
    }
    compensationLeaveHistoryAdmin() {
        return this.$http.get('api/CompensationLeavePendingTL');
    }
    compensationLeaveResult(request) {
        return this.$http.put('api/CompensationLeaveStatus', request);
    }
}


angular.module("app").service("leaveRequestService", leaveRequestService);
