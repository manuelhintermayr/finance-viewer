using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinanceViewer.Net.Controllers.Api
{
    public class DashboardOptionsController : Controller
    {
        // GET: Dashboard Options
        public ActionResult Index()
        {
            Response.StatusCode = 400;
            return Content("Bad request. Please go to the main page and try again.");
        }
    }
}
