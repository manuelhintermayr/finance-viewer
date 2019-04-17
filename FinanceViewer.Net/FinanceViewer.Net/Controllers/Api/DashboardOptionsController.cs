using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceViewer.Net.Models.DbModels;

namespace FinanceViewer.Net.Controllers.Api
{
    public class DashboardOptionsController : Controller
    {
        private readonly financeviewerEntities _context;

        public DashboardOptionsController()
        {
            _context = new financeviewerEntities();
        }

        // GET: Dashboard Options
        public ActionResult Index()
        {
            Response.StatusCode = 400;
            return Content("Bad request. Please go to the main page and try again.");
        }
    }
}
