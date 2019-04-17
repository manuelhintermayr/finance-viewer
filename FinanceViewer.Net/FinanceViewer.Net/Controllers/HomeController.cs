using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceViewer.Net.Models.DbModels;

namespace FinanceViewer.Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly financeviewerEntities _context;

        public HomeController()
        {
            _context = new financeviewerEntities();
        }

        public ActionResult Index()
        {
            if (_context.UserIsLoggedInCorrectly(Session))
            {
                //return _context.RedirectToCorrectDashboard();
            }

            return View();
        }
    }
}