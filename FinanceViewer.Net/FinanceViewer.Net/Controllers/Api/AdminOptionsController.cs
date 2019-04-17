using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinanceViewer.Net.Models.DbModels;

namespace FinanceViewer.Net.Controllers.Api
{
    public class AdminOptionsController : Controller
    {
        private readonly financeviewerEntities _context;

        public AdminOptionsController()
        {
            _context = new financeviewerEntities();
        }

        // GET: Admin Options
        public ActionResult Index()
        {
            if (!_context.UserIsLoggedInCorrectly(Session))
            {
                Response.StatusCode = 403;
                return Content("Not logged in.");
            }

            if (!_context.UserIsAdmin(Session["m_user"].ToString()))
            {
                Response.StatusCode = 403;
                return Content("Logged in user is not admin.");
            }

            var action = Request.QueryString["action"];
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "getUsers":
                        return GetUser();
                    case "setView":
                        return SetView();
                    case "addUser":
                        return AddUser();
                    case "removeUser":
                        return RemoveUser();
                    case "updateUser":
                        return UpdateUser();
                    case "setPassword":
                        return SetPassword();
                    case "addYear":
                        return AddYear();
                    case "removeYear":
                        return RemoveYear();
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

        private ActionResult RemoveYear()
        {
            throw new NotImplementedException();
        }

        private ActionResult AddYear()
        {
            throw new NotImplementedException();
        }

        private ActionResult SetPassword()
        {
            throw new NotImplementedException();
        }

        private ActionResult UpdateUser()
        {
            throw new NotImplementedException();
        }

        private ActionResult RemoveUser()
        {
            throw new NotImplementedException();
        }

        private ActionResult AddUser()
        {
            throw new NotImplementedException();
        }

        private ActionResult SetView()
        {
            throw new NotImplementedException();
        }

        private ActionResult GetUser()
        {
            var result = _context.GetUsers();
            
            Response.StatusCode = 200;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
