using LMS.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LMS.Repository.Modules
{
    public interface IHolidayRepository
    {
        IEnumerable<HolidayType> GetHolidayList();
        HolidayType SelectHolidayById(int? holidayId);
        void AddHoliday(HolidayType holiday);
        void UpdateHoliday(HolidayType holiday);
        void DeleteHoliday(int? holidayId);
    }
}
