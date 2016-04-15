module models{
    export class TeamProjectAC{
       
        Id :number;
        ProjectName: string;
        ListOfEmployee: Array<EmployeeViewModel>;
        ProjectId: number;
        TeamLeader: EmployeeViewModel;
        IsProjectArchived: boolean;
    }
}
