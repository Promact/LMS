using LMS.Test.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test
{
    [TestFixture]
    public class HolidayControllerTest
    {
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:39414");

        }

        /// <summary>
        /// Checking of fetching all holiday list 
        /// Anyone can access
        /// </summary>
        [Test]
        public void GetHolidayType()
        {
            //Admin Login
            //var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };                    
            //Team Leader Login
            //var login = new EmployeeViewModel() { Email = "exampleTL@exampleTL.com", Password = "exampleTL" };
            //Employee Login
            var login = new EmployeeViewModel() { Email = "rajdeep@promactinfo.com", Password = "Promact2015" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;


            var response = client.GetAsync("api/HolidayType").Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;

            var holidays = JsonConvert.DeserializeObject<List<HolidayType>>(responseContent);

            Assert.AreNotEqual(0, holidays.Count);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Checking of fetching a particular holiday by its id
        /// Only Admin can access
        /// </summary>
        [Test]
        public void GetHolidayTypeById()
        {
            var response = client.GetAsync("api/HolidayType/3").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response = client.GetAsync("api/Holidaytype/50").Result;
            Assert.AreNotEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        /// <summary>
        /// Checking of creating new type of holiday
        /// Only Admin can access
        /// </summary>
        [Test]
        public void PostHoliday()
        {
            //Admin Login
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };                    
            //Team Leader Login
            //var login = new EmployeeViewModel() { Email = "exampleTL@exampleTL.com", Password = "exampleTL" };
            //Employee Login
            //var login = new EmployeeViewModel() { Email = "rajdeep@promactinfo.com", Password = "Promact2015" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;


            //var holiday = new HolidayType { Date = DateTime.Now, Name = "blah" };
            var holiday = new HolidayType { Name = null };
            var holidayJson = JsonConvert.SerializeObject(holiday);
            var response = client.PostAsync("api/HolidayType", new StringContent(holidayJson, Encoding.UTF8, "application/Json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnedHoliday = JsonConvert.DeserializeObject<HolidayType>(responseContent);
            Assert.AreNotEqual(0, returnedHoliday.Id);
        }

        /// <summary>
        /// Checking of deleting a particular holiday from database by its id
        /// Only Admin can access
        /// </summary>
        [Test]
        public void DeleteHoliday()
        {
            //Admin Login
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };                    
            //Team Leader Login
            //var login = new EmployeeViewModel() { Email = "exampleTL@exampleTL.com", Password = "exampleTL" };
            //Employee Login
            //var login = new EmployeeViewModel() { Email = "rajdeep@promactinfo.com", Password = "Promact2015" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;


            var response = client.DeleteAsync("api/HolidayType/21").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Checking of editing a particular holiday
        /// Only Admin can access
        /// </summary>
        [Test]
        public void PutHoliday()
        {
            //Admin Login
            //var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };                    
            //Team Leader Login
            var login = new EmployeeViewModel() { Email = "exampleTL@exampleTL.com", Password = "exampleTL" };
            //Employee Login
            //var login = new EmployeeViewModel() { Email = "rajdeep@promactinfo.com", Password = "Promact2015" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;


            var holiday = client.GetAsync("api/HolidayType/13").Result;
            var content = holiday.Content.ReadAsStringAsync().Result;
            var update = JsonConvert.DeserializeObject<HolidayType>(content);


            var editedHoliday = new HolidayType { Id = 13, Date = DateTime.Now, Name = "ABCD", CreatedOn = update.CreatedOn };

            var editedHolidayJson = JsonConvert.SerializeObject(editedHoliday);

            var response = client.PutAsync("api/HolidayType/13", new StringContent(editedHolidayJson, Encoding.UTF8, "application/Json")).Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;

            var returnedEditedHoliday = JsonConvert.DeserializeObject<HolidayType>(responseContent);

            
            Assert.AreEqual("ABCD", returnedEditedHoliday.Name);
        }
    }
}

