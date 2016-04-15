/// <reference path="../../scripts/typings/tsd.d.ts" />

interface IprojectService {
    getAllTeamProjects: () => ng.IPromise<any>;
    getTeamProjectDetailsById: (id: any) => ng.IPromise<any>;
    addTeamProject: (teamProjectAC: any) => ng.IPromise<any>;
    updateTeamProject: (teamProjectAC: any) => ng.IPromise<any>;
    archiveTeamProject: (id: number) => ng.IPromise<any>;
    getAllEmployee: () => ng.IPromise<any>;
    getAllTeamLeader: () => ng.IPromise<any>;
    getTeamCalendar: (ProjectName: any) => ng.IPromise<any>;
    getAllProjectByTeamLeaderId: () => ng.IPromise<any>;
}

class projectService implements IprojectService {
    static $inject: string[] = ["$http"];

    constructor(private $http: ng.IHttpService) {

    }

    getAllTeamProjects() {
        return this.$http.get('api/Project');


    }
    getTeamCalendar(projectName) {
        return this.$http.get('api/TeamCalendar/' + projectName);
    }
    getTeamProjectDetailsById(id) {
        return this.$http.get('api/Project/' + id);

    }
    addTeamProject(teamProjectAC) {
        return this.$http.post('api/Project', teamProjectAC);

    }
    updateTeamProject(teamProjectAC) {
        return this.$http.put('api/Project', teamProjectAC);
    }
    archiveTeamProject(teamProjectAC) {
        return this.$http.put('api/Project/Archive/', teamProjectAC);

    }
    getAllEmployee() {
        return this.$http.get('api/Project/Employee');

    }
    getAllTeamLeader() {
        return this.$http.get('api/Project/TeamLeader');
    }
    getAllProjectByTeamLeaderId() {
        return this.$http.get('api/Project/TeamLeaderId');
    }
}

angular.module("app").service("projectService", projectService);
