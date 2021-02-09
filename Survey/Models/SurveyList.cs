using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Models
{
    public class SurveyList
    {
        public int SurveyID { get; set; }
        public string SurveyTitle { get; set; }
        
        public string SurveyTitleArabic { get; set; }

        public string SurveyDescriptionEnglish { get; set; }

        public string SurveyDescriptionArabic { get; set; }

        
    }
}