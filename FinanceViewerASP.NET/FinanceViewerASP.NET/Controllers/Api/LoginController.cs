using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceViewerASP.NET.Models;
using FinanceViewerASP.NET.Models.AnswerModels;
using FinanceViewerASP.NET.Models.DbModels;
using FinanceViewerASP.NET.Models.GetModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceViewerASP.NET.Controllers.Api
{
    [Route("/login.php")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly FinanceviewerContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        //public LoginController(IHttpContextAccessor httpContextAccessor)
        //{
        //    _context = new FinanceviewerContext();
        //    _httpContextAccessor = httpContextAccessor;
        //}

        public LoginController()
        {
            _context = new FinanceviewerContext();
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LoginData loginData)
        {
            var x = HttpContext.Session.GetString("m_user");
            //var xyz = _session.GetString("m_user");
            var la = HttpContext.Session.Keys;
            
            if (loginData == null)
            {
                return BadRequest("Bad request. Please go to the main page and try again.");
            }

            if (_context.CheckCorrectUser(loginData))
            {
                HttpContext.Session.SetString("m_user", loginData.username);
                HttpContext.Session.SetString("m_password", loginData.password);

                var asd = HttpContext.Session;
                var a = HttpContext.Session.Id;
                var f = HttpContext.Session.Keys;
                var y = HttpContext.Session.Get("m_user");
                var xy = HttpContext.Session.GetString("m_user");
                var l = HttpContext.Session.Keys;
                return Ok(new UserPrefs()
                {
                    name = _context.GetFirstNameForUsername(loginData.username),
                    url = _context.GetUrlForUser(loginData.username)
                });
            }
            else
            {
                return BadRequest($"Login invalid {{user: {loginData.username}}}");
            }

        }
    }
}