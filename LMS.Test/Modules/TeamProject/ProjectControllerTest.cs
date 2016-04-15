using LMS.DomainModel.ApplicationClasses;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test.Modules.TeamProject

{
    [TestFixture]
    class ProjectControllerTest
    {
        private HttpClient client;
        [SetUp]
        public void SetUp()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:39414");
        }
        [Test]
        public void GetProjectResponseStatus()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var response = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;

           response = client.GetAsync("api/Project").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response = client.GetAsync("api/Project/5").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            //response = client.PostAsync("api/Employee",null).Result;
            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //response = client.PutAsync("api/Employee", null).Result;
            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //response = client.DeleteAsync("api/Employee", null).Result;
            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
         [Test]
        public void GetProjectResponse()
        {
                 var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
                 var loginJson = JsonConvert.SerializeObject(login);
                 var response = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
                 response = client.GetAsync("api/Project").Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;

                var teams = JsonConvert.DeserializeObject<List<TeamProjectAC>>(responseContent);

                Assert.AreEqual(3, teams.Count);
           
        }
         [Test]
        public void GetProjectResponseById()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginresponse = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/Project/27").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var team = JsonConvert.DeserializeObject<TeamProjectAC>(responseContent);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);           
        }
         [Test]
        public void PostProjectDetails()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var response = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;   
            var teamProject=new TeamProjectAC();
            teamProject.ProjectName="BCDEF";
            teamProject.ListOfEmployee = new List<EmployeeViewModel>();
            teamProject.ListOfEmployee.Add(new EmployeeViewModel { Id = "08bcaaa3-f780-4117-9c53-f2c1995b60ff" });
            teamProject.ListOfEmployee.Add(new EmployeeViewModel { Id = "08bcaaa3-f780-4117-9c53-f2c1995b60ff" });
            teamProject.TeamLeader.Id= "7bcdff58-1fa3-4b29-b1b4-a381440cd737";
            var teamProjectJson = JsonConvert.SerializeObject(teamProject);
             response = client.PostAsync("api/Project",new StringContent( teamProjectJson,Encoding.UTF8,"application/json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var team = JsonConvert.DeserializeObject<TeamProjectAC>(responseContent);
            Assert.AreEqual(2, team.ListOfEmployee.Count); 
        }
         [Test]
        public void PutProjectDetails()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var response = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var teamProject = new TeamProjectAC();
            teamProject.Id = 4;
            teamProject.ProjectName = "BCDEF";
            teamProject.ProjectId = 55;
            teamProject.ListOfEmployee = new List<EmployeeViewModel>();
            teamProject.ListOfEmployee.Add(new EmployeeViewModel { Id = "08bcaaa3-f780-4117-9c53-f2c1995b60ff" });
            teamProject.ListOfEmployee.Add(new EmployeeViewModel { Id = "08bcaaa3-f780-4117-9c53-f2c1995b60ff" });
            teamProject.TeamLeader.Id = "7bcdff58-1fa3-4b29-b1b4-a381440cd737";
             var teamProjectJson = JsonConvert.SerializeObject(teamProject);
            response = client.PutAsync("api/Project", new StringContent(teamProjectJson, Encoding.UTF8, "application/json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var team = JsonConvert.DeserializeObject<TeamProjectAC>(responseContent);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
         [Test]
        public void DeleteProjectDetails()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var response = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
             response = client.DeleteAsync("api/Project/18").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public void GetTeamLeaders()
         {
             var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
             var loginJson = JsonConvert.SerializeObject(login);
             var response = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
             response = client.GetAsync("api/Project/TeamLeader").Result;

             var responseContent = response.Content.ReadAsStringAsync().Result;

             var teams = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(responseContent);

             Assert.AreEqual(1, teams.Count);

         }
        [Test]
        public void GetEmployees()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var response = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            response = client.GetAsync("api/Project/Employee").Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;

            var teams = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(responseContent);

            Assert.AreEqual(4, teams.Count);

        }
    }
}
