/// <reference path="../../scripts/typings/tsd.d.ts" />
/// <reference path="../services/employeeservice.ts" />
/// <reference path="../models/employeeviewmodel.ts" />
/// <reference path="../models/teamprojectac.ts" />
/// <reference path="../services/projectservice.ts" />

interface IprojectControllerScope extends ng.IScope {
    getAllTeamProjectsFromService: Function;
    getTeamProjectByIdFromService: Function;
    addTeamProjectToService: Function;
    updateTeamProjectToService: Function;
    archiveTeamProjectToService: Function;
    getAllEmployeeFromService: Function;
    getAllTeamLeaderFromService: Function;
    toggleSelection: Function;
    loadTags: Function;
    projectList: any;
    project: any;
    newEmployeeList: any;
    employeeId: any;
    teamLeaderList: any;
    employeeList: any;
    teamProject: any;

    teamLeader: any;
    projectName: any;
    tags: any;
    defaultTag: any;
    teamProjectDetails: any;
    tagEmployee: any;
    getTeamCalendar: any;
    getAllProjectByTeamLeaderId: any;
    teamProjectLeaveDetails: any;
    selectedProject: any;
    projectCreate: any;
    createProjectModal: any;
    dialogInstance: any;
    cancelModal: any;
    updateProjectModal: any;
    projectCreated: any;
    updateProject: any;
    archiveProjectModal: any;
    getProjectById: any;
    teamLeaders: any;
    eventSources: any;
    events: any;
    deletedProjectList: any;
    uiConfig: any;
    alertEventOnClick: any;
    alertOnDrop: any;
    alertOnResize: any;
    readyProjectList: any;
    extraEventSignature: any;
    showToast: Function;
    loading: any;
    testQuery: any;
    onPaginate: Function;
    page: any;
    limit: any;
    count: any;
    
}

class projectController {
    static $inject: string[] = ["$scope", "$routeParams", "$location", "$q", "projectService", "$mdDialog", "$mdToast"];

    constructor(private $scope: IprojectControllerScope, private $routeParams, private $location, private $q, private projectService: IprojectService, private $mdDialog, private $mdToast) {

       
        $scope.getAllTeamProjectsFromService = () => this.getAllTeamProjectsFromService();


        $scope.getTeamProjectByIdFromService = () => this.getTeamProjectByIdFromService();
        $scope.loadTags = (query) => this.loadTags(query)
        $scope.addTeamProjectToService = () => this.addTeamProjectToService();
        $scope.updateTeamProjectToService = () => this.updateTeamProjectToService();
        $scope.archiveTeamProjectToService = () => this.archiveTeamProjectToService();
        $scope.getAllEmployeeFromService = () => this.getAllEmployeeFromService();
        $scope.getAllTeamLeaderFromService = () => this.getAllTeamLeaderFromService();
        $scope.getTeamCalendar = (selectedProject) => this.getTeamCalendar(selectedProject);
        $scope.getAllProjectByTeamLeaderId = () => this.getAllProjectByTeamLeaderId();
        //$scope.toggleSelection = (employee) => this.toggleSelection(employee);
        $scope.projectCreate = () => this.activate();
        $scope.projectCreated = () => this.activated();
        //this.activate();
        $scope.updateProjectModal = (project) => this.updateProjectModal(project);
        $scope.projectCreate = () => this.activate();
        $scope.createProjectModal = () => this.createProjectModal();
        $scope.cancelModal = () => this.cancelModal();
        $scope.archiveProjectModal = (projectId) => this.archiveProjectModal(projectId);
        $scope.getProjectById = () => this.getProjectById();
        $scope.readyProjectList = [];
        $scope.deletedProjectList = [];
        $scope.eventSources = [];
        $scope.events = [];
        $scope.extraEventSignature = () => this.extraEventSignature(event);
        $scope.showToast = () => this.showToast();
        $scope.testQuery = [];
        
        this.onLoad();
    }

    onLoad() {
     
        //this.getAllEmployeeFromService();
        //this.getAllTeamLeaderFromService();
        this.getAllTeamProjectsFromService();
        this.testQuery;
    }

    activate() {

        //this.getAllTeamProjectsFromService();

        this.$scope.defaultTag = [];
        this.getAllEmployeeFromService();
        this.getAllTeamLeaderFromService();
        this.$scope.tags = [];
    }
    activated() {

        this.$scope.defaultTag = [];
        this.getAllEmployeeFromService();
        this.getAllTeamLeaderFromService();
        this.$scope.tags = [];
        this.getTeamProjectByIdFromService();

    }

