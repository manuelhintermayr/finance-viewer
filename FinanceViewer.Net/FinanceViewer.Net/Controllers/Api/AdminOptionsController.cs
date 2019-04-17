using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using FinanceViewer.Net.Models.DbModels;
using FinanceViewer.Net.Models.GetModels;
using Newtonsoft.Json.Linq;

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
                        return GetUsers();
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
            if (Request.HttpMethod == "POST" 
                && Request.Params["id"]!=null
                && Request.Params["username"]!=null
                && Request.Params["firstname"] != null
                && Request.Params["lastname"] != null
                && Request.Params["isLocked"] != null
                && Request.Params["password"] != null
                && Request.Params["username"] != ""
                && Request.Params["username"] != " "
                && Request.Params["firstname"] != ""
                && Request.Params["firstname"] != " "
                && Request.Params["lastname"] != ""
                && Request.Params["lastname"] != " "
                && Request.Params["isLocked"] != ""
                && Request.Params["isLocked"] != " "
                && Request.Params["password"] != ""
                && Request.Params["password"] != " "
                && Request.Params["username"] !=AdminCredentials.Username)
            {
                string username = _context.SQLEscape(Request.Params["username"]);
                string firstname = _context.SQLEscape(Request.Params["firstname"]);
                string lastname = _context.SQLEscape(Request.Params["lastname"]);
                bool isLocked = _context.SQLEscape(Request.Params["isLocked"]) != "0";
                string password = Crypto.HashPassword(Crypto.SHA256(Request.Params["password"]));
                int[] years = new int[]{2018, 2019};
            }
            else
            {
                Response.StatusCode = 400;
                return Content("Not all values are set.");  
            }

            throw new NotImplementedException();
        }

        private ActionResult SetView()
        {
            throw new NotImplementedException();
        }

        private ActionResult GetUsers()
        {
            var result = _context.GetUsers();
            
            Response.StatusCode = 200;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
