using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Models
{
    public class ManageQuestions
    {
        [Required(ErrorMessage = "Please Select Survey Name")]
        
        public IEnumerable<SelectListItem> Surveys { get; set; }
        [Display(Name = "Select Survey Name")]
        public int SurveyID { get; set; }
        [Required(ErrorMessage = "Please Select Question Group")]
        public IEnumerable<SelectListItem> Group { get; set; }
        [Display(Name = " Select Question Group")]
        public int QuestionGroupID { get; set; }

        [Required(ErrorMessage = "Please Select QuestionType")]
        public IEnumerable<SelectListItem> QuestionTypes { get; set; }
        [Display(Name = " Select Question Type")]
        public int QuestionTypeID { get; set; }
        
        [Required(ErrorMessage = "Please Enter Question")]
        [Display(Name = " Enter Question English")]
        public string QuestionE { get; set; }
        [Required(ErrorMessage = "Please Enter Question Arabic")]
        [Display(Name = " Enter Question Arabic")]
        public string QuestionA { get; set; }
        [Required(ErrorMessage = "Please Enter Answer 1")]
        [Display(Name = " Enter Answer 1 English")]
        public string Answer1E { get; set; }
        [Required(ErrorMessage = "Please Enter Answer 2")]
        [Display(Name = " Enter Answer 2 English")]
        public string Answer2E { get; set; }
        [Required(ErrorMessage = "Please Enter Answer 3")]
        [Display(Name = " Enter Answer 3 English ")]
        public string Answer3E { get; set; }
        [Required(ErrorMessage = "Please Enter Answer 4")]
        [Display(Name = " Enter Answer 4 English ")]
        public string Answer4E { get; set; }
        [Required(ErrorMessage = "Please Enter Answer 5")]
        [Display(Name = " Enter Answer 5 English")]
        public string Answer5E { get; set; }
        [Required(ErrorMessage = "Please Enter Answer 1 Arabic")]
        [Display(Name = " Enter Answer 1 Arabic ")]
        public string Answer1A { get; set; }
        [Required(ErrorMessage = "Please Enter Answer 2 Arabic")]
        [Display(Name = " Enter Answer 2 Arabic ")]
        public string Answer2A { get; set; }
        [Required(ErrorMessage = "Please Enter Answer 3 Arabic")]
        [Display(Name = " Enter Answer 3 Arabic ")]
        public string Answer3A { get; set; }
        [Required(ErrorMessage = "Please Enter Answer 4 Arabic")]
        [Display(Name = " Enter Answer 4 Arabic ")]
        public string Answer4A { get; set; }
        [Required(ErrorMessage = "Please Enter Answer 5 Arabic")]
        [Display(Name = " Enter Answer 5 Arabic ")]
        public string Answer5A { get; set; }

        public string ErrorMessage { get; set; }



    }
}