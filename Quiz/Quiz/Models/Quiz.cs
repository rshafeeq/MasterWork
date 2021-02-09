using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.Models
{
    public class RadioButtonLiatModel
    {
        public List<RadioButtonData> RadioButtonListData { get; set; }
        public string Question { get; set; }
    }
    public class RadioButtonData
    {
        public string Id { get; set; }
        public string Value { get; set; }

        public string correctAnswer { get; set; }
        
    }

    public class AnswerData
    {
       public int QuizId { get; set; }
      public int TeamId { get; set; }
      public int QuestionId { get; set; }
       public string Answer { get; set; }
      public int Points { get; set; }
    }
}