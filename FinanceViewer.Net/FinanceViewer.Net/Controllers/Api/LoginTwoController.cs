using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using FinanceViewer.Net.Models.GetModels;

namespace FinanceViewer.Net.Controllers.Api
{
    [System.Web.Mvc.Route("/login.php")]
    public class LoginTwoController : Controller
    {
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Post([FromBody] LoginData loginData)
        {
            Response.StatusCode = 200;
            return Json("Bad request. Please go to the main page and try again.");
        }
    }
}
