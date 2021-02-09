using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Survey.Models
{
    public class AssignSurvey
    {
        public List<User> UserUnSelectedList { get; set; }
        public List<User> UserSelectedList { get; set; }
        public int[] SelectedUserID { get; set; }

    }
    public class User
    {
        public int UserId { get; set; }
        public string fullName { get; set; }
    }
}