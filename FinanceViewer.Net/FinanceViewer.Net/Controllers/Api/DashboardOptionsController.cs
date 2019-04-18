using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceViewer.Net.Models.AnswerModels;
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
            var viewsFromDb = _context.GetViewsForUserAndYear(username, year);

            List<Views> convertedViews = new List<Views>();

            foreach (var view in viewsFromDb)
            {
                convertedViews.Add(GetViewArrayByRowResult(view));
            }
        
            Response.StatusCode = 200;
            return Json(new
                    {
                        year,
                        username,
                        data = convertedViews
                    },
                    JsonRequestBehavior.AllowGet);
        }

        private Views GetViewArrayByRowResult(fv_views viewFromDb)
        {
            Views.ViewData yearData = new Views.ViewData() {
                    january = viewFromDb.v_month_01,
                    february = viewFromDb.v_month_02,
                    march = viewFromDb.v_month_03,
                    april = viewFromDb.v_month_04,
                    may = viewFromDb.v_month_05,
                    june = viewFromDb.v_month_06,
                    july = viewFromDb.v_month_07,
                    august = viewFromDb.v_month_08,
                    september = viewFromDb.v_month_09,
                    october = viewFromDb.v_month_10,
                    november = viewFromDb.v_month_11,
                    december = viewFromDb.v_month_12,
                    tempJanuary = viewFromDb.v_month_01,
                    tempFebruary = viewFromDb.v_month_02,
                    tempMarch = viewFromDb.v_month_03,
                    tempApril = viewFromDb.v_month_04,
                    tempMay = viewFromDb.v_month_05,
                    tempJune = viewFromDb.v_month_06,
                    tempJuly = viewFromDb.v_month_07,
                    tempAugust = viewFromDb.v_month_08,
                    tempSeptember = viewFromDb.v_month_09,
                    tempOctober = viewFromDb.v_month_10,
                    tempNovember = viewFromDb.v_month_11,
                    tempDecember = viewFromDb.v_month_12
                };

            Views view = new Views()
            {
                count = viewFromDb.v_id,
                name = viewFromDb.v_name,
                description = viewFromDb.v_description,
                id = viewFromDb.v_html_id,
                notes = viewFromDb.v_notes,
                profile_url = viewFromDb.v_profile_url,
                data = yearData
            };
              
            return view;
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
