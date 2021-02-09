using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Quiz.Models
{
    public class ManageQuiz
    {
        public int QuizId { get; set; }
        [Required(ErrorMessage = "Please enter QuizName")]
        [Display(Name = "Enter Quiz Name")]
        public string QuizName { get;set;}
        [Required(ErrorMessage = "Please enter No of Levels")]
        [Display(Name = "Enter Levels")]
        public int NoOfLevels { get; set; }
        [Required(ErrorMessage = "Please enter No of Level1 Qustions")]
        [Display(Name = "Enter Level1 Questions")]
        public int NoOfQuestionsLevel1 { get; set; }
        [Required(ErrorMessage = "Please enter No of Level2 Qustions")]
        [Display(Name = "Enter Level2 Questions")]
        public int NoOfQuestionsLevel2 { get; set; }
        [Required(ErrorMessage = "Please enter No of Level3 Qustions")]
        [Display(Name = "Enter Level3 Questions")]
        public int NoOfQuestionsLevel3 { get; set; }

        
      
    }
}