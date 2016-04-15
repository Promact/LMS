using System;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using LMS.Test.Models;


namespace LMS.Test
{
    [TestFixture]
    public class EmployeeControllerTest
    {
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:39414");

        }    

        [Test]
        public void GetEmployeesResponse()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };

            var loginJson = JsonConvert.SerializeObject(login);

            var response = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;

            response = client.GetAsync("api/Employee").Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;

            var employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(responseContent);

            Assert.AreEqual(15, employees.Count);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        
        }

    [Test]
        public void GetEmployeeById()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };

            var loginJson = JsonConvert.SerializeObject(login);

            var response = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;

            response = client.GetAsync("api/Employee/758a0c46-921a-45da-8a05-abf0c33530e6").Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response = client.GetAsync("api/Employee/58").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Test]
        public void PostEmployee()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };

            var loginJson = JsonConvert.SerializeObject(login);

            var response = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;

            var employee = new EmployeeViewModel() { Name = "abcd", Password = "abcd", DateOfJoining = DateTime.Now, Email = "rhit@bla.com" };
            
            var employeeJson = JsonConvert.SerializeObject(employee);

            response = client.PostAsync("api/Employee", new StringContent(employeeJson, Encoding.UTF8, "application/json")).Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;

            var returnedEmployee = JsonConvert.DeserializeObject<EmployeeViewModel>(responseContent);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, returnedEmployee.Id);

        }
        [Test]
        public void PutEmployee()
        {
            var employee = new EmployeeViewModel() { Id = "44590315-26d8-48a7-a9a5-e6451c3b3ce4", Name = "rhiteek",  Email = "rhit@rhit.com" };

            var employeeJson = JsonConvert.SerializeObject(employee);

            var response = client.PutAsync("api/Employee", new StringContent(employeeJson, Encoding.UTF8, "application/json")).Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;

            var returnedEmployee = JsonConvert.DeserializeObject<EmployeeViewModel>(responseContent);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, returnedEmployee.Id);
           
        }

        [Test]
        public void DeleteEmployee()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };

            var loginJson = JsonConvert.SerializeObject(login);

            var response = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;

            response = client.DeleteAsync("api/Employee/f1e1df7c-b382-44bb-9263-729918c74b71").Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response = client.DeleteAsync("api/Employee/21").Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]

        public void ChangePassword()
        {
            var changePassword = new ChangePasswordAC() { EmployeeId = "85f80596-7eec-4e1c-bc47-25048022c09b", OldPassword = "proton@123", NewPassword = "pn@123", ConfirmPassword = "pon@123" };

            var changePasswordJson = JsonConvert.SerializeObject(changePassword);

            var response = client.PutAsync("api/Employee/Update", new StringContent(changePasswordJson, Encoding.UTF8, "application/json")).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

             changePassword = new ChangePasswordAC() { EmployeeId = "85f80596-7eec-4e1c-bc47-25048022c09b", OldPassword = "proton@123", NewPassword = "pn@123", ConfirmPassword = "pn@123" };

             changePasswordJson = JsonConvert.SerializeObject(changePassword);

             response = client.PutAsync("api/Employee/Update", new StringContent(changePasswordJson, Encoding.UTF8, "application/json")).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
