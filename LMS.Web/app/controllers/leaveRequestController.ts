/// <reference path="../../scripts/typings/tsd.d.ts" />
/// <reference path="../services/leaverequestservice.ts" />
 
interface ILeaveRequestControllerScope extends ng.IScope {
    allLeaveRequest: any;
    allLeaveRequestPending: any;
    leaveRequestForEmployee: any;
    leaveRequestForEmployeeViaStatus: any;
    leaveRequestApply: any;
    leaveRequestApprove: any;
    leaveRequestReject: any;
    leaveRequestCancel: any;
    allLeaveRequestTeam: any;
    allLeaveRequestTeamViaStatus: any;
    teamCalendar: any;
    applySickLeave: any;
    updateSickLeave: any;
    approveModal: any;
    rejectModal: any;
    dialogInstance: any;
    leaveDetails: any;
    leaveRequestByTypeSick: any;
    leaveRequestByTypeCasual: any;
    leaveRequestByTypeCompensation: any;
    leaveById: any;
    getEmployeeByIdDataFromService: any;
    leaveRequestUnderTeamLeaderPending: any;
    leave: any;
    cancelModal: any;
    sickLeaveApplyModal: any;
    sickLeavePending: any;
    sickLeaveApproveModal: any;
    sickLeaveRejectModal: any;
    sickLeaveRequestApprove: any;
    sickLeaveRequestReject: any;
    compensationLeaveHistoryAdmin: any;
    compensationLeaveRequestApprove: any;
    compensationLeaveRequestReject: any;
    compensationLeaveApproveModal: any;
    compensationLeaveRejectModal: any;
    escalateModal: any;
    leaveRequestEscalate: any;
    cancelLeaveRequestModal: any;
    onCancel: any;
    onFileSelect: any;
    currentDate: any;
    loading: any;
    leaveApplyLoaderModal: any;
    minDate: any;
    selectedFile: any;
    query: any;
    role: any;
    sickLeaveLoaderModal: any;
}

