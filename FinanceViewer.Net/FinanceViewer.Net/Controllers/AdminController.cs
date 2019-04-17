using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceViewer.Net.Models.DbModels;

namespace FinanceViewer.Net.Controllers
{
    public class AdminController : Controller
    {
        private readonly financeviewerEntities _context;

        public AdminController()
        {
            _context = new financeviewerEntities();
        }

        // GET: Admin
        public ActionResult Admin()
        {
            if (!_context.UserIsLoggedInCorrectly(Session))
            {
                return Redirect("/logout.php");
            }

            return View();
        }
    }
}
