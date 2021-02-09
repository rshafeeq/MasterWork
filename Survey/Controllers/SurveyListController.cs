using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Survey.Controllers
{
    public class SurveyListController : Controller
    {
        readonly string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        // GET: SurveysList
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql =
                    //"Select * from Surveys S inner join UserSurvey US on s.SurveyID=US.SurveyID where UserID='" + Session["UserID"] + "' and status=0 ";
                "Select * from Surveys S inner join UserSurvey US on s.SurveyID=US.SurveyID where UserID='" +
                    Session["UserID"] +
                    "' and status=0 and S.SurveyID in(Select SurveyID from Questions where SurveyID in (SELECT SurveyID   FROM [Survey].[dbo].[UserSurvey] where Userid='" +
                    Session["UserID"] + "'))";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            return View(dt);

        }

        public DataTable CheckQuestionsAvailable(int userid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql =
                    "Select * from Questions where SurveyID in (SELECT SurveyID   FROM [Survey].[dbo].[UserSurvey] where Userid='"+ userid + "')";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }

            return dt;
        }
    }
}