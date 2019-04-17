using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceViewer.Net.Models.DbModels;

namespace FinanceViewer.Net.Controllers.Api
{
    public class DashboardOptionsController : Controller
    {
        private readonly financeviewerEntities _context;
        private string username = "";
        private int year = 0;

        public DashboardOptionsController()
        {
            _context = new financeviewerEntities();
        }

        // GET: Dashboard Options
        public ActionResult Index()
        {
            if (!_context.UserIsLoggedInCorrectly(Session))
            {
                Response.StatusCode = 403;
                return Content("Not logged in.");
            }

            SetUsernameForView();
            SetYearForView();

            var action = Request.QueryString["action"];
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "getViews":
                        return GetViews();
                    case "addView":
                        return AddView();
                    case "removeView":
                        return RemoveView();
                    case "updateViewMonth":
                        return UpdateViewMonth();
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

        private void SetUsernameForView()
        {
            username = Session["m_user"].ToString();

            if (_context.UserIsAdmin(Session["m_user"].ToString()) && Session["m_view_username"] != null)
            {
                username = Session["m_view_username"].ToString();
            }
        }

        private void SetYearForView()
        {
            var years = _context.GetYearsForUser(username);

            if (Session["m_view_year"]!=null)
            {
                if (years.Contains((int)Session["m_view_year"]))
                {//check if set year is valid
                    year = (int) Session["m_view_year"];
                }
                else
                {
                    SetYearFirstValueOfArray(years);
                }
            }
            else
            {
                SetYearFirstValueOfArray(years);
            }
        }

        private void SetYearFirstValueOfArray(int[] years)
        {
            if (years.Length != 0)
            {
                year = years[0];
            }
        }

        private ActionResult GetViews()
        {
            throw new NotImplementedException();
        }

        private ActionResult AddView()
        {
            throw new NotImplementedException();
        }

        private ActionResult RemoveView()
        {
            throw new NotImplementedException();
        }

        private ActionResult UpdateViewMonth()
        {
            throw new NotImplementedException();
        }

        private ActionResult ActionNotSupported(string action)
        {
            Response.StatusCode = 400;
            return Content($"Action {action} is not supported.");
        }
    }
}
