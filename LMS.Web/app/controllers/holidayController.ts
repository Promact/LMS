
interface IholidayControllerScope extends ng.IScope {
    holidayList: Array<string>;
    holiday: any;
    editedHoliday: Object;
    newHoliday: Object;

    allHolidayList: Function;
    getHolidayById: Function;
    addHoliday: Function;
    editHoliday: Function;
    deleteHoliday: Function;
    holidayTypes: Array<any>;
    holidayAddModal: any;
    dialogInstance: any;
    cancelModal: any;
    holidayEditModal: any;
    holidayDeleteModal: any;
    loading: any;
    currentDate: any;
}

class holidayController {
    static $inject: string[] = ["$scope", "holidayService", "$routeParams", "$location", "$rootScope", "$mdDialog", "$mdToast"];
    public Id: number;

    constructor(private $scope: IholidayControllerScope, private holidayService: IholidayService, private $routeParams, private $location, private $rootScope, private $mdDialog, private $mdToast) {
        this.$scope.allHolidayList = () => this.allHolidayList();
        this.$scope.getHolidayById = () => this.getHolidayById();
        this.$scope.deleteHoliday = () => this.deleteHoliday();
        this.$scope.addHoliday = (holiday) => this.addHoliday(holiday);
        this.$scope.editHoliday = (holiday) => this.editHoliday(holiday);
        $rootScope.TypeOfHoliday = TypeOfHoliday;
        $scope.holidayTypes = [{ name: "General", value: 0 }, { name: "Restricted", value: 1 }]
        $scope.holidayAddModal = () => this.holidayAddModal();
        $scope.cancelModal = () => this.cancelModal();
        $scope.holidayEditModal = (holidayId) => this.holidayEditModal(holidayId);
        $scope.holidayDeleteModal = (holidayId) => this.holidayDeleteModal(holidayId);
        this.date();
    }
    private date() {
        var date = new Date();
        this.$scope.currentDate = new Date(date.getFullYear(),0,1);
    }
    private allHolidayList() {
        this.$scope.loading = true;
        var promise = this.holidayService.getHolidayList();
        promise.then((data) => {
            for (var i = 0; i < data.data.length; i++) {
                data.data[i].Date = new Date(data.data[i].Date);
            }
            this.$scope.loading = false;
            this.$scope.holidayList = data.data;
        })
    }

    private getHolidayById() {
        this.Id = this.$routeParams.Id;
        var promise = this.holidayService.getHolidayById(this.Id);
        promise.then((data) => {
            data.data.Date = new Date(data.data.Date);
            this.$scope.holiday = data.data;
            this.$scope.holiday.TypeOfHoliday = this.$scope.holidayTypes[this.$scope.holiday.TypeOfHoliday];
        })
    }

    private deleteHoliday() {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$scope.cancelModal();
        this.Id = this.$routeParams.Id;
        var promise = this.holidayService.deleteHoliday(this.Id);
        promise.then((data) => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Holiday Deleted'));
            this.$scope.allHolidayList();

        })
            .catch((Error) => {
                this.$scope.loading = false;
                this.$mdToast.show(this.$mdToast.simple().textContent('something went wrong'));
            })
    }

    private addHoliday(newHoliday)
    {
        var bool=true;
        var promise = this.holidayService.getHolidayList();
        promise.then((data) =>
        {
            for (var i = 0; i < data.data.length; i++) {
                data.data[i].Date = new Date(data.data[i].Date);
            }
            for (var i = 0; i < data.data.length; i++)
            {
                if (newHoliday.Name == data.data[i].Name)
                {
                    bool = false;
                }

            }

            if (bool == false)
            {
                this.$mdToast.show(this.$mdToast.simple().textContent('Duplicate Holiday'));
                this.$mdDialog.hide();
            }
            else
            {
                this.$scope.loading = true;
        this.$scope.dialogInstance = this.$scope.cancelModal();
                var promise = this.holidayService.addHoliday(newHoliday);
                promise.then((data) =>
                {
                    this.$scope.loading = false;
                    this.$mdToast.show(this.$mdToast.simple().textContent('Holiday Added'));
                    this.$scope.newHoliday = null;
            this.$scope.allHolidayList();
                })
                    .catch((Error) =>
                    {
                        this.$scope.loading = false;
                        this.$mdToast.show(this.$mdToast.simple().textContent('something went wrong'));
                    })
            }
        })
    }

    private editHoliday(holiday) {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$scope.cancelModal();
        holiday.TypeOfHoliday = holiday.TypeOfHoliday.name;
        var promise = this.holidayService.editHoliday(holiday);
        promise.then((data) => {
            this.$scope.loading = false;
            this.$mdToast.show(this.$mdToast.simple().textContent('Holiday Edited'));
            this.$scope.allHolidayList();

        })
            .catch((Error) => {
                this.$scope.loading = false;
                this.$mdToast.show(this.$mdToast.simple().textContent('something went wrong'));
            })
    }
    private holidayAddModal() {
        this.$scope.newHoliday = null;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/holidayAddModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
    private cancelModal() {
        this.$scope.dialogInstance = this.$mdDialog.hide();
    }
    private holidayEditModal(holidayId) {
        this.$routeParams.Id = holidayId;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/holidayEditModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
    private holidayDeleteModal(holidayId) {
        this.$routeParams.Id = holidayId;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/holidayDeleteModal.html',
            scope: this.$scope,
            clickOutsideToClose: false,
            escapeToClose: true,
            preserveScope: true
        })
    }
}

angular.module("app").controller("holidayController", holidayController);