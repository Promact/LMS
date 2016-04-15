using LMS.DomainModel.Models;
using LMS.Repository.Modules;
using LMS.Repository.Modules.Holiday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace LMS.Core.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/HolidayType")]
    public class HolidayTypeController : ApiController
    {
        private readonly IHolidayRepository _holidayRepository;
        public HolidayTypeController(IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }

        // List of all holidays
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAllHoliday()
        {
            return Ok(_holidayRepository.GetHolidayList());
        }



        //  Finding a holiday details by id
        [HttpGet]
        [Route("Details/{Id}")]
        public IHttpActionResult FindHolidayById(int Id)
        {
            var holiday = _holidayRepository.SelectHolidayById(Id);
            if (holiday == null)
            {
                return NotFound();
            }
            return Ok(holiday);
        }


        // Adding new holiday
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult AddNewHoliday(HolidayType holiday)
        {
            //if (holiday == null)
            //{
            //    return NotFound();
            //}
            try
            {
                if (ModelState.IsValid)
                {
                    _holidayRepository.AddHoliday(holiday);
                }
                return Ok(holiday);
            }
            catch (Exception)
            {
                
                return BadRequest();
            }
            
        }
        

        // Delete a holiday by id
        [HttpDelete]
        [Route("Delete/{Id}")]
        public IHttpActionResult DeleteHoliday(int Id)
        {
            if (_holidayRepository.SelectHolidayById(Id) == null)
            {
                return NotFound();
            }
            try
            {
                _holidayRepository.DeleteHoliday(Id);
                return Ok();
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }


        // Update a holiday
        [HttpPut]
        [Route("Edit")]
        public IHttpActionResult EditHoliday(HolidayType holiday)
        {
            if (holiday == null)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _holidayRepository.UpdateHoliday(holiday);
                }
                return Ok(holiday);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
