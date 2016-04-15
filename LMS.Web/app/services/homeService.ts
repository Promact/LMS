interface IhomeService
{
    getCurrentUserRole : () => ng.IPromise<any>;
} 

class homeService implements IhomeService
{
    static $inject: string[] = ['$http'];
    constructor(private $http: ng.IHttpService)
    {

    }
    getCurrentUserRole()
    {
        return this.$http.get('Account/CurrentUser');
    }
}

angular.module("app").service("homeService", homeService);