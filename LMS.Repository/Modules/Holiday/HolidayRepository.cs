using LMS.DomainModel.DataRepository;
using LMS.DomainModel.Models;
using LMS.Repository.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repository.Modules.Holiday
{
    public class HolidayRepository : IHolidayRepository
    {
        private IDataRepository<HolidayType> _holidayDataRepository;

        public HolidayRepository(IDataRepository<HolidayType> holidayDataRepository)
        {
            _holidayDataRepository = holidayDataRepository;
        }


        /// <summary>
        /// Fetches all data from table 'HolidayType'
        /// Authorization : Admin, Employee, Team Leader
        /// </summary>
        /// <returns>IEnumerable List</returns>
        public IEnumerable<HolidayType> GetHolidayList()
        {
            return _holidayDataRepository.List().ToList();
        }


        /// <summary>
        /// Finds a particular holiday by its id from table 'HolidayType'
        /// Authorization : Admin
        /// </summary>
        /// <param name="holidayId"></param>
        /// <returns>HolidayType object</returns>
        public HolidayType SelectHolidayById(int? holidayId)
        {
            return _holidayDataRepository.GetById(holidayId);
        }


        /// <summary>
        /// Creates a new holiday item in table 'HolidayType'
        /// Authorization : Admin
        /// </summary>
        /// <param name="holiday"></param>
        public void AddHoliday(HolidayType holiday)
        {
            holiday.CreatedOn = DateTime.Now;
            _holidayDataRepository.Insert(holiday);
        }


        /// <summary>
        /// Updates a pre-stored entity of table 'HolidayType'
        /// Authorization : Admin
        /// </summary>
        /// <param name="holiday"></param>
        public void UpdateHoliday(HolidayType holiday)
        {
            _holidayDataRepository.Update(holiday);
        }


        /// <summary>
        /// Deletes an entity from table 'HolidayType' by its id
        /// Authorization : Admin
        /// </summary>
        /// <param name="holidayId"></param>
        public void DeleteHoliday(int? holidayId)
        {
            _holidayDataRepository.Delete(holidayId);
        }
    }
}
