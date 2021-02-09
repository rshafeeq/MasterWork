using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quiz.Models;

namespace Quiz.Controllers
{
    public class QuizController : Controller
    {
        readonly string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        // GET: Quiz
        public ActionResult Index(int id)
        {
            
            Session["QuizID"] = id;

           
            //if (Request.QueryString["teamid"] != null)
            //{

            //    if (Request.QueryString["answer"] != null)
            //    {
            //        dt = GetQuestionLevel1();
            //        Session["QuestionId"] = dt.Rows[0]["QuestionId"].ToString();
            //        Session["Points"] = dt.Rows[0]["Points"].ToString();
            //    }
            //}

            if (Request.QueryString["teamid"] != null)
            {
                string teamid = Request.QueryString["teamid"].ToString();
                if (Request.QueryString["answer"] != null)
                {
                    string answer = Request.QueryString["answer"].ToString();
                    int points = getpoints(Convert.ToInt32(Session["QuestionId"]), answer);
                    SaveResults(id, teamid, Convert.ToInt32(Session["QuestionId"]), answer, points);
                }

            }
            DataSet dtPoints = new DataSet();
            dtPoints = getTeamPoints(Convert.ToInt32(Session["QuizID"]));
            ViewData["team1"] = 0;
            ViewData["team2"] = 0;
            ViewData["team3"] = 0;
            ViewData["team4"] = 0;
            if (dtPoints.Tables[0].Rows.Count>0)
            {
                ViewData["team1"] = dtPoints.Tables[0].Rows[0]["Points"];
            }
            if (dtPoints.Tables[1].Rows.Count > 0)
            {
                ViewData["team2"] = dtPoints.Tables[1].Rows[0]["Points"];
            }
            if (dtPoints.Tables[2].Rows.Count > 0)
            {
                ViewData["team3"] = dtPoints.Tables[2].Rows[0]["Points"];
            }
            if (dtPoints.Tables[3].Rows.Count > 0)
            {
                ViewData["team4"] = dtPoints.Tables[3].Rows[0]["Points"];
            }
            DataTable dt = new DataTable();
            dt = GetQuestionLevel1();
            Session["QuestionId"] = dt.Rows[0]["QuestionId"].ToString();
            Session["Points"] = dt.Rows[0]["Points"].ToString();
             
            RadioButtonLiatModel _radiobuttonliatmodel = new RadioButtonLiatModel();
            _radiobuttonliatmodel.RadioButtonListData = new List<RadioButtonData>();
            _radiobuttonliatmodel.Question = dt.Rows[0]["Question"].ToString();
            _radiobuttonliatmodel.RadioButtonListData.Add(new RadioButtonData { Id = "inlineRadio1", Value = dt.Rows[0]["Option1"].ToString(), correctAnswer= dt.Rows[0]["RightAnswer"].ToString() });
            _radiobuttonliatmodel.RadioButtonListData.Add(new RadioButtonData { Id = "inlineRadio1", Value = dt.Rows[0]["Option2"].ToString(), correctAnswer = dt.Rows[0]["RightAnswer"].ToString() });
            _radiobuttonliatmodel.RadioButtonListData.Add(new RadioButtonData { Id = "inlineRadio1", Value = dt.Rows[0]["Option3"].ToString(), correctAnswer = dt.Rows[0]["RightAnswer"].ToString() });
            _radiobuttonliatmodel.RadioButtonListData.Add(new RadioButtonData { Id = "inlineRadio1", Value = dt.Rows[0]["Option4"].ToString(), correctAnswer = dt.Rows[0]["RightAnswer"].ToString() });

            return View(_radiobuttonliatmodel);
        }
        [HttpPost]
        public ActionResult Index(FormCollection formCollection,int id)
        {

            
            return View("Index");
        }
        public DataTable GetQuestionLevel1()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {

                string sql =
                    "SELECT  QuestionId,Question,ltrim(rtrim(Option1)) Option1 ,ltrim(rtrim(Option2))Option2,ltrim(rtrim(Option3))Option3,ltrim(rtrim(Option4))Option4," +
                    "Points,RightAnswer ,QuestionLevel ,GoldenQuestion,QuestionArabic,Option1Arabic ,Option2Arabic ,Option3Arabic,Option4Arabic ,RightAnswerArabic FROM Questions " +
                    " where  QuestionLevel=1 and GoldenQuestion='No' and QuestionId not in (SELECT [QuestionId] FROM [TempAnswer]) ORDER BY NEWID()";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);

