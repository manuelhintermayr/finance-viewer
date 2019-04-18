using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceViewer.Net.Models.AnswerModels
{
    public class User
    {
        public string username;
        public string firstname;
        public string lastname;
        public bool isLocked;
        public int[] years;
    }
}