class leaveRequestController {
    static $inject: string[] = ["$scope", "leaveRequestService", "$routeParams", "$rootScope", "$location", "$mdDialog", "$mdToast", "Upload", "$http", "homeService"];
    constructor(private $scope: ILeaveRequestControllerScope, private leaveRequestService: IleaveRequestService, private $routeParams, private $rootScope, private $location, private $mdDialog, private $mdToast, private Upload, private $http, private homeService: IhomeService) {
        $scope.allLeaveRequest = () => this.allLeaveRequest();
        $scope.allLeaveRequestPending = () => this.allLeaveRequestPending();
        $scope.leaveRequestForEmployee = () => this.leaveRequestForEmployee();
        $scope.leaveRequestForEmployeeViaStatus = () => this.leaveRequestForEmployeeViaStatus(this.$routeParams.Id, this.$routeParams.status);
        $scope.leaveRequestApply = () => this.leaveRequestApply();
        $scope.leaveRequestApprove = () => this.leaveRequestApprove();
        $scope.leaveRequestReject = () => this.leaveRequestReject();
        $scope.leaveRequestCancel = () => this.leaveRequestCancel();
        $scope.allLeaveRequestTeam = () => this.allLeaveRequestTeam();
        $scope.allLeaveRequestTeamViaStatus = () => this.allLeaveRequestTeamViaStatus(this.$routeParams.status);
        $scope.teamCalendar = () => this.teamCalendar($routeParams.Id);
        $scope.applySickLeave = () => this.applySickLeave();
        $scope.updateSickLeave = () => this.updateSickLeave();
        $rootScope.LeaveRequestCondition = LeaveRequestCondition;
        $rootScope.LeaveRequestType = LeaveRequestType;
        $rootScope.LeaveRequestUnit = LeaveRequestUnit;
        $rootScope.EmployeeDesignation = EmployeeDesignation;
        $scope.leaveDetails = () => this.leaveDetails();
        $scope.leaveRequestByTypeSick = () => this.leaveRequestByTypeSick();
        $scope.leaveRequestByTypeCasual = () => this.leaveRequestByTypeCasual();
        $scope.leaveRequestByTypeCompensation = () => this.leaveRequestByTypeCompensation();
        $scope.leaveById = () => this.leaveById();
        $scope.getEmployeeByIdDataFromService = () => this.getEmployeeByIdDataFromService();
        $scope.leaveRequestUnderTeamLeaderPending = () => this.leaveRequestUnderTeamLeaderPending();
        $scope.approveModal = (request) => this.approveModal(request);
        $scope.rejectModal = (request) => this.rejectModal(request);
        $scope.cancelModal = () => this.cancelModal();
        $scope.sickLeaveApplyModal = (request) => this.sickLeaveApplyModal(request);
        $scope.sickLeavePending = () => this.sickLeavePending();
        $scope.sickLeaveApproveModal = (request) => this.sickLeaveApproveModal(request);
        $scope.sickLeaveRejectModal = (request) => this.sickLeaveRejectModal(request);
        $scope.sickLeaveRequestApprove = () => this.sickLeaveRequestApprove();
        $scope.sickLeaveRequestReject = () => this.sickLeaveRequestReject();
        $scope.compensationLeaveHistoryAdmin = () => this.compensationLeaveHistoryAdmin();
        $scope.compensationLeaveRequestApprove = () => this.compensationLeaveRequestApprove();
        $scope.compensationLeaveRequestReject = () => this.compensationLeaveRequestReject();
        $scope.compensationLeaveApproveModal = (request) => this.compensationLeaveApproveModal(request);
        $scope.compensationLeaveRejectModal = (request) => this.compensationLeaveRejectModal(request);
        $scope.escalateModal = (leave) => this.escalateModal(leave);
        $scope.leaveRequestEscalate = () => this.leaveRequestEscalate();
        $scope.cancelLeaveRequestModal = (request) => this.cancelLeaveRequestModal(request);
        $scope.onCancel = () => this.onCancel();
        $scope.onFileSelect = ($file) => this.onFileSelect($file);
        this.$scope.currentDate = new Date();
        $scope.leaveApplyLoaderModal = (request) => this.leaveApplyLoaderModal(request);
        $scope.minDate = new Date();
        $scope.query = {
            order: 'name',
            limit: 5,
            page: 1
        };

        var role = this.homeService.getCurrentUserRole().then((result) => {
            this.$scope.role = result.data;
        });
        this.$scope.sickLeaveLoaderModal = (request) => this.sickLeaveLoaderModal(request);
    }
    private leaveApplyLoaderModal(request) {
        this.$scope.leave = request;
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/leaveApplyLoaderModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }

