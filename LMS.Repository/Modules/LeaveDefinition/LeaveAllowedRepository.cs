using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DomainModel.DataRepository;
using LMS.DomainModel.Models;

namespace LMS.Repository.Modules.LeaveDefinition
{
    public class LeaveAllowedRepository : ILeaveAllowedRepository
    {
        private readonly IDataRepository<LeaveAllowed> _leaveAllowedDataRepository;
        public LeaveAllowedRepository(IDataRepository<LeaveAllowed> leaveAllowedDataRepository)
        {
            _leaveAllowedDataRepository = leaveAllowedDataRepository;
        }

        /// <summary>
        /// Get list of all leaves
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LeaveAllowed> GetAllLeaveTypes()
        {
            return _leaveAllowedDataRepository.List().ToList();
        }

        /// <summary>
        /// Get leave by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LeaveAllowed GetLeaveTypeById(int id)
        {
            return _leaveAllowedDataRepository.GetById(id);
        }

        /// <summary>
        /// Create new leave along with number of days allowed
        /// </summary>
        /// <param name="leave"></param>
        public void Add(DomainModel.Models.LeaveAllowed leave)
        {
            leave.CreatedOn = DateTime.Now;
            _leaveAllowedDataRepository.Insert(leave);
            _leaveAllowedDataRepository.Save();
        }

        /// <summary>
        /// Update leave type
        /// </summary>
        /// <param name="leave"></param>
        public void Update(DomainModel.Models.LeaveAllowed leave)
        {
            _leaveAllowedDataRepository.Update(leave);
            _leaveAllowedDataRepository.Save();
        }
    }
}
