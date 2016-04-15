/// <reference path="../../scripts/typings/tsd.d.ts" />
/// <reference path="../services/employeeservice.ts" />

interface IemployeeControllerScope extends ng.IScope {
    getEmployeeDataFromService: Function;
    getEmployeeByIdDataFromService: Function;
    addEmployeeFromService: Function;
    updateEmployeeFromService: Function;
    deleteEmployeeFromService: Function;
    changePasswordFromService: Function;
    editProfile: Function;
    onCancel(): void;
    onBack(): void;
    employeeList: any;
    employee: Object;
    newEmployee: Object;
    designationTypes: Array<any>;
    allLeaveList: any;
    loading: any; 
}

class employeeController {
    static $inject: string[] = ["$scope", "$routeParams", "$location", "$mdToast",  "employeeService"];

    constructor(private $scope: IemployeeControllerScope, private $routeParams, private $location, private $mdToast, private employeeService: IemployeeService) {


        this.activate();

        $scope.getEmployeeDataFromService = () => this.getEmployeeDataFromService();

        $scope.getEmployeeByIdDataFromService = () => this.getEmployeeByIdDataFromService();

        $scope.addEmployeeFromService = (newEmployee) => this.addEmployeeFromService(newEmployee);

        $scope.updateEmployeeFromService = (updatedEmployee) => this.updateEmployeeFromService(updatedEmployee);

        $scope.deleteEmployeeFromService = () => this.deleteEmployeeFromService();

        $scope.changePasswordFromService = (changePassword) => this.changePasswordFromService(changePassword);

        $scope.editProfile = (editedProfile) => this.editProfile(editedProfile);

        $scope.onCancel = () => this.onCancel();

        $scope.onBack = () => this.onBack();

        $scope.designationTypes = [{ name: "User", value: 0 }, { name: "TeamLeader", value: 1 }]

        $scope.allLeaveList = () => this.allLeaveList();
    }

    activate() {


    }



    getEmployeeDataFromService() {
        this.$scope.loading = true;
        var promise = this.employeeService.getAllEmployees();
        promise.then((result) => {
            this.$scope.loading = false;
            this.$scope.employeeList = result.data;
        })
    }


    getEmployeeByIdDataFromService() {
        this.$scope.loading = true;
        var Id = this.$routeParams.Id;
        var promise = this.employeeService.getEmployeeById(Id);
        promise.then((result) => {
            result.data.DateOfJoining = new Date(result.data.DateOfJoining);
            this.$scope.loading = false;
            this.$scope.employee = result.data;
        })
        promise.catch((Error) => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Something went wrong. Make sure fields entered are correct.'));
        })

    }

    addEmployeeFromService(newEmployee) {

        var promise = this.employeeService.addEmployee(newEmployee);
        this.$scope.loading = true;
        promise.then(() => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Employee added successfully.'));
            this.$location.path('/employee/allEmployees');
        })
        promise.catch((Error) => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Something went wrong. Make sure fields entered are correct.'));
        });
    }

    updateEmployeeFromService(updatedEmployee) {
        this.$scope.loading = true;
        var id = this.$routeParams.Id;
        var promise = this.employeeService.updateEmployee(updatedEmployee);
        promise.then(() => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Employee updated successfully.'));
            this.$location.path('/employee/Details/' + id);
        })
        promise.catch((Error) => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Something went wrong. Make sure fields entered are correct.'));
        })
    }

    deleteEmployeeFromService() {
        this.$scope.loading = true;
        var Id = this.$routeParams.Id;

        var promise = this.employeeService.deleteEmployee(Id);
        promise.then(() => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Employee deleted successfully.'));
            this.$location.path('/employee/allEmployees');
        })
        promise.catch((Error) => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Something went wrong. Make sure fields entered are correct.'));
        })

    }

    changePasswordFromService(changePassword) {
        this.$scope.loading = true;
        changePassword.EmployeeId = this.$routeParams.Id;
        var promise = this.employeeService.changePassword(changePassword);
        promise.then(() => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Password changed successfully.'));
            this.$location.path('/employeeById/' + changePassword.EmployeeId);
        })
        promise.catch((Error) => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Something went wrong. Make sure fields entered are correct.'));
        })
    }

    editProfile(editedProfile) {
        this.$scope.loading = true;
        var id = this.$routeParams.Id;
        var promise = this.employeeService.updateEmployee(editedProfile);
        promise.then(() => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Profile edited successfully.'));
            this.$location.path('/employeeById/' + id);
        })
        promise.catch((Error) => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Something went wrong. Make sure fields entered are correct.'));
        })
    }

    onCancel() {
        this.$location.path('/employee/allEmployees');
    }

    onBack() {
        var id = this.$routeParams.Id;
        this.$location.path('/employeeById/' + id);
    }

    private allLeaveList() {
        var promise = this.employeeService.getLeaveList();
        promise.then((data) => {
            this.$scope.allLeaveList = data.data;
            if (data.data.length == 0 || data.data.length == 1) {
                this.$mdToast.show(this.$mdToast.simple().textContent('Add sick and casual leaves first and than proceed'));
                this.$location.path('/LeaveType');
            }
        })
    }
}


angular.module("app").controller("employeeController", employeeController);