    private sickLeaveLoaderModal(request) {
        this.$scope.leave = request;
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/sickLeaveLoader.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
    private onCancel() {
        this.$location.path('/');
    }
    private onFileSelect($file) {
        this.$scope.selectedFile = $file[0];
    }
    private allLeaveRequest() {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.allLeaveRequest();
        promise.then((data) => {
            for (var i = 0; i < data.data.length; i++) {
                data.data[i].StartDate = new Date(data.data[i].StartDate);
                data.data[i].EndDate = new Date(data.data[i].EndDate);
                data.data[i].CreatedOn = new Date(data.data[i].CreatedOn);
            }
            this.$scope.loading = false;
            this.$scope.allLeaveRequest = data.data;
        })
    }
    private leaveRequestForEmployee() {
        this.$scope.loading = true;
        var employeeId = this.$routeParams.Id;
        var promise = this.leaveRequestService.leaveRequestForEmployee(employeeId);
        promise.then((data) => {
            data.data.StartDate = new Date(data.data.StartDate);
            data.data.EndDate = new Date(data.data.EndDate);
            data.data.CreatedOn = new Date(data.data.CreatedOn);
            this.$scope.leaveRequestForEmployee = data.data;
            this.$scope.loading = false;
        })
    }
    private allLeaveRequestPending() {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.allLeaveRequestPending();
        promise.then((data) => {
            for (var i = 0; i < data.data.length; i++) {
                data.data[i].StartDate = new Date(data.data[i].StartDate);
                data.data[i].EndDate = new Date(data.data[i].EndDate);
                data.data[i].CreatedOn = new Date(data.data[i].CreatedOn);
            }
            this.$scope.loading = false;
            this.$scope.allLeaveRequestPending = data.data;
        })
    }
    private leaveRequestForEmployeeViaStatus(employeeId, status) {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.leaveRequestForEmployeeViaStatus(employeeId, status);
        promise.then((data) => {
            this.$scope.loading = false;
            this.$scope.leaveRequestForEmployeeViaStatus = data;
        })
    }
    private leaveRequestApply() {
        this.$scope.loading = true;
        var request = this.$scope.leave;
        var promise = this.leaveRequestService.leaveRequestApply(request);
        promise.then((data) => {
            this.$scope.leaveRequestApply = data;
            this.$location.path("/LeaveHistory");
            this.$scope.loading = false;
            this.$mdDialog.hide();
            this.$mdToast.show(this.$mdToast.simple().textContent("Succesfully Leave Applied").position("bottom right"));
        })
    }
    private leaveRequestApprove() {

        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var request = this.$scope.leave;
        request.Condition = "Approved";
        request.leaveRequestId = request.Id;
        var promise = this.leaveRequestService.leaveRequestResult(request);
        promise.then((data) => {
            if (this.$scope.role == "Admin") {
                this.$location.path("/LeaveRequest/All");
            }
            if (this.$scope.role == "TeamLeader") {
                this.$location.path("/team/LeaveRequest");
            }
            this.$scope.loading = false;
            this.$scope.leaveRequestApprove = data;
            this.$mdToast.show(this.$mdToast.simple().textContent("Leave Approved"));
        })
    }
    private leaveRequestReject() {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var request = this.$scope.leave;
        request.Condition = "Rejected";
        request.leaveRequestId = request.Id;
        var promise = this.leaveRequestService.leaveRequestResult(request);
        promise.then((data) => {
            if (this.$scope.role == "Admin") {
                this.$location.path("/LeaveRequest/All");
            }
            if (this.$scope.role == "TeamLeader") {
                this.$location.path("/team/LeaveRequest");
            }
            this.$scope.loading = false;
            this.$scope.leaveRequestReject = data;
            this.$mdToast.show(this.$mdToast.simple().textContent("Leave Rejected"));
        })
    }
    private leaveRequestEscalate() {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var request = this.$scope.leave;
        request.Condition = "Escalated";
        request.leaveRequestId = request.Id;
        var promise = this.leaveRequestService.leaveRequestResult(request);
        promise.then((data) => {
            if (this.$scope.role == "Admin") {
                this.$location.path("/LeaveRequest/All");
            }
            if (this.$scope.role == "TeamLeader") {
                this.$location.path("/team/LeaveRequest");
            }
            this.$scope.loading = false;
            this.$scope.leaveRequestEscalate = data;
            this.$mdToast.show(this.$mdToast.simple().textContent("Leave Escalated"));
        })
    }
    private approveModal(request) {
        this.$scope.leave = request;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/approveModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
    private rejectModal(request) {
        this.$scope.leave = request;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/rejectModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
    private escalateModal(request) {
        this.$scope.leave = request;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/escalateModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
    private sickLeaveApproveModal(request) {
        this.$scope.leave = request;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/sickLeaveApproveModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
    private sickLeaveRejectModal(request) {
        this.$scope.leave = request;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/sickLeaveRejectModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
    private cancelModal() {
        this.$scope.dialogInstance = this.$mdDialog.hide();
    }
    private sickLeaveRequestApprove() {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var request = this.$scope.leave;
        request.Condition = "Approved";
        request.leaveRequestId = request.Id;
        var promise = this.leaveRequestService.sickLeaveRequestResult(request);
        promise.then((data) => {
            if (this.$scope.role == "Admin") {
                this.$location.path("/LeaveRequest/All");
            }
            if (this.$scope.role == "TeamLeader") {
                this.$location.path("/team/LeaveRequest");
            }
            this.$scope.loading = false;
            this.$scope.sickLeaveRequestApprove = data;
        })
    }
    private sickLeaveRequestReject() {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var request = this.$scope.leave;
        request.Condition = "Rejected";
        request.leaveRequestId = request.Id;
        var promise = this.leaveRequestService.sickLeaveRequestResult(request);
        promise.then((data) => {
            if (this.$scope.role == "Admin") {
                this.$location.path("/LeaveRequest/All");
            }
            if (this.$scope.role == "TeamLeader") {
                this.$location.path("/team/LeaveRequest");
            }
            this.$scope.loading = false;
            this.$scope.leaveRequestApprove = data;
        })
    }
    private compensationLeaveApproveModal(request) {
        this.$scope.leave = request;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/compensationApproveModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
    private compensationLeaveRejectModal(request) {
        this.$scope.leave = request;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/compensationRejectModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
    private compensationLeaveRequestApprove() {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var request = this.$scope.leave;
        request.Condition = "Approved";
        request.leaveRequestId = request.Id;
        var promise = this.leaveRequestService.compensationLeaveResult(request);
        promise.then((data) => {
            if (this.$scope.role == "Admin") {
                this.$location.path("/LeaveRequest/All");
            }
            if (this.$scope.role == "TeamLeader") {
                this.$location.path("/team/LeaveRequest");
            }
            this.$scope.loading = false;
            this.$scope.leaveRequestApprove = data;
        })
    }
    private compensationLeaveRequestReject() {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var request = this.$scope.leave;
        request.Condition = "Rejected";
        request.leaveRequestId = request.Id;
        var promise = this.leaveRequestService.compensationLeaveResult(request);
        promise.then((data) => {
            if (this.$scope.role == "Admin") {
                this.$location.path("/LeaveRequest/All");
            }
            if (this.$scope.role == "TeamLeader") {
                this.$location.path("/team/LeaveRequest");
            }
            this.$scope.loading = false;
            this.$scope.leaveRequestApprove = data;
        })
    }
    private sickLeaveApplyModal(request) {
        this.$scope.getEmployeeByIdDataFromService = request;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/sickLeaveApply.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
    private leaveRequestCancel() {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var Id = this.$routeParams.Id;
        var promise = this.leaveRequestService.leaveRequestCancel(Id);
        promise.then((data) => {
            this.$scope.loading = false;
            this.$scope.leaveRequestCancel = data;
        })
    }
    private allLeaveRequestTeam() {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.allLeaveRequestTeam();
        promise.then((data) => {
            for (var i = 0; i < data.data.length; i++) {
                data.data[i].StartDate = new Date(data.data[i].StartDate);
                data.data[i].EndDate = new Date(data.data[i].EndDate);
                data.data[i].CreatedOn = new Date(data.data[i].CreatedOn);
            }
            this.$scope.loading = false;
            this.$scope.allLeaveRequestTeam = data.data;
        })
    }
    private allLeaveRequestTeamViaStatus(status) {
        var promise = this.leaveRequestService.allLeaveRequestTeamViaStatus(status);
        promise.then((data) => {
            this.$scope.allLeaveRequestTeamViaStatus = data;
        })
    }
    private teamCalendar(projectId) {
        var promise = this.leaveRequestService.teamCalendar(projectId);
        promise.then((data) => {
            this.$scope.teamCalendar = data;
        })
    }
    private applySickLeave() {
        this.$scope.loading = false;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var request = this.$scope.getEmployeeByIdDataFromService;
        request.EmployeeId = request.Id;
        request.Id = 0;
        var promise = this.leaveRequestService.applySickLeave(request);
        promise.then((data) => {
            this.$scope.loading = false;
            this.$scope.applySickLeave = data;
        })
    }
    private updateSickLeaveRequest(request) {
        data: request,
        this.$http({
            method: 'PUT',
            url: 'api/SickLeaveUpdate',
            //headers: { 'Content-Type': false },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("model", angular.toJson(data.model));
                for (var i = 0; i < data.files; i++) {
                    formData.append("file" + i, data.files[i]);
                }
                return formData;
            },
            data: { model: request, files: this.$scope.selectedFile }
        }).then((data) => {
            this.$scope.loading = false;
            this.$scope.updateSickLeave = data;
        })
    }
    private updateSickLeave() {
        var request = this.$scope.leave;
        this.$scope.loading = true;
        this.Upload.upload({
            url: "api/SickLeaveUpdate",
            method: "PUT",
            data: { data: JSON.stringify(request) },
            file: this.$scope.selectedFile
        }).then((data) => {
            this.$location.path("/LeaveHistory");
            this.$scope.loading = false;
            this.$mdDialog.hide();
            this.$scope.updateSickLeave = data;
        })
    }
    private leaveDetails() {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.leaveDetails();
        promise.then((data) => {
            this.$scope.loading = false;
            this.$scope.leaveDetails = data.data;
        })
    }
    private leaveRequestByTypeSick() {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.leaveRequestByType("Sick");
        promise.then((data) => {
            for (var i = 0; i < data.data.length; i++) {
                data.data[i].StartDate = new Date(data.data[i].StartDate);
                data.data[i].EndDate = new Date(data.data[i].EndDate);
                data.data[i].CreatedOn = new Date(data.data[i].CreatedOn);
            }
            this.$scope.loading = false;
            this.$scope.leaveRequestByTypeSick = data.data;
        })
    }
    private leaveRequestByTypeCasual() {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.leaveRequestByType("Casual");
        promise.then((data) => {
            for (var i = 0; i < data.data.length; i++) {
                data.data[i].StartDate = new Date(data.data[i].StartDate);
                data.data[i].EndDate = new Date(data.data[i].EndDate);
                data.data[i].CreatedOn = new Date(data.data[i].CreatedOn);
            }
            this.$scope.loading = false;
            this.$scope.leaveRequestByTypeCasual = data.data;
        })
    }
    private leaveRequestByTypeCompensation() {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.leaveRequestByType("Compensation");
        promise.then((data) => {
            for (var i = 0; i < data.data.length; i++) {
                data.data[i].StartDate = new Date(data.data[i].StartDate);
                data.data[i].EndDate = new Date(data.data[i].EndDate);
                data.data[i].CreatedOn = new Date(data.data[i].CreatedOn);
            }
            this.$scope.loading = false;
            this.$scope.leaveRequestByTypeCompensation = data.data;
        })
    }
    private leaveById() {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.leaveById(this.$routeParams.Id);
        promise.then((data) => {
            data.data.EndDate = new Date(data.data.EndDate);
            this.$scope.loading = false;
            this.$scope.leaveById = data.data;
        })
    }
    private getEmployeeByIdDataFromService() {
        this.$scope.loading = true;
        var Id = this.$routeParams.Id;
        var promise = this.leaveRequestService.getEmployeeById(Id);
        promise.then((data) => {
            this.$scope.loading = false;
            this.$scope.getEmployeeByIdDataFromService = data.data;
        })
    }
    private leaveRequestUnderTeamLeaderPending() {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.leaveRequestUnderTeamLeaderPending();
        promise.then((data) => {
            for (var i = 0; i < data.data.length; i++) {
                data.data[i].StartDate = new Date(data.data[i].StartDate);
                data.data[i].EndDate = new Date(data.data[i].EndDate);
            }
            this.$scope.loading = false;
            this.$scope.leaveRequestUnderTeamLeaderPending = data.data;
        })
    }
    private sickLeavePending() {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.sickLeavePending();
        promise.then((data) => {
            for (var i = 0; i < data.data.length; i++) {
                data.data[i].StartDate = new Date(data.data[i].StartDate);
                data.data[i].EndDate = new Date(data.data[i].EndDate);
                data.data[i].CreatedOn = new Date(data.data[i].CreatedOn);
            }
            this.$scope.loading = false;
            this.$scope.sickLeavePending = data.data;
        })
    }
    private compensationLeaveHistoryAdmin() {
        this.$scope.loading = true;
        var promise = this.leaveRequestService.compensationLeaveHistoryAdmin();
        promise.then((data) => {
            for (var i = 0; i < data.data.length; i++) {
                data.data[i].StartDate = new Date(data.data[i].StartDate);
                data.data[i].EndDate = new Date(data.data[i].EndDate);
                data.data[i].CreatedOn = new Date(data.data[i].CreatedOn);
            }
            this.$scope.loading = false;
            this.$scope.compensationLeaveHistoryAdmin = data.data;
        })
    }
    private cancelLeaveRequestModal(request) {
        this.$routeParams.Id = request;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/cancelLeaveRequestModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
}

angular.module("app").controller("leaveRequestController", leaveRequestController);
