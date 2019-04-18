using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace FinanceViewer.Net.Controllers.Api
{
    public static class ControllerExtensions
    {
        public static JObject GetJsonPostObjectFromRequest(this Controller m)
        {
            //used https://stackoverflow.com/questions/6362781/how-to-get-raw-request-body-in-asp-net
            string req_txt;
            using (StreamReader reader = new StreamReader(m.Request.InputStream))
            {
                req_txt = reader.ReadToEnd();
            }

            JObject jObject = JObject.Parse(req_txt);
            return jObject;
        }
    }
}