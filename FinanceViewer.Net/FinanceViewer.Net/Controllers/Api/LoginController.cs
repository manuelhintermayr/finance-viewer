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
            //var x = HttpContext.Session.GetString("m_user");
            //var xyz = _session.GetString("m_user");
            //var la = HttpContext.Session.Keys;

            if (loginData == null || loginData.password == null || loginData.username == null)
            {
                Response.StatusCode = 400;
                return Content("Bad request. Please go to the main page and try again.");
            }

            if (_context.CheckCorrectUser(loginData))
            {
                //HttpContext.Session.SetString("m_user", loginData.username);
                //HttpContext.Session.SetString("m_password", loginData.password);
                // ^^ going to update this for security later

                //var asd = HttpContext.Session;
                //var a = HttpContext.Session.Id;
                //var f = HttpContext.Session.Keys;
                //var y = HttpContext.Session.Get("m_user");
                //var xy = HttpContext.Session.GetString("m_user");
                //var l = HttpContext.Session.Keys;

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


        //// POST: Login/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Login/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Login/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
