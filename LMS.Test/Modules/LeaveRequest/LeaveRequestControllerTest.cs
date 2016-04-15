using LMS.DomainModel.Models;
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

namespace LMS.Test.Modules.LeaveRequest
{
    [TestFixture]
    public class LeaveRequestControllerTest
    {
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:39414");
        }

        /// <summary>
        /// To check the status of GetAllLeavesDetails for correct values
        /// </summary>
        [Test]
        public void GetAllLeavesReponsesStatus()
        {
            var login = new EmployeeViewModel() { Email = "rushi@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/Leaves").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<List<LeaveRequest>>(responseContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, returnResponse.Count);
        }

        /// <summary>
        /// To check the status of GetLeavesDetailsForEmployee for correct employeeId values
        /// </summary>
        [Test]
        public void GetLeavesDetailsReponsesStatus()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/Employee/e271c7f9-ae64-42a2-bfa7-c84eef31aeea/Leaves").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            var leaves = JsonConvert.DeserializeObject<List<LeaveRequest>>(content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, leaves.Count);
        }

        /// <summary>
        /// To check the status of GetLeavesDetailsForEmployee for wrong employeeId values
        /// </summary>
        [Test]
        public void GetLeavesDetailsForError()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/Employee/acb8-1b04-4a3b-9969/Leaves").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// To check the status of GetAllLeavesDetailsByStatus for correct Status values
        /// </summary>
        [Test]
        public void GetAllLeavesDetailsByStatusResponseStatus()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/Leaves/Approved").Result;
            string content = response.Content.ReadAsStringAsync().Result;
            var leaves = JsonConvert.DeserializeObject<List<LeaveRequest>>(content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, leaves.Count);
        }

        /// <summary>
        /// To check the status of GetAllLeavesDetailsByStatus for wrong Status value
        /// </summary>
        [Test]
        public void GetAllLeavesDetailsByStatusForError()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/Leaves/Escalad").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// To check the status of GetLeavesDetailsForEmployeeByStatus for correct values
        /// </summary>
        [Test]
        public void GetLeavesDetailsForEmployeeByStatusResponseStatus()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("57af3879-bad7-4403-8801-114435bc01d7/EmployeeLeavesStatus/approved").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var leaves = JsonConvert.DeserializeObject<List<LeaveRequest>>(content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, leaves.Count);
        }

        /// <summary>
        /// To check the status of GetLeavesDetailsForEmployeeByStatus for wrong employeeId and correct status value
        /// </summary>
        [Test]
        public void GetLeavesDetailsForEmployeeByStatusForErrorWithWrongEmployee()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("57af3879-bad7-4403-8801-114435bc/EmployeeLeavesStatus/approved").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var leaves = JsonConvert.DeserializeObject<LeaveRequest>(content);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual(0, leaves.Id);
        }

        /// <summary>
        /// To check the status of GetLeavesDetailsForEmployeeByStatus for correct employeeId and wrong status value
        /// </summary>
        [Test]
        public void GetLeavesDetailsForEmployeeByStatusForErrorWithWrongStatus()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("57af3879-bad7-4403-8801-114435bc01d7/EmployeeLeavesStatus/afsgv").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// To check the status of CreateLeaveRequest for correct values and check its existence
        /// </summary>
        [Test]
        public void CreateLeaveRequestResponseStatus()
        {
            var login = new EmployeeViewModel() { Email = "siddhartha@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var leave = new LeaveRequest() { StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date, Type = LMS.DomainModel.Models.Type.Casual, Reason = "Chutti Chaahye mujhe", Unit = DomainModel.Models.Unit.Full };
            var leaveJson = JsonConvert.SerializeObject(leave);
            var response = client.PostAsync("api/LeaveRequest", new StringContent(leaveJson, Encoding.UTF8, "application/json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<LeaveRequest>(responseContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, returnResponse.EmployeeId);
        }

        /// <summary>
        /// To check the status of CreateLeaveRequest for wrong modelState 
        /// </summary>
        [Test]
        public void CreateLeaveRequestForError()
        {
            var login = new EmployeeViewModel() { Email = "siddhartha@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var leave = new LeaveRequest() { EndDate = DateTime.Now.Date, Type = LMS.DomainModel.Models.Type.Casual, Reason = "Chutti Chaahye mujhe", Unit = DomainModel.Models.Unit.Full };
            var leaveJson = JsonConvert.SerializeObject(leave);
            var response = client.PostAsync("api/LeaveRequest", new StringContent(leaveJson, Encoding.UTF8, "application/json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<LeaveRequest>(responseContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(null, returnResponse);
        }

        /// <summary>
        /// To check the status of LeaveRequestStatusResult for correct values
        /// </summary>
        [Test]
        public void ApprovedRequestResponseStatus()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin@123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var leaveStatus = new LeaveStatus() { LeaveRequestId = 1, Condition = DomainModel.Models.Condition.Approved };
            var leaveStatusJson = JsonConvert.SerializeObject(leaveStatus);
            var response = client.PutAsync("api/LeaveRequest", new StringContent(leaveStatusJson, Encoding.UTF8, "application/json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<LeaveStatus>(responseContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(DomainModel.Models.Condition.Approved, returnResponse.Condition);
        }

        /// <summary>
        /// To check the status of LeaveRequestStatusResult for wrong leaveRequestId
        /// </summary>
        [Test]
        public void ApprovedRequestForError()
        {
            var login = new EmployeeViewModel() { Email = "khyati@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var leaveStatus = new LeaveStatus() { LeaveRequestId = 1000, Condition = DomainModel.Models.Condition.Approved };
            var leaveStatusJson = JsonConvert.SerializeObject(leaveStatus);
            var response = client.PutAsync("api/LeaveRequest", new StringContent(leaveStatusJson, Encoding.UTF8, "application/json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<LeaveStatus>(responseContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(null, returnResponse);
        }

        /// <summary>
        /// To check the status of DeleteLeaveRequest for correct values
        /// </summary>
        [Test]
        public void CancelLeaveRequestResponseStatus()
        {
            var login = new EmployeeViewModel() { Email = "siddhartha@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.DeleteAsync("api/LeaveRequest/5").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<int?>(responseContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, returnResponse.Value);
        }

        /// <summary>
        /// To check the status of DeleteLeaveRequest for wrong LeaveRequestId
        /// </summary>
        [Test]
        public void CancelLeaveRequestForErrorForWrongLeaveRequestId()
        {
            var login = new EmployeeViewModel() { Email = "siddhartha@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.DeleteAsync("api/LeaveRequest/140").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<int?>(responseContent);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual(null, returnResponse);
        }

        /// <summary>
        /// To check the status of DeleteLeaveRequest for wrong EmployeeId
        /// </summary>
        [Test]
        public void CancelLeaveRequestForErrorForWrongEmployeeId()
        {
            var login = new EmployeeViewModel() { Email = "rahul@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.DeleteAsync("api/LeaveRequest/8").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.AreEqual(null, responseContent);
        }

        /// <summary>
        /// To check the status of GetAllLeaveRequestListForTeam for correct values
        /// </summary>
        [Test]
        public void GetAllLeaveRequestListForTeamResponseStatus()
        {
            var login = new EmployeeViewModel() { Email = "rushi@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/TeamLeaves").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<List<LeaveStatus>>(responseContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, returnResponse.Count);
        }

        /// <summary>
        /// To check the status of GetLeaveRequestListForTeamByStatus for correct values
        /// </summary>
        [Test]
        public void GetLeaveRequestListForTeamByStatusResponseStatus()
        {
            var login = new EmployeeViewModel() { Email = "khyati@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/TeamLeaves/approved").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<List<LeaveStatus>>(responseContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, returnResponse.Count);
        }

        /// <summary>
        /// To check the status of GetLeaveRequestListForTeamByStatus for wrong status
        /// </summary>
        [Test]
        public void GetLeaveRequestListForTeamByStatusForError()
        {
            var login = new EmployeeViewModel() { Email = "khyati@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/TeamLeaves/asdfghrg").Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);;
        }

        /// <summary>
        /// To check the status of TeamCalendar for correct values
        /// </summary>
        [Test]
        public void TeamCalendarResponseStatus()
        {
            var login = new EmployeeViewModel() { Email = "khyati@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/TeamCalendar/1").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<List<LeaveRequest>>(responseContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, returnResponse.Count);
        }

        /// <summary>
        /// To check the status of TeamCalendar for wrong ProjectId
        /// </summary>
        [Test]
        public void TeamCalendarForError()
        {
            var login = new EmployeeViewModel() { Email = "khyati@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/TeamCalendar/555").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<List<LeaveRequest>>(responseContent);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual(null, returnResponse);
        }

        /// <summary>
        /// To check the status of TeamCalendar for correct ProjectId but wrong TL
        /// </summary>
        [Test]
        public void TeamCalendarForErrorForWrongTL()
        {
            var login = new EmployeeViewModel() { Email = "khyati@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var response = client.GetAsync("api/TeamCalendar/2").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<List<LeaveRequest>>(responseContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(null, returnResponse);
        }

        /// <summary>
        /// To check the status of ApplyForSickLeave for correct values
        /// </summary>
        [Test]
        public void ApplyForSickLeaveResponseStatus()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var leave = new LeaveRequest() { StartDate = DateTime.Now.Date, Type = LMS.DomainModel.Models.Type.Sick, Reason = "Fever", Unit = DomainModel.Models.Unit.Full, EmployeeId = "57af3879-bad7-4403-8801-114435bc01d7", CreatedOn = DateTime.Now };
            var leaveJson = JsonConvert.SerializeObject(leave);
            var response = client.PostAsync("api/SickLeaveRequest", new StringContent(leaveJson, Encoding.UTF8, "application/json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<LeaveRequest>(responseContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(0, returnResponse.Id);
        }

        /// <summary>
        /// To check the status of ApplyForSickLeave for wrong values
        /// </summary>
        [Test]
        public void ApplyForSickLeaveForError()
        {
            var login = new EmployeeViewModel() { Email = "admin@promactinfo.com", Password = "admin.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var leave = new LeaveRequest() { Type = LMS.DomainModel.Models.Type.Sick, Reason = "Fever", Unit = DomainModel.Models.Unit.Full, EmployeeId = "57af3879-bad7-4403-8801-114435bc01d7", CreatedOn = DateTime.Now };
            var leaveJson = JsonConvert.SerializeObject(leave);
            var response = client.PostAsync("api/SickLeaveRequest", new StringContent(leaveJson, Encoding.UTF8, "application/json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<LeaveRequest>(responseContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(null, returnResponse);
        }

        /// <summary>
        /// To check the status of UpdateSickLeave for correct values
        /// </summary>
        [Test]
        public void UpdateSickLeaveResponseStatus()
        {
            var leave = new LeaveRequest() { StartDate = DateTime.Now.Date, Type = LMS.DomainModel.Models.Type.Sick, Reason = "Fever", Unit = DomainModel.Models.Unit.Full, EmployeeId = "57af3879-bad7-4403-8801-114435bc01d7", EndDate = DateTime.Now, Certificate = "Yes I don't have it", DoctorName = "Bimari Ram", Id = 3, Number = "1234567890", Condition = DomainModel.Models.Condition.Pending, CreatedOn = DateTime.Now };
            var leaveJson = JsonConvert.SerializeObject(leave);
            var response = client.PutAsync("api/SickLeaveUpdate", new StringContent(leaveJson, Encoding.UTF8, "application/json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<LeaveRequest>(responseContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(DomainModel.Models.Condition.Approved, returnResponse.Condition);
        }

        /// <summary>
        /// To check the status of UpdateSickLeave for wrong modelState
        /// </summary>
        [Test]
        public void UpdateSickLeaveForError()
        {
            var login = new EmployeeViewModel() { Email = "rahul@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var leave = new LeaveRequest() { Type = LMS.DomainModel.Models.Type.Sick, Reason = "Fever", Unit = DomainModel.Models.Unit.Full, EmployeeId = "57af3879-bad7-4403-8801-114435bc01d7", EndDate = DateTime.Now, Certificate = "Yes I don't have it", DoctorName = "Bimari Ram", Id = 3, Number = "1234567890", Condition = DomainModel.Models.Condition.Pending, CreatedOn = DateTime.Now };
            var leaveJson = JsonConvert.SerializeObject(leave);
            var response = client.PutAsync("api/SickLeaveUpdate", new StringContent(leaveJson, Encoding.UTF8, "application/json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<LeaveRequest>(responseContent);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(null, returnResponse);
        }

        /// <summary>
        /// To check the status of UpdateSickLeave for correct modelState but wrong employee login
        /// </summary>
        [Test]
        public void UpdateSickLeaveForErrorWithWrongEmployee()
        {
            var login = new EmployeeViewModel() { Email = "siddhartha@promactinfo.com", Password = "sid.123" };
            var loginJson = JsonConvert.SerializeObject(login);
            var loginResult = client.PostAsync("Account/Login", new StringContent(loginJson, Encoding.UTF8, "application/json")).Result;
            var leave = new LeaveRequest() { Type = LMS.DomainModel.Models.Type.Sick, Reason = "Fever", Unit = DomainModel.Models.Unit.Full, EmployeeId = "57af3879-bad7-4403-8801-114435bc01d7", EndDate = DateTime.Now, Certificate = "Yes I don't have it", DoctorName = "Bimari Ram", Id = 3, Number = "1234567890", Condition = DomainModel.Models.Condition.Pending, CreatedOn = DateTime.Now };
            var leaveJson = JsonConvert.SerializeObject(leave);
            var response = client.PutAsync("api/SickLeaveUpdate", new StringContent(leaveJson, Encoding.UTF8, "application/json")).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var returnResponse = JsonConvert.DeserializeObject<LeaveRequest>(responseContent);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual(null, returnResponse);
        }
    }
}
