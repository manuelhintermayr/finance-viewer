using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceViewer.Net.Models.DbModels;

namespace FinanceViewer.Net.Controllers
{
    public class DashboardController : Controller
    {
        private readonly financeviewerEntities _context;

        public DashboardController()
        {
            _context = new financeviewerEntities();
        }

        // GET: Dashboard
        public ActionResult Dashboard()
        {
            if (!_context.UserIsLoggedInCorrectly(Session))
            {
                return Redirect("/logout.php");
            }

            return View();
        }
    }
}
