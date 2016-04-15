
interface IleaveAllowedControllerScope extends ng.IScope {
    leaveList: Array<any>;
    leaveTypes: Array<any>;
    leave: any;
    newLeave: Object;

    allLeaveList: Function;
    getLeaveById: Function;
    addLeave: Function;
    editLeave: Function;
    dialogInstance: any;
    leaveAddModal: any;
    leaveEditModal: any;
    cancelModal: any;
    loading: any;
}

class leaveAllowedController {
    static $inject: string[] = ["$scope", "leaveService", "$routeParams", "$location", "$rootScope", "$mdDialog", "$mdToast"];
    public Id: number;

    constructor(private $scope: IleaveAllowedControllerScope, private leaveService: IleaveService, private $routeParams, private $location, private $rootScope, private $mdDialog, private $mdToast) {
        this.$scope.allLeaveList = () => this.allLeaveList();
        this.$scope.addLeave = (leave) => this.addLeave(leave);
        this.$scope.getLeaveById = () => this.getLeaveById();
        this.$scope.editLeave = (leave) => this.editLeave(leave);
        $rootScope.TypeOfLeave = LeaveRequestType;
        $scope.leaveTypes = [{ name: "Sick", value: 0 }, { name: "Compensation", value: 1 }, { name: "Casual", value: 2 }, { name: "Paternity", value: 3 }, { name: "Maternity", value: 4 }]
        $scope.leaveAddModal = () => this.leaveAddModal();
        $scope.cancelModal = () => this.cancelModal();
        $scope.leaveEditModal = (leaveId) => this.leaveEditModal(leaveId);
    }

    private getLeaveById() {
        this.Id = this.$routeParams.Id;
        var promise = this.leaveService.getLeaveById(this.Id);
        promise.then((data) => {
            this.$scope.leave = data.data;
            this.$scope.leave.LeaveType = this.$scope.leaveTypes[this.$scope.leave.LeaveType];
        })
    }

    private allLeaveList() {
        this.$scope.loading = true;
        var promise = this.leaveService.getLeaveList();
        promise.then((data) => {
            this.$scope.loading = false;
            this.$scope.leaveList = data.data;
        })
            .catch((Error) => {
                this.$scope.loading = false;
                this.$mdToast.show(this.$mdToast.simple().textContent('something went wrong'));
            })
    }

    private addLeave(newLeave) {

        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var promise = this.leaveService.addLeave(newLeave);
        promise.then((data) => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Leave Added'));
            this.$scope.newLeave = null;
            this.$scope.allLeaveList();
        })
            .catch((Error) => {
                this.$scope.loading = false;
                this.$mdToast.show(this.$mdToast.simple().textContent('something went wrong'));
            })
    }


    private editLeave(leave) {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        leave.LeaveType = leave.LeaveType.name;
        this.Id = this.$routeParams.Id;
        var promise = this.leaveService.editLeave(leave);
        promise.then((data) => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Leave Edited'));
            this.$scope.allLeaveList();
        })
            .catch((Error) => {
                this.$scope.loading = false;
                this.$mdToast.show(this.$mdToast.simple().textContent('something went wrong'));
            })
    }
    private leaveAddModal() {
        this.$scope.newLeave = null;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/leaveAddModal.html',
            scope: this.$scope,
            preserveScope: true,
            clickOutsideToClose: false,
            escapeToClose: true
        })
    }
    private cancelModal() {
        this.$scope.dialogInstance = this.$mdDialog.hide();
    }
    private leaveEditModal(leaveId) {
        this.$routeParams.Id = leaveId;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/leaveEditModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
}

angular.module("app").controller("leaveAllowedController", leaveAllowedController);