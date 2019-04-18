using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
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

        private ActionResult GetUsers()
        {
            var result = _context.GetUsers();

            Response.StatusCode = 200;
            return Json(result, JsonRequestBehavior.AllowGet);
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
                    
                    var newUser = _context.fv_users.Add(user);
                    _context.AddNewYearForUser(DateTime.Today.Year, username, false);
                    _context.AddNewYearForUser((DateTime.Today.Year + 1), username, false);

                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        if (ex is DbEntityValidationException || ex is DbUpdateException || ex is SqlException)
                        {
                            Response.StatusCode = 400;
                            return Content("Could not create a new user. SQL Execution failed.");
                        }

                        throw;
                    }


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
        }

        private ActionResult RemoveUser()
        {
            JObject POST = GetJsonPostObjectFromRequest();

            if (POST["username"]!=null)
            {
                string username = _context.SQLEscape(POST["username"].ToString());


                //Check if view was found
                List<fv_views> views = null;
                try
                {
                    views = _context.fv_views.Where(m => m.v_u_name == username).ToList();
                }
                catch (InvalidOperationException) { }
                if (views != null)
                {
                    _context.fv_views.RemoveRange(views);

                    //Deleting Years
                    var years = _context.GetYearsForUser(username);
                    bool errorWhileDeletingYears = false;
                    foreach (int year in years) {
                        if (!_context.RemoveYearByYearAndUsername(year, username, false))
                        {
                            errorWhileDeletingYears = true;
                        }
                    }

                    if (!errorWhileDeletingYears) {

                        //Check if user was found
                        fv_users finalUser = null;
                        try
                        {
                            finalUser = _context.fv_users.Single(m => m.u_name == username);
                        }
                        catch (InvalidOperationException) { }
                        if (finalUser == null)
                        {
                            Response.StatusCode = 400;
                            return Content($"Could not find the user to delete {username}.");
                        }

                        //Deleting the user
                        _context.fv_users.Remove(finalUser);

                        try
                        {
                            _context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            if (ex is DbEntityValidationException || ex is DbUpdateException || ex is SqlException)
                            {
                                Response.StatusCode = 400;
                                return Content($"Could not delete user {username}. SQL Execution failed.");
                            }

                            throw;
                        }


                        Response.StatusCode = 200;
                        return Json(new { message = "User deleted." },
                            JsonRequestBehavior.AllowGet);

                    } else {
                        Response.StatusCode = 400;
                        return Content($"Could not delete years for the user {username}.");
                    }

                }
                else
                {
                    Response.StatusCode = 400;
                    return Content($"Could not delete views for the user {username}.");
                }

            }
            else
            {
                Response.StatusCode = 400;
                return Content("Username is not set.");
            }
        }

        private ActionResult UpdateUser()
        {
            JObject POST = GetJsonPostObjectFromRequest();

            if (POST["username"] != null
                && POST["firstname"] != null
                && POST["lastname"] != null
                && POST["isLocked"] != null
                && POST["username"].ToString() != ""
                && POST["username"].ToString() != " "
                && POST["firstname"].ToString() != ""
                && POST["firstname"].ToString() != " "
                && POST["lastname"].ToString() != ""
                && POST["lastname"].ToString() != " "
                && POST["username"].ToString() != AdminCredentials.Username) {

                string username = _context.SQLEscape(POST["username"].ToString());
                string firstname = _context.SQLEscape(POST["firstname"].ToString());
                string lastname = _context.SQLEscape(POST["lastname"].ToString());
                bool isLocked = Boolean.Parse(_context.SQLEscape(POST["isLocked"].ToString()));

                //Check if user was found
                fv_users finalUser = null;
                try
                {
                    finalUser = _context.fv_users.Single(m => m.u_name == username);
                }
                catch (InvalidOperationException) { }
                if (finalUser == null)
                {
                    Response.StatusCode = 400;
                    return Content($"Could not find the user to update {username}.");
                }

                //updating the user
                finalUser.u_firstName = firstname;
                finalUser.u_lastName = lastname;
                finalUser.u_isLocked = isLocked ? 1 : 0;

                _context.fv_users.AddOrUpdate(finalUser);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex is DbEntityValidationException || ex is DbUpdateException || ex is SqlException)
                    {
                        Response.StatusCode = 400;
                        return Content($"Could not update the user {username}. SQL Execution failed.");
                    }

                    throw;
                }

                Response.StatusCode = 200;
                return Json(new
                    {
                        username = finalUser.u_name,
                        firstname = finalUser.u_firstName,
                        lastname = finalUser.u_lastName,
                        isLocked = finalUser.u_isLocked != 0
                    },
                    JsonRequestBehavior.AllowGet);
            }
            else {
                Response.StatusCode = 400;
                return Content("Not all values are set.");
             }
        }

        private ActionResult SetPassword()
        {
            JObject POST = GetJsonPostObjectFromRequest();

            if (POST["username"]!=null
                && POST["newPassword"] != null
                && POST["username"].ToString() != ""
                && POST["username"].ToString() != " "
                && POST["newPassword"].ToString() != ""
                && POST["newPassword"].ToString() != " ")
            {
                string username = _context.SQLEscape(POST["username"].ToString());
                string newPassword = Crypto.HashPassword(Crypto.SHA256(POST["newPassword"].ToString()));

                //Check if user was found
                fv_users finalUser = null;
                try
                {
                    finalUser = _context.fv_users.Single(m => m.u_name == username);
                }
                catch (InvalidOperationException) { }
                if (finalUser == null)
                {
                    Response.StatusCode = 400;
                    return Content($"Could not find the user to update password: {username}.");
                }

                //updating the user
                finalUser.u_password = newPassword;
                _context.fv_users.AddOrUpdate(finalUser);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex is DbEntityValidationException || ex is DbUpdateException || ex is SqlException)
                    {
                        Response.StatusCode = 400;
                        return Content($"Could not set password for user {username}. SQL Execution failed.");
                    }

                    throw;
                }

                Response.StatusCode = 200;
                return Json(new { message = "Password set." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = 400;
                return Content("Not all values are set.");
            }
        }

        private ActionResult AddYear()
        {
            JObject POST = GetJsonPostObjectFromRequest();

            if (POST["username"] != null
                && POST["year"] != null
                && POST["username"].ToString() != ""
                && POST["username"].ToString() != " "
                && POST["year"].ToString() != ""
                && POST["year"].ToString() != " ") 
            {
                string username = _context.SQLEscape(POST["username"].ToString());
                string yearString = _context.SQLEscape(POST["year"].ToString());

                if (int.TryParse(yearString, out int newYear) && newYear > 0) 
                {
                    try
                    {
                        _context.AddNewYearForUser(newYear, username);
                    }
                    catch (Exception ex)
                    {
                        if (ex is DbEntityValidationException || ex is DbUpdateException || ex is SqlException)
                        {
                            Response.StatusCode = 400;
                            return Content("Could not create a new year entry. SQL Execution failed.");
                        }

                        throw;
                    }

                    Response.StatusCode = 200;
                    return Json(new { newYear }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Response.StatusCode = 400;
                    return Content("Value for new year is not valid.");
                }
            }
            else
            {
                Response.StatusCode = 400;
                return Content("Not all values are set.");
            }
        }


        private ActionResult RemoveYear()
        {
            JObject POST = GetJsonPostObjectFromRequest();

            if (POST["username"] != null
                && POST["year"] != null
                && POST["username"].ToString() != ""
                && POST["username"].ToString() != " "
                && POST["year"].ToString() != ""
                && POST["year"].ToString() != " ")
            {
                string username = _context.SQLEscape(POST["username"].ToString());
                string yearString = _context.SQLEscape(POST["year"].ToString());

                if (int.TryParse(yearString, out int year) && year > 0)
                {
                    if (_context.GetYearsForUser(username).Length> 1)
                    {
                        //Check if view was found
                        List<fv_views> views = null;
                        try
                        {
                            views = _context.fv_views.Where(m => m.v_u_name == username && m.v_y_year == yearString).ToList();
                        }
                        catch (InvalidOperationException) { }

                        if (views != null)
                        {
                            _context.fv_views.RemoveRange(views);

                            //Deleting years:
                            try
                            {
                                _context.RemoveYearByYearAndUsername(year, username);
                            }
                            catch (Exception ex)
                            {
                                if (ex is DbEntityValidationException || ex is DbUpdateException || ex is SqlException)
                                {
                                    Response.StatusCode = 400;
                                    return Content("Could not delete the year entry. SQL Execution failed.");
                                }

                                throw;
                            }

                            Response.StatusCode = 200;
                            return Json(new { message = "Year deleted." }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            Response.StatusCode = 400;
                            return Content($"Could not delete views for the user {username} and year {year}.");
                        }
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return Content("There must be at least on year left.");
                    }
                }
                else
                {
                    Response.StatusCode = 400;
                    return Content("Value for year is not valid.");
                }
            }
            else
            {
                Response.StatusCode = 400;
                return Content("Not all values are set.");
            }
        }

        private ActionResult SetView()
        {
            JObject POST = GetJsonPostObjectFromRequest();

            if (POST["username"]!=null)
            {
                Session["m_view_username"] = POST["username"].ToString();

                Response.StatusCode = 200;
                return Json(new { message = "Username for view set." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = 400;
                return Content("Username is not set.");
            }
        }

        private ActionResult ActionNotSupported(string action)
        {
            Response.StatusCode = 400;
            return Content($"Action {action} is not supported.");
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
        
    }
}
