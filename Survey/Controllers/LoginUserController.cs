using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Survey.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Survey.Controllers
{
    public class LoginUserController : Controller
    {
        // GET: LoginUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginUser loginUser)
        {
            string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

            SqlConnection sqlcon = new SqlConnection(connString);
            string sqlQuery =
                "Select UserID, [UserName], ltrim(rtrim(FirstName)) +' '+ ltrim(rtrim(LastName)) as FullName, [Password],roles.Roles,roles.RoleID from Users users " +
                "Inner Join UsersRoles roles on users.RoleId=roles.RoleID " +
                "where UserName=@username and password=@password";
            try
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlcon);
                cmd.Parameters.AddWithValue("@username", loginUser.UserName);
                cmd.Parameters.AddWithValue("@password", loginUser.Password);

                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    Session["UserName"] = loginUser.UserName;
                    Session["Role"] = sdr["Roles"].ToString();
                    Session["Fullname"] = sdr["FullName"].ToString();
                    Session["RoleId"]= sdr["RoleID"].ToString();
                    Session["UserID"] = sdr["UserID"].ToString();

                    //return Redirect("Welcome");
                    return RedirectToAction("Welcome", "LoginUser");
                }
                else
                {
                    ViewData["Message"] = "Login Details are Incorrect !";
                }
            }
            finally
            {
                sqlcon.Close();
            }


            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }
    }
}