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
    public class ManageQuizController : Controller
    {
        readonly string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        // GET: ManageQuiz
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql =
                    "SELECT  QuizId ,QuizName,NoOfLevels ,NoOfQuestionsLevel1 ,NoOfQuestionsLevel2  ,NoOfQuestionsLevel3 FROM Quiz";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            return View(dt);

        }

        // GET: ManageQuiz/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManageQuiz/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageQuiz/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, ManageQuiz manageQuiz)
        {
            try
            {

                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "INSERT INTO dbo.Quiz (QuizName ,NoOfLevels ,NoOfQuestionsLevel1 ,NoOfQuestionsLevel2 ,NoOfQuestionsLevel3) " +
                    "VALUES (@QuizName ,@NoOfLevels ,@NoOfQuestionsLevel1 ,@NoOfQuestionsLevel2 ,@NoOfQuestionsLevel3)";
                var cmd = new SqlCommand(Sql, sqlcon);
                cmd.Parameters.AddWithValue("@QuizName", manageQuiz.QuizName);
                cmd.Parameters.AddWithValue("@NoOfLevels", manageQuiz.NoOfLevels);
                cmd.Parameters.AddWithValue("@NoOfQuestionsLevel1", manageQuiz.NoOfQuestionsLevel1);
                cmd.Parameters.AddWithValue("@NoOfQuestionsLevel2", manageQuiz.NoOfQuestionsLevel2);
                cmd.Parameters.AddWithValue("@NoOfQuestionsLevel3", manageQuiz.NoOfQuestionsLevel3);

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


                return RedirectToAction("Index", "ManageQuiz");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageQuiz/Edit/5
        public ActionResult Edit(int id)
        {
            ManageQuiz manageQuiz = new ManageQuiz();
            DataTable datatable = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql =
                    "SELECT * from Quiz where QuizID='" + id + "' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(datatable);
            }

            if (datatable.Rows.Count == 1)
            {
                manageQuiz.QuizName = datatable.Rows[0]["QuizName"].ToString();
                manageQuiz.NoOfLevels = Convert.ToInt32(datatable.Rows[0]["NoOfLevels"]);
                manageQuiz.NoOfQuestionsLevel1 = Convert.ToInt32(datatable.Rows[0]["NoOfQuestionsLevel1"]);
                manageQuiz.NoOfQuestionsLevel2 = Convert.ToInt32(datatable.Rows[0]["NoOfQuestionsLevel2"]);
                manageQuiz.NoOfQuestionsLevel3 = Convert.ToInt32(datatable.Rows[0]["NoOfQuestionsLevel3"]);

                return View(manageQuiz);

            }

            return RedirectToAction("Index", "ManageQuiz");
        }
    

        // POST: ManageQuiz/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection,ManageQuiz manageQuiz)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(connString))
                {

                    string sql = "UPDATE dbo.Quiz    " +
                        "SET QuizName = @QuizName" +
                        ",NoOfLevels = @NoOfLevels" +
                        ",NoOfQuestionsLevel1 = @NoOfQuestionsLevel1" +
                        ",NoOfQuestionsLevel2 = @NoOfQuestionsLevel2," +
                        "NoOfQuestionsLevel3 = @NoOfQuestionsLevel3 " +
                        "WHERE QuizId='"+ id +"'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@QuizName", manageQuiz.QuizName);
                    cmd.Parameters.AddWithValue("@NoOfLevels", manageQuiz.NoOfLevels);
                    cmd.Parameters.AddWithValue("@NoOfQuestionsLevel1", manageQuiz.NoOfQuestionsLevel1);
                    cmd.Parameters.AddWithValue("@NoOfQuestionsLevel2", manageQuiz.NoOfQuestionsLevel2);
                    cmd.Parameters.AddWithValue("@NoOfQuestionsLevel3", manageQuiz.NoOfQuestionsLevel3);


                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    finally
                    {
                        con.Close();
                        cmd.Dispose();
                    }
                }

                return RedirectToAction("Index", "ManageQuiz");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageQuiz/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add insert logic here

                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "Delete from [dbo].[Quiz] where QuizId=@QuizId";
                var cmd = new SqlCommand(Sql, sqlcon);
                cmd.Parameters.AddWithValue("@QuizId", id);
                try
                {
                    sqlcon.Open();
                    cmd.ExecuteNonQuery();

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
            return RedirectToAction("Index", "ManageQuiz");
        }

        // POST: ManageQuiz/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
