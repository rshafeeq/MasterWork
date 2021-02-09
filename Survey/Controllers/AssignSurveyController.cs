using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Survey.Models;
using System.Configuration;

namespace Survey.Controllers
{
    public class AssignSurveyController : Controller
    {
        readonly string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        // GET: AssignSurvey
        [HttpGet]
        public ActionResult Index()
        {
            if (Request.QueryString["bp"] == "right")
            {

            }
            


            DataSet ds = BindSurveysDDl();
            ViewBag.Surveys = ds.Tables[0];
            List<SelectListItem> GetSurveys = new List<SelectListItem>();
            foreach (DataRow dr in ViewBag.Surveys.Rows)
            {
                GetSurveys.Add(new SelectListItem { Text = @dr["SurveyTitle"].ToString(), Value = @dr["SurveyID"].ToString() });
            }
            ViewBag.Surveylist = GetSurveys;



            DataSet ds1 = BindRoles();
            ViewBag.GetUsersRoles = ds1.Tables[0];
            List<SelectListItem> GetUsersRoles = new List<SelectListItem>();
            foreach (DataRow dr in ViewBag.GetUsersRoles.Rows)
            {
                GetUsersRoles.Add(new SelectListItem { Text = @dr["Roles"].ToString(), Value = @dr["RoleID"].ToString() });
            }
            ViewBag.UsersRoleList = GetUsersRoles;

            


            AssignSurvey _assignSurveymodel = new AssignSurvey();
            _assignSurveymodel.UserUnSelectedList = new List<User>();
            _assignSurveymodel.UserSelectedList = new List<User>();
            _assignSurveymodel.UserUnSelectedList = GetUserData();
            return View(_assignSurveymodel);


        }

        public List<User> GetUserData()
        {
            DataSet ds = new DataSet();
            ds = BindUsers();
            
            List<User> _user = new List<User>();
            for(int i=0;i<ds.Tables[0].Rows.Count;i++ )
            {
                _user.Add(new User { UserId =Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]), fullName = ds.Tables[0].Rows[i]["Fullname"].ToString() });
            }
           
            return _user;

        }
        
        [HttpPost]
        public ActionResult Index(AssignSurvey _assignSurveymodel)
        {
           

                int SurveyDDLValue = Convert.ToInt32(Request.Form["Surveylist"].ToString());



                //Process your request for each country and again pass the list
                _assignSurveymodel.UserUnSelectedList = GetUserData().Where(m => !_assignSurveymodel.SelectedUserID.Contains(m.UserId)).ToList();
                _assignSurveymodel.UserSelectedList = GetUserData().Where(m => _assignSurveymodel.SelectedUserID.Contains(m.UserId)).ToList();
                string selectedUserId = "";
                foreach (int item in _assignSurveymodel.SelectedUserID)
                {

                    AssignUserSurvey(item, SurveyDDLValue);
                }
                ViewBag.SelectedUserId = selectedUserId;
                //return View(_assignSurveymodel);
               
            
            return RedirectToAction("Index", "ManageSurveys");

        }
        public DataSet BindSurveysDDl()
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "SELECT SurveyID,SurveyTitle FROM dbo.Surveys ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(ds);
            }
            return ds;
        }


        public DataSet BindUsers()
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
               
                    string sql = "SELECT FirstName +' '+ [LastName] +' ('+ UR.Roles +')' as Fullname,UserID FROM Users U Inner Join UsersRoles UR on U.RoleID= UR.RoleID order by U.RoleID ";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    da.Fill(ds);
            
            }
            return ds;

        }
        public DataSet BindRoles()
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                string sql = "SELECT  RoleID  ,Roles   FROM Survey.dbo.UsersRoles";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(ds);


            }
            return ds;

        }
        public ActionResult Right()
        {
            return RedirectToAction("Index", "AssignSurvey");
        }
        public void AssignUserSurvey(int UserID, int SurveyID)
        {

            // TODO: Add insert logic here
            DeleteUserServeyUser(UserID, SurveyID);
            SqlConnection sqlcon = new SqlConnection(connString);
            var Sql = "INSERT INTO dbo.UserSurvey ([UserID],[SurveyID])  " +
                      "VALUES " +
                      "('" + UserID + "','" + SurveyID + "')";
            var cmd = new SqlCommand(Sql, sqlcon);

            try
            {
                sqlcon.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlcon.Close();
                cmd.Dispose();
            }


            RedirectToAction("Index", "AssignSurveys");
        }
        public void DeleteUserServeyUser(int UserID, int SurveyID)
        {

            // TODO: Add Delete logic here

            SqlConnection sqlcon = new SqlConnection(connString);
            var Sql = "Delete from UserSurvey where UserID='" + UserID + "' and SurveyID='"+ SurveyID + "'";
            var cmd = new SqlCommand(Sql, sqlcon);

            try
            {
                sqlcon.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                sqlcon.Close();
                cmd.Dispose();
            }


            RedirectToAction("Index", "AssignSurveys");
        }


    }
}
