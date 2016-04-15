using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DomainModel.Identity
{
    public class ApplicationRoleStore : RoleStore<IdentityRole>
    {
        public ApplicationRoleStore(DbContext dbContext)
            : base(dbContext)
        {

        }

    }
}