    private createProjectModal() {
        this.$scope.projectName = null;
        this.$scope.teamLeader = null;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/projectAddModal.html',
            scope: this.$scope,
            preserveScope: true,
            clickOutsideToClose: false,
            escapeToClose: true
        })
    }

    private updateProjectModal(project) {
        this.$scope.updateProject = project;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/projectUpdateModal.html',
            scope: this.$scope,
            preserveScope:true,
            clickOutsideToClose: false,
            escapeToClose: true
        })
    }

    private cancelModal() {
        this.$mdDialog.hide(this.$scope.dialogInstance);
        preserveScope:true
            this.$scope.loading = false;
    }

    private archiveProjectModal(projectId) {
        this.$routeParams.Id = projectId;
        this.$scope.dialogInstance = this.$mdDialog.show({
            templateUrl: '/templates/archiveProjectModal.html',
            scope: this.$scope,
            preserveScope: true,
            clickOutsideToClose: false,
            escapeToClose: true
        })
    }

    getAllTeamProjectsFromService() {
        this.$scope.loading = true;
        this.$scope.projectList = [];
        this.$scope.readyProjectList = [];
        this.$scope.deletedProjectList = [];
        var promise = this.projectService.getAllTeamProjects();
      
        promise.then((data) => {
            this.$scope.loading = false;
            this.$scope.projectList = data.data;
            this.$scope.count = data.data.length;

        })
    }
    testQuery = {
       
        limit: 5,
        page: 1
    };
   
    getProject() {
        //var promise = this.$scope.getAllTeamProjectsFromService.get(this.$scope.limit, this.$scope.page,this.$scope.projectList).$promise;
    }
   


    getTeamProjectByIdFromService() {
        this.$scope.loading = true;
        var id = this.$scope.updateProject.ProjectId;

        var promise = this.projectService.getTeamProjectDetailsById(id);
        promise.then((data) =>
        {
            this.$scope.loading = false;
            this.$scope.project = data.data;
           
           
            for (var i = 0; i < this.$scope.project.ListOfEmployee.length; i++)
            {
                this.$scope.defaultTag.push({ text: this.$scope.project.ListOfEmployee[i].Name, id: this.$scope.project.ListOfEmployee[i].Id });
            }
        });

    }

    getProjectById() {
        var id = this.$routeParams.Id;
        var promise = this.projectService.getTeamProjectDetailsById(id);
        promise.then((data) => {
            this.$scope.project = data.data;
        })
        }

    addTeamProjectToService() {

        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var teamProject = new models.TeamProjectAC();
        var teamLeader = new models.EmployeeViewModel();
        var employeeList = new Array<models.EmployeeViewModel>();
        if (this.$scope.teamLeader != undefined) {
            teamLeader.Id = this.$scope.teamLeader;
            if (this.$scope.projectName != null) {
                teamProject.ProjectName = this.$scope.projectName;
                var value;
                teamProject.TeamLeader = teamLeader;
                for (var i = 0; i < this.$scope.defaultTag.length; i++) {
                    if (this.$scope.defaultTag[i].id == null) {
                        this.$mdToast.show(this.$mdToast.simple().textContent('Please tag the employee correctly'));
                     
                        this.$scope.createProjectModal();
                        value = null;
                    }
                    else {
                        var x = new models.EmployeeViewModel();
                        x.Id = this.$scope.defaultTag[i].id;
                        employeeList.push(x);
                        value = 'abc';
                    }
                }
                if (value != null) {
                    teamProject.ListOfEmployee = employeeList;
                    var promise = this.projectService.addTeamProject(teamProject);
                    promise.then((data) => {
                        this.$scope.loading = false;
                        this.$mdToast.show(this.$mdToast.simple().textContent('The project is Successfully created'));

                        this.$scope.getAllTeamProjectsFromService();

                    });
                    promise.catch((Error) => {
                        this.showToast();
                    });
                }
            }
            else {
                this.$mdToast.show(this.$mdToast.simple().textContent('Please provide ProjectName correctly'));
                this.$scope.createProjectModal();}
        }
        else {
            this.$mdToast.show(this.$mdToast.simple().textContent('Please provide TeamLeader correctly'));
            this.$scope.createProjectModal();
        }
       
    }



    updateTeamProjectToService() {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var project = this.$scope.updateProject;


        var teamProject = new models.TeamProjectAC();
        var teamLeader = new models.EmployeeViewModel();
        var employeeList = new Array<models.EmployeeViewModel>();
        if (this.$scope.project.TeamLeader != undefined) {
            teamLeader.Id = this.$scope.project.TeamLeader.Id;
            if (this.$scope.project.ProjectName != null) {
                teamProject.ProjectName = this.$scope.updateProject.ProjectName;
                teamProject.Id = this.$scope.updateProject.Id;
                teamProject.ProjectId = this.$scope.updateProject.ProjectId;
                teamProject.TeamLeader = JSON.parse(this.$scope.updateProject.TeamLeader);
                var value;
                for (var i = 0; i < this.$scope.defaultTag.length; i++) {
                    if (this.$scope.defaultTag[i].id == null) {
                        this.$mdToast.show(this.$mdToast.simple().textContent('Please tag the employee correctly'));
                        this.$scope.updateProjectModal();
                        value = null;
                    }
                    else {
                        var x = new models.EmployeeViewModel();
                        x.Id = this.$scope.defaultTag[i].id;
                        employeeList.push(x);
                        value = 'abc';
                    }
                }
                if (value != null) {
                    teamProject.ListOfEmployee = employeeList;
                    var promise = this.projectService.updateTeamProject(teamProject);
                    promise.then((data) => {
                        this.$scope.loading = false;
                        this.$mdToast.show(this.$mdToast.simple().textContent('The project is Successfully updated'));

                        this.$scope.getAllTeamProjectsFromService();


                    });
                    promise.catch((Error) => {
                        this.showToast();
                    });
                }
                } else {
                    this.$mdToast.show(this.$mdToast.simple().textContent('Please provide ProjectName correctly'));
                    this.$scope.updateProjectModal();

                }

            } else {
                this.$mdToast.show(this.$mdToast.simple().textContent('Please provide TeamLeader correctly'));
                this.$scope.updateProjectModal();

            }

        }
    
    archiveTeamProjectToService() {
        this.$scope.loading = true;
        this.$scope.dialogInstance = this.$mdDialog.hide();
        var id = this.$routeParams.Id;
        var teamProject = new models.TeamProjectAC();
        var promise = this.projectService.getTeamProjectDetailsById(id);
        promise.then((data) => {
            this.$scope.teamProject = data.data;
            var promise = this.projectService.archiveTeamProject(this.$scope.teamProject);
            promise.then((data) => {
                this.$scope.loading = false;
                this.$scope.archiveTeamProjectToService = data;
                this.$mdToast.show(this.$mdToast.simple().textContent('The project is Successfully archived'));
                this.$scope.getAllTeamProjectsFromService();
            });
        });
    }

    getAllEmployeeFromService() {
        var promise = this.projectService.getAllEmployee();
        promise.then((data) => {
            this.$scope.employeeList = data.data;
            if (this.$scope.employeeList.length != 0) {
                for (var i = 0; i < this.$scope.employeeList.length; i++) {

                    
                    this.$scope.tags.push({ text: this.$scope.employeeList[i].Name, id: this.$scope.employeeList[i].Id });
                }
            }else{
                this.$scope.dialogInstance = this.$mdDialog.hide();
                this.$location.path('/employee/create');

                this.$mdToast.show(this.$mdToast.simple().textContent('No employee available.Add employees first'));
            }
        });
    }
    loadTags(query) {
        var deferred = this.$q.defer();

        deferred.resolve(this.$scope.tags);
        return deferred.promise;

    }

    getAllTeamLeaderFromService() {
        var promise = this.projectService.getAllTeamLeader();
        promise.then((data) => {
            if (data.data.length != 0) {
                this.$scope.teamLeaderList = data.data;
            }
            else {
                this.$scope.dialogInstance = this.$mdDialog.hide();
                this.$location.path('/employee/create');
                this.$mdToast.show(this.$mdToast.simple().textContent('No TeamLeader available.Add TeamLeader first'));}
        });
    }

    getTeamCalendar(selectedProject) {
        this.$scope.loading = true;
        this.$scope.eventSources = [];
        this.$scope.events = [];
        var promise = this.projectService.getTeamCalendar(selectedProject);
        promise.then((data) => {
            if (data.data.length == 0)
            { this.$mdToast.show(this.$mdToast.simple().textContent('No employees under this project has taken leave')); }
            this.$scope.loading = false;
            this.$scope.teamProjectLeaveDetails = data.data;

            for (var i = 0; i < this.$scope.teamProjectLeaveDetails.length; i++) {

                this.$scope.events.push({ title: this.$scope.teamProjectLeaveDetails[i].EmployeeName, end: this.$scope.teamProjectLeaveDetails[i].EndDate, start: this.$scope.teamProjectLeaveDetails[i].StartDate });

            }

            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();


            //this.$scope.eventSources=this.$scope.events;
            this.$scope.eventSources = [

                { title: 'Long Event', start: new Date(), end: new Date(2016, 3, 28) }

            ];
            this.$scope.uiConfig = {
                calendar: {
                    height: 450,
                    editable: true,
                    header: {
                        left: 'month basicWeek ',
                        center: 'title',
                        right: 'today prev,next'
                    },
                    dayClick: this.$scope.alertEventOnClick,
                    eventDrop: this.$scope.alertOnDrop,
                    eventResize: this.$scope.alertOnResize
                }
            };
            this.$scope.getAllProjectByTeamLeaderId();
        })
    }
    extraEventSignature(event) {
        return "" + event.price;
    }

    getAllProjectByTeamLeaderId() {
        this.$scope.loading = true;
        this.$scope.eventSources = [];
        var promise = this.projectService.getAllProjectByTeamLeaderId();
        promise.then((data) => {
            this.$scope.loading = false;
            this.$scope.teamProjectDetails = data.data;
        })
        }
    private showToast() {
        this.$mdToast.show(this.$mdToast.simple().textContent('something went wrong')
            )
    }

  
}

angular.module("app").controller("projectController", projectController);
