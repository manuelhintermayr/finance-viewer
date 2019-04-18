using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinanceViewer.Net.Models.AnswerModels
{
    public class Views
    {
        public int count;
        public string name;
        public string description;
        public string id;
        public string notes;
        public string profile_url;
        public ViewData data;

        public class ViewData
        {
            public string january;
            public string february;
            public string march;
            public string april;
            public string may;
            public string june;
            public string july;
            public string august;
            public string september;
            public string october;
            public string november;
            public string december;
            public string tempJanuary;
            public string tempFebruary;
            public string tempMarch;
            public string tempApril;
            public string tempMay;
            public string tempJune;
            public string tempJuly;
            public string tempAugust;
            public string tempSeptember;
            public string tempOctober;
            public string tempNovember;
            public string tempDecember;
        }
    }
}