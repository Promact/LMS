using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.Models
{
    /// <summary>
    /// Half Reject is used for sick leave if rejected by first chance
    /// </summary>
        public enum Condition
        {
            Pending,
            Escalated,
            Approved,
            Rejected,
            HalfRejected,
            Cancel
        }
      public enum Designation
      {
          User,
          TeamLeader,
          Admin
      }
        public enum Unit
        {
            FirstHalf,
            SecondHalf,
            Full
        }
        public enum Type
        {
            Sick,
            Compensation,
            Casual,
            Paternity,
            Maternity
        }
        public enum TypeOfHoliday
        {
            General,
            Restricted
        }
    }

