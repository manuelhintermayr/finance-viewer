using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceViewer.Net.Models.AnswerModels;
using FinanceViewer.Net.Models.DbModels;

namespace FinanceViewer.Net.Controllers.Api
{
    public class BasicOptionsController : Controller
    {
        private readonly financeviewerEntities _context;

        public BasicOptionsController()
        {
            _context = new financeviewerEntities();
        }

        // GET: Basic Options
        public ActionResult Index()
        {
            if (!_context.UserIsLoggedInCorrectly(Session))
            {
                Response.StatusCode = 403;
                return Content("Not logged in.");
            }

            var action = Request.QueryString["action"];
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "getInfo":
                        return GetInfo();
                    case "setViewYear":
                        return SetViewYear();
                    default:
                        return ActionNotSupported(action);
                }
            }
            else
            {
                Response.StatusCode = 400;
                return Content("Bad request.");
            }
        }

        private ActionResult ActionNotSupported(string action)
        {
            Response.StatusCode = 400;
            return Content($"Action {action} is not supported.");
        }

        private ActionResult SetViewYear()
        {
            throw new NotImplementedException();
        }

        private ActionResult GetInfo()
        {
            string username = Session["m_user"].ToString();

            Response.StatusCode = 200;
            return Json(new UserInfo()
            {
                name = _context.GetFirstNameForUsername(username),
                isAdmin = _context.UserIsAdmin(username),
                years = _context.GetYearsForUser(username)
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
