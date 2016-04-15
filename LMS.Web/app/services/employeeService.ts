/// <reference path="../../scripts/typings/tsd.d.ts" />

interface IemployeeService {
    getAllEmployees: () => ng.IPromise<any>;
    getEmployeeById: (Id : string) => ng.IPromise<any>;
    addEmployee: (newEmployee : any) => ng.IPromise<any>;
    updateEmployee: (updatedEmployee : any) => ng.IPromise<any>;
    deleteEmployee: (id :string) => ng.IPromise<any>;
    changePassword: (changePassword: any) => ng.IPromise<any>;
    getLeaveList: () => ng.IPromise<any>;
}

class employeeService implements IemployeeService {
    static $inject: string[] = ["$http"];

    constructor(private $http: ng.IHttpService) {

    }

    getAllEmployees() {
        return this.$http.get('api/Employee');
    }

    getEmployeeById(Id) {
        return this.$http.get('api/Employee/'+Id);
    }

    addEmployee(newEmployee) {
        return this.$http.post('api/Employee',newEmployee);
    }

    updateEmployee(updatedEmployee) {
        return this.$http.put('api/Employee',updatedEmployee);
    }

    deleteEmployee(id) {
        return this.$http.delete('api/Employee/'+id);
    }

    changePassword(changePassword) {
        return this.$http.put('api/Employee/Update',changePassword);
    }
    getLeaveList() {
        return this.$http.get('/api/LeaveAllowed');
    }
}

angular.module("app").service("employeeService", employeeService);
