using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using FinanceViewer.Net.Models.AnswerModels;
using FinanceViewer.Net.Models.DbModels;
using FinanceViewer.Net.Models.GetModels;

namespace FinanceViewer.Net.Controllers.Api
{
    public class LoginController : Controller
    {
        private readonly financeviewerEntities _context;

        public LoginController()
        {
            _context = new financeviewerEntities();
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Post([FromBody]LoginData loginData)
        {
            if (loginData == null || loginData.password == null || loginData.username == null)
            {
                Response.StatusCode = 400;
                return Content("Bad request. Please go to the main page and try again.");
            }

            if (_context.CheckCorrectUser(loginData))
            {
                Session["m_user"] = loginData.username;
                Session["m_password"] = loginData.password;
                // ^^ going to update this for security later

                Response.StatusCode = 200;
                return Json(new UserPrefs()
                {
                    name = _context.GetFirstNameForUsername(loginData.username),
                    url = _context.GetUrlForUser(loginData.username)
                });
            }
            else
            {
                Response.StatusCode = 400;
                return Content($"Login invalid {{user: {loginData.username}}}");
            }
        }
    }
}
