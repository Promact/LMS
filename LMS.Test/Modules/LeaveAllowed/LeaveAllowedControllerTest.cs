using LMS.Test.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test.Controllers
{
    [TestFixture]
    class LeaveAllowedControllerTest
    {
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:39414");
        }

        /// <summary>
        /// Checking of assigning number of days for casual and sick leaves
        /// </summary>
        [Test]
        public void PostLeaves()
        {
            //Admin Login
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };                    
            //Team Leader Login
            //var login = new EmployeeViewModel() { Email = "exampleTL@exampleTL.com", Password = "exampleTL" };
            //Employee Login
            //var login = new EmployeeViewModel() { Email = "rajdeep@promactinfo.com", Password = "Promact2015" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;



            var leave = new LeaveAllowed { LeaveType = LMS.DomainModel.Models.Type.Casual , AllowedDays = 20 };
            var leaveJson = JsonConvert.SerializeObject(leave);
            var response = client.PostAsync("api/LeaveAllowed", new StringContent(leaveJson, Encoding.UTF8, "application/Json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnedLeave = JsonConvert.DeserializeObject<LeaveAllowed>(responseContent);
            Assert.AreNotEqual(0, returnedLeave.Id);
        }

        /// <summary>
        /// Checking of editing the number of days for casual or sick leave
        /// </summary>
        [Test]
        public void PutLeaves()
        {
            //Admin Login
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            //Team Leader Login
            //var login = new EmployeeViewModel() { Email = "exampleTL@exampleTL.com", Password = "exampleTL" };
            //Employee Login
            //var login = new EmployeeViewModel() { Email = "rajdeep@promactinfo.com", Password = "Promact2015" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;



            var editedLeave = new LeaveAllowed { Id = 3, LeaveType = LMS.DomainModel.Models.Type.Casual, AllowedDays = 15 };
            var editedLeaveJson = JsonConvert.SerializeObject(editedLeave);
            var response = client.PutAsync("api/LeaveAllowed/3", new StringContent(editedLeaveJson, Encoding.UTF8, "application/Json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnedLeave = JsonConvert.DeserializeObject<LeaveAllowed>(responseContent);
            Assert.AreEqual(15, returnedLeave.AllowedDays);
        }
    }
}
