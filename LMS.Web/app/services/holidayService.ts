 
interface IholidayService
{
    getHolidayList : () => ng.IPromise<any>;
    getHolidayById: (number) => ng.IPromise<any>;
    deleteHoliday: (number) => ng.IPromise<any>;
    addHoliday: (holiday) => ng.IPromise<any>;
    editHoliday: (editedHoliday) => ng.IPromise<any>;
}

class holidayService implements IholidayService
{
    static $inject: string[] = ["$http"];

    constructor(private $http: ng.IHttpService)
    {
            
    }

    getHolidayList()
    {
        return this.$http.get('/api/HolidayType');
    }

    getHolidayById(Id)
    {
        return this.$http.get('/api/HolidayType/Details/' + Id);
    }

    deleteHoliday(Id)
    {
        return this.$http.delete('/api/HolidayType/Delete/' + Id);
    }

    addHoliday(holiday)
    {
        return this.$http.post('/api/HolidayType/Create', holiday);
    }

    editHoliday(editedHoliday)
    {
        return this.$http.put('/api/HolidayType/Edit', editedHoliday);
    }
}

angular.module("app").service("holidayService", holidayService);