                con.Open();
                try
                {
                    da.Fill(dt);

                }
                finally
                {
                    con.Close();
                    da.Dispose();
                }

            }
            return dt;
        }
        public int getpoints(int QuestionId, string RightAnswer)
        {
            int points = 0;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {

                string sql =
                    "SELECT    Points  FROM Quiz.dbo.Questions where QuestionId ='"+ QuestionId+"' and RightAnswer='"+RightAnswer+"'";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);

                con.Open();
                try
                {
                    da.Fill(dt);
                    if(dt.Rows.Count>0)
                    {
                        points = Convert.ToInt32(dt.Rows[0]["Points"]);
                    }

                }
                finally
                {
                    con.Close();
                    da.Dispose();
                }

            }
           
            return points;
        }
        public DataSet getTeamPoints(int QuizId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connString))
            {
                string sql =
                    "SELECT Sum(Points) Points,TeamId FROM Quiz.dbo.Answers where  QuizId = '"+QuizId+ "' and TeamId = 'Team1'   group by TeamId " +
                    "SELECT  Sum(Points) Points,TeamId FROM Quiz.dbo.Answers where  QuizId = '" + QuizId + "' and TeamId = 'Team2'   group by TeamId " +
                    "SELECT  Sum(Points) Points,TeamId FROM Quiz.dbo.Answers where  QuizId = '" + QuizId + "' and TeamId = 'Team3'   group by TeamId " +
                    "SELECT Sum(Points) Points,TeamId FROM Quiz.dbo.Answers where  QuizId = '" + QuizId + "' and TeamId = 'Team4'   group by TeamId ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);

                con.Open();
                try
                {
                    da.Fill(ds);
                    

                }
                finally
                {
                    con.Close();
                    da.Dispose();
                }

            }

            return ds;
        }
        public DataTable GetQuestionLevel2()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {

                string sql =
                    "SELECT top 1 * FROM [Quiz].[dbo].[Questions]" +
                    " where  QuestionLevel=2 and GoldenQuestion='No'  " +
                    "and QuestionId " +
                    "not in (SELECT [QuestionId] FROM [TempAnswer]) " +
                    "ORDER BY NEWID()";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                con.Open();
                try
                {
                    da.Fill(dt);

                }
                finally
                {
                    con.Close();
                    da.Dispose();
                }

            }
            return dt;
        }
        public DataTable GetGoldenQuestion()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {

                string sql =
                    "SELECT top 1 * FROM [Quiz].[dbo].[Questions] " +
                    "where   GoldenQuestion='Yes'  and QuestionId " +
                    "not in (SELECT [QuestionId] FROM [TempAnswer]) " +
                    "ORDER BY NEWID()";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                con.Open();
                try
                {
                    da.Fill(dt);

                }
                finally
                {
                    con.Close();
                    da.Dispose();
                }
            }
            return dt;
        }
        public DataTable GetLevels()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {

                string sql =
                    "SELECT [NoOfLevels] ,[NoOfQuestionsLevel1],[NoOfQuestionsLevel2],[NoOfQuestionsLevel3] FROM [Quiz].[dbo].[Quiz] where Quizid='1'";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                con.Open();
                try
                {
                    da.Fill(dt);

                }
                finally
                {
                    con.Close();
                    da.Dispose();
                }
            }
            return dt;
        }
        [HttpPost]
        public void Test()
        {

        }
        [HttpPost]
        public void SaveResults(int QuizId, string TeamId, int QuestionId,string Answer,int Points)
        {
            try
            {
                // TODO: Add insert logic here

                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "INSERT INTO dbo.Answers (QuizId,TeamId,QuestionId,Answer,Points) " +
                    "VALUES  " +
                    "('"+ QuizId + "' ,'"+ TeamId +"' ,'"+ QuestionId+"' ,'"+Answer+"' ,'"+Points+"')";
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


               
            }
            catch
            {
               
            }
            //RedirectToAction("Index", "ManageQuiz");
        }
    }
}