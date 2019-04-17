using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using FinanceViewer.Net.Models.AnswerModels;
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
            JObject POST = GetJsonPostObjectFromRequest();     

            if (Request.HttpMethod == "POST" 
                && POST["id"]!=null
                && POST["username"]!=null
                && POST["firstname"] != null
                && POST["lastname"] != null
                && POST["isLocked"] != null
                && POST["password"] != null
                && POST["username"].ToString() != ""
                && POST["username"].ToString() != " "
                && POST["firstname"].ToString() != ""
                && POST["firstname"].ToString() != " "
                && POST["lastname"].ToString() != ""
                && POST["lastname"].ToString() != " "
                && POST["isLocked"].ToString() != ""
                && POST["isLocked"].ToString() != " "
                && POST["password"].ToString() != ""
                && POST["password"].ToString() != " "
                && POST["username"].ToString() != AdminCredentials.Username)
            {
                string username = _context.SQLEscape(POST["username"].ToString());
                string firstname = _context.SQLEscape(POST["firstname"].ToString());
                string lastname = _context.SQLEscape(POST["lastname"].ToString());
                bool isLocked = Boolean.Parse(_context.SQLEscape(POST["isLocked"].ToString()));
                string password = Crypto.HashPassword(Crypto.SHA256(POST["password"].ToString()));

                if (!username.Contains(" "))
                {
                    fv_users user = new fv_users()
                    {
                        u_name = username,
                        u_password = password,
                        u_isLocked = isLocked ? 1:0,
                        u_firstName = firstname,
                        u_lastName = lastname
                    };
                    var newYearOne = new fv_years()
                    {
                        y_u_name = username,
                        y_year = DateTime.Today.Year.ToString()
                    };
                    var newYearTwo = new fv_years()
                    {
                        y_u_name = username,
                        y_year = ((DateTime.Today.Year) + 1).ToString()
                    };


                    var newUser = _context.fv_users.Add(user);
                    _context.fv_years.Add(newYearOne);
                    _context.fv_years.Add(newYearTwo);

                    _context.SaveChanges();

                    NewUser userResult = new NewUser()
                    {
                        id = POST["id"].ToString(),
                        username = newUser.u_name,
                        origianlUsername = newUser.u_name,
                        firstname = newUser.u_firstName,
                        lastname = newUser.u_lastName,
                        isLocked = newUser.u_isLocked,
                        years = new int[]{ DateTime.Today.Year, DateTime.Today.Year+1 }
                    };

                    Response.StatusCode = 200;
                    return Json(userResult, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Response.StatusCode = 400;
                    return Content("Username should not contain whitespaces.");
                }
            }
            else
            {
                Response.StatusCode = 400;
                return Content("Not all values are set.");  
            }

            throw new NotImplementedException();
        }

        private JObject GetJsonPostObjectFromRequest()
        {
            //used https://stackoverflow.com/questions/6362781/how-to-get-raw-request-body-in-asp-net
            string req_txt;
            using (StreamReader reader = new StreamReader(Request.InputStream))
            {
                req_txt = reader.ReadToEnd();
            }

            JObject jObject = JObject.Parse(req_txt);
            return jObject;
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
