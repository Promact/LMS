using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace LMS.Core.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Index");
            }

            else
            {
                if(User.IsInRole("Admin"))
                {
                    return View("Admin");
                }

                else if(User.IsInRole("TeamLeader"))
                {
                    return View("TeamLeader");
                }

                else
                {
                    return View("Employee");
                }
            }

            
        }
    }
}