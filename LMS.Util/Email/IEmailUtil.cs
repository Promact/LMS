using LMS.DomainModel.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Util.Email
{
    public interface IEmailUtil
    {
        void ApplyLeave(string employeeEmail,string body, List<string> teamLeaderEmail);
        void ApproveAndReject(string teamLeaderEmail, string body, string employeeEmail);
    }
}
