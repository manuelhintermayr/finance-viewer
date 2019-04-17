using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceViewer.Net.Models.AnswerModels
{
    public class NewUser
    {
        public string id;
        public string username;
        public string origianlUsername;
        public string firstname;
        public string lastname;
        public int isLocked;
        public int[] years;
    }
}