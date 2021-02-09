using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Survey.Models;
namespace Survey.Controllers
{
    public class ManageSurveysController : Controller
    {
        readonly string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        // GET: ManageSurveys
        public ActionResult Index()
        {
            
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql =
                    "SELECT SurveyID, SurveyTitle,SurveyTitleArabic,SurveyDescriptionArabic,SurveyDescriptionEnglish,SurveyStartDate,SurveyEndDate, case Active when '1' then 'Active' else 'Not Active' end Active  FROM Surveys ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            return View(dt);
        }

        // GET: ManageSurveys/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManageSurveys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageSurveys/Create
        [HttpPost]
        public ActionResult Create(ManageSurveys manageSurveys)
        {
            try
            {
                // TODO: Add insert logic here

                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "INSERT INTO dbo.Surveys (SurveyTitle, SurveyTitleArabic,SurveyDescriptionArabic," +
                          " SurveyDescriptionEnglish, SurveyStartDate, SurveyEndDate, Active)  " +
                          "VALUES " +
                          "(@SurveyTitle, @SurveyTitleArabic, @SurveyDescriptionArabic, @SurveyDescriptionEnglish, " +
                          "@SurveyStartDate, @SurveyEndDate, @Active)";
                var cmd = new SqlCommand(Sql, sqlcon);
                cmd.Parameters.AddWithValue("@SurveyTitle", manageSurveys.SurveyTitle);
                cmd.Parameters.AddWithValue("@SurveyTitleArabic", manageSurveys.SurveyTitleArabic);
                cmd.Parameters.AddWithValue("@SurveyDescriptionEnglish", manageSurveys.SurveyDescriptionEnglish);
                cmd.Parameters.AddWithValue("@SurveyDescriptionArabic", manageSurveys.SurveyDescriptionArabic);
                cmd.Parameters.AddWithValue("@SurveyStartDate", manageSurveys.SurveyStartDate);
                cmd.Parameters.AddWithValue("@SurveyEndDate", manageSurveys.SurveyEndDate);
                cmd.Parameters.AddWithValue("@Active", manageSurveys.Active);

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


                return RedirectToAction("Index", "ManageSurveys");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageSurveys/Edit/5
        public ActionResult Edit(int id)
        {
            ManageSurveys manageSurveys = new ManageSurveys();
            DataTable datatable = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql =
                    "SELECT * from Surveys where SurveyID='" + id + "' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(datatable);
            }

            if (datatable.Rows.Count == 1)
            {
                manageSurveys.SurveyTitle = datatable.Rows[0]["SurveyTitle"].ToString();
                manageSurveys.SurveyTitleArabic = datatable.Rows[0]["SurveyTitleArabic"].ToString();
                manageSurveys.SurveyDescriptionEnglish = datatable.Rows[0]["SurveyDescriptionEnglish"].ToString();
                manageSurveys.SurveyDescriptionArabic = datatable.Rows[0]["SurveyDescriptionArabic"].ToString();
                manageSurveys.SurveyStartDate = Convert.ToDateTime(datatable.Rows[0]["SurveyStartDate"]);
                manageSurveys.SurveyEndDate = Convert.ToDateTime(datatable.Rows[0]["SurveyEndDate"]);
                manageSurveys.Active = Convert.ToInt32(datatable.Rows[0]["Active"].ToString());


                return View(manageSurveys);

            }

            return RedirectToAction("Index", "ManageSurveys");
        }

        // POST: ManageSurveys/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ManageSurveys manageSurveys)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(connString))
                {

                    string sql = "UPDATE dbo.Surveys " +
                                 "SET SurveyTitle = @SurveyTitle ," +
                                 "SurveyTitleArabic = @SurveyTitleArabic," +
                                 "SurveyDescriptionArabic = @SurveyDescriptionArabic ," +
                                 "SurveyDescriptionEnglish = @SurveyDescriptionEnglish ," +
                                 "SurveyStartDate = @SurveyStartDate ," +
                                 "SurveyEndDate = @SurveyEndDate   ," +
                                 "Active = @Active " +
                                 "WHERE SurveyID='" + id + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@SurveyTitle", manageSurveys.SurveyTitle);
                    cmd.Parameters.AddWithValue("@SurveyTitleArabic", manageSurveys.SurveyTitleArabic);
                    cmd.Parameters.AddWithValue("@SurveyDescriptionArabic", manageSurveys.SurveyDescriptionArabic);
                    cmd.Parameters.AddWithValue("@SurveyDescriptionEnglish", manageSurveys.SurveyDescriptionEnglish);
                    cmd.Parameters.AddWithValue("@SurveyStartDate", manageSurveys.SurveyStartDate);
                    cmd.Parameters.AddWithValue("@SurveyEndDate", manageSurveys.SurveyEndDate);
                    cmd.Parameters.AddWithValue("@Active", manageSurveys.Active);



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

                return RedirectToAction("Index", "ManageSurveys");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageSurveys/Delete/5
        public ActionResult Delete(int id)
        {
            DeleteUserSurveys(id);
            DeleteAnswerSurveys(id);

            // TODO: Add insert logic here

                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "Delete from [dbo].[Surveys] where SurveyID=@SurveyID";
                var cmd = new SqlCommand(Sql, sqlcon);
                cmd.Parameters.AddWithValue("@SurveyID", id);
            try
            {
                sqlcon.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                return this.RedirectToAction("Index", "ManageSurveys", new { value1 = "Cannot Delete as Questions to this Survey are still available. Kindly delete it first" });

            }
            finally
            {
                sqlcon.Close();
                cmd.Dispose();
            }

            return RedirectToAction("Index", "ManageSurveys");

        }


        public void DeleteUserSurveys(int id)
        {

            SqlConnection sqlcon = new SqlConnection(connString);
            var Sql = "Delete from [dbo].[UserSurvey] where SurveyID=@SurveyID";
            var cmd = new SqlCommand(Sql, sqlcon);
            cmd.Parameters.AddWithValue("@SurveyID", id);
            try
            {
                sqlcon.Open();
                cmd.ExecuteNonQuery();

            }
            catch(Exception ex)
            {

            }
            finally
            {
                sqlcon.Close();
                cmd.Dispose();
            }

        }
        public void DeleteAnswerSurveys(int id)
        {

            SqlConnection sqlcon = new SqlConnection(connString);
            var Sql = "Delete from [dbo].[Answers] where SurveyID=@SurveyID";
            var cmd = new SqlCommand(Sql, sqlcon);
            cmd.Parameters.AddWithValue("@SurveyID", id);
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



        // POST: ManageSurveys/Delete/5
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
