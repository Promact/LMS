using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.ApplicationClasses
{
    public class LeaveDetailsAC
    {
        public double SickLeaveTaken { get; set; }
        public double CasualLeaveTaken { get; set; }
        public double SickLeaveLeft { get; set; }
        public double CasualLeaveLeft { get; set; }
        public double Compensation { get; set; }
    }
}
