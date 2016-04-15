using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Test.Models
{
    public enum Condition
    {
        Pending,
        Escalated,
        Approved,
        Rejected
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
}
