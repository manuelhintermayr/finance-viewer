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

        public LoginController()
        {
            _context = new FinanceviewerContext();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LoginData loginData)
        {
            if (loginData == null)
            {
                return BadRequest("Bad request. Please go to the main page and try again.");
            }

            if (_context.CheckCorrectUser(loginData))
            {
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