
interface IleaveService
{
    getLeaveList : () => ng.IPromise<any>;
    getLeaveById: (number) => ng.IPromise<any>;
    addLeave: (Object) => ng.IPromise<any>;
    editLeave: (editedLeave) => ng.IPromise<any>;
}

class leaveService
{
    static $inject: string[] = ["$http"];

    constructor(private $http: ng.IHttpService)
    {
    }

    getLeaveList()
    {
        return this.$http.get('/api/LeaveAllowed');
    }

    getLeaveById(Id)
    {
        return this.$http.get('/api/LeaveAllowed/Details/' + Id); 
    }

    addLeave(leaveAllowed)
    {
        return this.$http.post('/api/LeaveAllowed', leaveAllowed);
    }

    editLeave(leave)
    {
        return this.$http.put('/api/LeaveAllowed/Edit', leave);
    }
}

angular.module("app").service("leaveService", leaveService);