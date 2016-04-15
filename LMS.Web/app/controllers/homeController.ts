/// <reference path="../../scripts/typings/tsd.d.ts" />

interface IhomeControllerScope extends ng.IScope
{
    currentUserRole: any;
    getCurrentUserRole : Function;
}

class homeController{
    static $inject: string[] = ["$scope", "$location", "homeService", "$rootScope"];

    constructor(private $scope: IhomeControllerScope, private $location, private homeService: IhomeService, private $rootScope)
    {
        $scope.getCurrentUserRole = () => this.getCurrentUserRole();
        this.onload();
    }

    onload()
    {
        this.getCurrentUserRole();
    }

    private getCurrentUserRole()
    {
        var promise = this.homeService.getCurrentUserRole();
        promise.then((data) =>
        {
            this.$scope.currentUserRole = data.data;

            this.$rootScope.currentUserRole = data.data;

            if (this.$scope.currentUserRole == "User")
            {
                this.$location.path('/employee/ApplyLeaveRequest');
            }
            if (this.$scope.currentUserRole == "Admin") {
                this.$location.path('/employee/allEmployees');
            }
            if (this.$scope.currentUserRole == "TeamLeader") {
                this.$location.path('/employee/ApplyLeaveRequest');
            }
        });
    }
    
}

angular.module("app").controller("homeController", homeController);
