using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Models
{
    public class SurveyTitle
    {
        public List<SelectListItem> Surveys { get; set; }
        public int SurveyID { get; set; }
        public string SurveyName { get; set; }
    }
}