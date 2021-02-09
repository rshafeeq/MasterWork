using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey.Models
{
    public class ManageSurveys
    {
        
        [Required(ErrorMessage = "Please Enter Survey Title")]
        [Display(Name = " Enter Title")]
        public string SurveyTitle { get; set; }
        [Required(ErrorMessage = "Please Enter Survey Title Arabic")]
        [Display(Name = " Enter Title Arabic")]
        public string SurveyTitleArabic { get; set; }
        [Required(ErrorMessage = "Please Enter Survey Description")]
        [Display(Name = " Enter Description")]
        public string SurveyDescriptionEnglish { get; set; }
        [Required(ErrorMessage = "Please Enter Survey Description Arabic")]
        [Display(Name = " Enter Description Arabic")]
        public string SurveyDescriptionArabic { get; set; }
        [Required(ErrorMessage = "Please Select Start Date")]
        [Display(Name = " Select Start Date")]
        [DataType(DataType.Date)]
        public DateTime SurveyStartDate { get; set; }
        [Required(ErrorMessage = "Please Select End Date")]
        [Display(Name = " Select End Date")]
        [DataType(DataType.Date)]
        public DateTime SurveyEndDate { get; set; }
        [Required(ErrorMessage = "Please Select Active")]
        [Display(Name = " Select Active")]
        public int Active { get; set; }
        
        
    }
}