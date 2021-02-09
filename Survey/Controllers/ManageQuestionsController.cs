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
    public class ManageQuestionsController : Controller
    {
        readonly string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        // GET: ManageQuestions
        public ActionResult Index()
        {
            string SurveyID = Request.QueryString["SurveyID"];
            string Survey = Request.QueryString["Survey"];


            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(SurveyID))
            {
                DataSet ds = BindSurveysDDl();
                ViewBag.Surveys = ds.Tables[0];
                List<SelectListItem> GetSurveys = new List<SelectListItem>();
                foreach (DataRow dr in ViewBag.Surveys.Rows)
                {
                    GetSurveys.Add(new SelectListItem { Text = @dr["SurveyTitle"].ToString(), Value = @dr["SurveyID"].ToString() });
                }
                ViewBag.Surveylist = GetSurveys;
                using (SqlConnection con = new SqlConnection(connString))
                {

                    string sql = "Select Q.QuestionArabic,S.SurveyID,Q.QuestionId,Q.QuestionEnglish from dbo.Questions Q Inner Join dbo.Surveys S on Q.SurveyID = S.SurveyID where S.SurveyID=''  ";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    con.Open();

                    da.Fill(dt);

                }
            }
            else
            {
                
               
                DataSet ds = BindSurveysDDl();
                ViewBag.Surveys = ds.Tables[0];
                List<SelectListItem> GetSurveys = new List<SelectListItem>();
                foreach (DataRow dr in ViewBag.Surveys.Rows)
                {
                    GetSurveys.Add(new SelectListItem { Text = @dr["SurveyTitle"].ToString(), Value = @dr["SurveyID"].ToString() });
                }

                ViewBag.Surveylist = GetSurveys;
                using (SqlConnection con = new SqlConnection(connString))
                {

                    string sql = "Select Q.QuestionArabic,S.SurveyID,Q.QuestionId,Q.QuestionEnglish from dbo.Questions Q Inner Join dbo.Surveys S on Q.SurveyID = S.SurveyID where S.SurveyID='" + SurveyID+"'   ";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    con.Open();

                    da.Fill(dt);

                }
            }
           
            return View(dt);



        }
        [HttpPost]
        public ActionResult Index (int id)
        {
            //string SurveyId = collection[""];
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "Select S.SurveyTitle,S.SurveyID,Q.QuestionId,Q.QuestionEnglish from dbo.Questions Q Inner Join dbo.Surveys S on Q.SurveyID = S.SurveyID  ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
               
                    da.Fill(dt);
                
            }

            return View(dt);
            //return View(dt);
        }
        [HttpPost]
        public void GetSelectedSurvey(int ID)
        {
            RedirectToAction("Index", "ManageQuestions",FormMethod.Post);
        }

        public class Class1
        {

            public List<Surveys> DropClient()
            {
                string connString1 = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
                SqlConnection db = new SqlConnection(connString1);
                string query = "SELECT SurveyID,SurveyTitle FROM dbo.Surveys";
                SqlCommand cmd = new SqlCommand(query, db);
                db.Open();
                Surveys obj = new Surveys();
                List<Surveys> list = new List<Surveys>();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {

                    while (dataReader.Read())
                    {
                        obj.SurveyID = Convert.ToInt32(dataReader["SurveyID"]);
                        obj.SurveyTitle = dataReader["SurveyTitle"].ToString();
                        list.Add(obj);


                    }
                    return list;
                }

            }
        }

        // GET: ManageQuestions/Details/5

            [HttpPost]
        public ActionResult Details(int id)
        {

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "Select S.SurveyTitle,S.SurveyID,Q.QuestionId,Q.QuestionEnglish from dbo.Questions Q Inner Join dbo.Surveys S on Q.SurveyID = S.SurveyID where SurveyID='' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                
                    da.Fill(dt);
                
            }

            return View(dt);
        }
        public DataSet BindSurveysDDl()
        {
            DataSet ds = new DataSet();
           
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "SELECT SurveyID,SurveyTitle FROM dbo.Surveys";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(ds);
            }
            return ds;
        }
        public DataSet BindQuestionGroupDDl()
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "SELECT QuestionGroupID  ,QuestionGroupNameE ,QuestionGroupNameA  FROM dbo.QuestionGroup";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(ds);
            }
            return ds;
        }
        public DataSet BindQuestionTypeDDl()
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "SELECT  QuestionTypeId  ,QuestionTypeEnglish ,QuestionTypeArabic FROM dbo.QuestionType";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(ds);
            }
            return ds;
        }

            // GET: ManageQuestions/Create
            public ActionResult Create()
        {
            DataSet ds = BindSurveysDDl();
            ViewBag.Surveys = ds.Tables[0];
            List<SelectListItem> GetSurveys = new List<SelectListItem>();
            foreach (DataRow dr in ViewBag.Surveys.Rows)
            {
                GetSurveys.Add(new SelectListItem { Text = @dr["SurveyTitle"].ToString(), Value = @dr["SurveyID"].ToString() });
            }
            ViewBag.Surveylist =  GetSurveys;

            DataSet ds1 = BindQuestionGroupDDl();
            ViewBag.QuestionGrouplist = ds1.Tables[0];
            List<SelectListItem> GetQuestionGroup = new List<SelectListItem>();
            foreach (DataRow dr in ViewBag.QuestionGrouplist.Rows)
            {
                GetQuestionGroup.Add(new SelectListItem { Text = @dr["QuestionGroupNameE"].ToString(), Value = @dr["QuestionGroupID"].ToString() });
            }
            ViewBag.QuestionGrouplist = GetQuestionGroup;


            DataSet ds2 = BindQuestionTypeDDl();
            ViewBag.QuestionTypelist = ds2.Tables[0];
            List<SelectListItem> GetQuestionType = new List<SelectListItem>();
            foreach (DataRow dr in ViewBag.QuestionTypelist.Rows)
            {
                GetQuestionType.Add(new SelectListItem { Text = @dr["QuestionTypeEnglish"].ToString(), Value = @dr["QuestionTypeId"].ToString() });
            }
            ViewBag.QuestionTypelist = GetQuestionType;


            return View();
        }

        // POST: ManageQuestions/Create
        [HttpPost]
        public ActionResult Create(ManageQuestions ManageQuestions)
        {
            try
            {
                string SurveyDDLValue = Request.Form["Surveylist"].ToString();
                string QuestionGroupDDLValue = Request.Form["QuestionGrouplist"].ToString();
                string QuestionTypeDDLValue = Request.Form["QuestionTypelist"].ToString();
                // TODO: Add insert logic here

                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "INSERT INTO dbo.Questions " +
                          "(QuestionTypeId,QuestionID,QuestionEnglish,QuestionArabic,Answer1English,Answer2English,Answer3English,Answer4English,Answer5English,Answer1Arabic,Answer2Arabic,Answer3Arabic,Answer4Arabic,Answer5Arabic,SurveyID,QuestionGroupID) " +
                          "VALUES " +
                          "('"+ QuestionTypeDDLValue + "' ,(case when (Select max(QuestionID)+1 from [Questions] where SurveyID='"+ SurveyDDLValue + "') is null then 1 else (Select max(QuestionID)+1 from Questions where SurveyID='" + SurveyDDLValue + "') end )" +
                          ",'"+ManageQuestions.QuestionE+ "' ,N'" + ManageQuestions.QuestionA + "' ,'" + ManageQuestions.Answer1E + "'  ,'" + ManageQuestions.Answer2E + "'  ,'" + ManageQuestions.Answer3E + "'  " +
                          " ,'" + ManageQuestions.Answer4E + "','" + ManageQuestions.Answer5E + "' , N'" + ManageQuestions.Answer1A + "'  ,N'" + ManageQuestions.Answer2A + "'  ,N'" + ManageQuestions.Answer3A + "'  ,N'" + ManageQuestions.Answer4A + "'  ,N'" + ManageQuestions.Answer5A + "'  ,'" + SurveyDDLValue + "','"+ QuestionGroupDDLValue + "')";
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


                return RedirectToAction("Index", "ManageQuestions");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageQuestions/Edit/5
        public ActionResult Edit(int QuestionID, int SurveyID)
        {
            ManageQuestions manageQuestions = new ManageQuestions();
            DataSet ds = BindSurveysDDl();
            ViewBag.Surveys = ds.Tables[0];
            List<SelectListItem> GetSurveys = new List<SelectListItem>();
            foreach (DataRow dr in ViewBag.Surveys.Rows)
            {
                GetSurveys.Add(new SelectListItem { Text = @dr["SurveyTitle"].ToString(), Value = @dr["SurveyID"].ToString()});
            }
            ViewBag.Surveylist = GetSurveys;

            DataSet ds1 = BindQuestionGroupDDl();
            ViewBag.QuestionGrouplist = ds1.Tables[0];
            List<SelectListItem> GetQuestionGroup = new List<SelectListItem>();
            foreach (DataRow dr in ViewBag.QuestionGrouplist.Rows)
            {
                GetQuestionGroup.Add(new SelectListItem { Text = @dr["QuestionGroupNameE"].ToString(), Value = @dr["QuestionGroupID"].ToString() });
            }
            ViewBag.QuestionGrouplist = GetQuestionGroup;


            DataSet ds2 = BindQuestionTypeDDl();
            ViewBag.QuestionTypelist = ds2.Tables[0];
            List<SelectListItem> GetQuestionType = new List<SelectListItem>();
            foreach (DataRow dr in ViewBag.QuestionTypelist.Rows)
            {
                GetQuestionType.Add(new SelectListItem { Text = @dr["QuestionTypeEnglish"].ToString(), Value = @dr["QuestionTypeId"].ToString() });
            }
            ViewBag.QuestionTypelist = GetQuestionType;

            
            DataTable datatable = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql =
                    "SELECT *  FROM [dbo].[Questions] where QuestionID='"+ QuestionID + "' and SurveyID='"+ SurveyID + "' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(datatable);
            }

            if (datatable.Rows.Count == 1)
            {
                manageQuestions.SurveyID = Convert.ToInt32(datatable.Rows[0]["SurveyID"]);
                manageQuestions.QuestionGroupID = Convert.ToInt32(datatable.Rows[0]["QuestionGroupID"]);
                manageQuestions.QuestionTypeID = Convert.ToInt32(datatable.Rows[0]["QuestionTypeID"].ToString());
                manageQuestions.QuestionE = datatable.Rows[0]["QuestionEnglish"].ToString();
                manageQuestions.QuestionA = datatable.Rows[0]["QuestionArabic"].ToString();
                manageQuestions.Answer1E = datatable.Rows[0]["Answer1English"].ToString();
                manageQuestions.Answer2E = datatable.Rows[0]["Answer2English"].ToString();
                manageQuestions.Answer3E = datatable.Rows[0]["Answer3English"].ToString();
                manageQuestions.Answer4E = datatable.Rows[0]["Answer4English"].ToString();
                manageQuestions.Answer5E = datatable.Rows[0]["Answer5English"].ToString();

                manageQuestions.Answer1A = datatable.Rows[0]["Answer1Arabic"].ToString();
                manageQuestions.Answer2A = datatable.Rows[0]["Answer2Arabic"].ToString();
                manageQuestions.Answer3A = datatable.Rows[0]["Answer3Arabic"].ToString();
                manageQuestions.Answer4A = datatable.Rows[0]["Answer4Arabic"].ToString();
                manageQuestions.Answer5A = datatable.Rows[0]["Answer5Arabic"].ToString();



                return View(manageQuestions);

            }

            return RedirectToAction("Index", "ManageQuestions");
        }

        // POST: ManageQuestions/Edit/5
        [HttpPost]
        public ActionResult Edit(int QuestionID, int SurveyID, ManageQuestions manageQuestions)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(connString))
                {

                    string sql = "UPDATE dbo.Questions  " +
                        "SET QuestiontypeId = '"+ manageQuestions .QuestionTypeID+ "' " +
                        ",QuestionEnglish = '"+ manageQuestions.QuestionE + "' " +
                        ",QuestionArabic = N'"+ manageQuestions.QuestionA + "'" +
                        ",NoOfAnswer = '' " +
                        ",Answer1English = '"+ manageQuestions .Answer1E+ "'" +
                        ",Answer2English = '" + manageQuestions.Answer2E + "' " +
                        ",Answer3English = '" + manageQuestions.Answer3E + "' " +
                        ",Answer4English = '" + manageQuestions.Answer4E + "'" +
                        ",Answer5English = '" + manageQuestions.Answer5E + "'" +
                        ",Answer6English = ''" +
                        ",Answer7English = ''" +
                        ",Answer8English = '' " +
                        ",Answer1Arabic = N'" + manageQuestions.Answer1A + "'" +
                        ",Answer2Arabic = N'" + manageQuestions.Answer2A + "'" +
                        ",Answer3Arabic = N'" + manageQuestions.Answer3A + "'" +
                        ",Answer4Arabic = N'" + manageQuestions.Answer4A + "'" +
                        ",Answer5Arabic = N'" + manageQuestions.Answer5A + "'" +
                        ",Answer6Arabic = N'' " +
                        ",Answer7Arabic = N'' " +
                        ",Answer8Arabic = N''" +
                        ",TextQuestionArabic  = N'' " +
                        ",QuestionGroupID = '"+ manageQuestions.QuestionGroupID+ "'" +
                        " WHERE QuestionID='"+ QuestionID + "' and SurveyID='"+ SurveyID + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    
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

                return RedirectToAction("Index", "ManageQuestions");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageQuestions/Delete/5
        public ActionResult Delete(int QuestionID,int SurveyID)
        {
            try
            {
                // TODO: Add insert logic here

                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "DELETE FROM [dbo].[Questions] WHERE QuestionId='"+QuestionID+"' and SurveyID='"+ SurveyID + "'";
                var cmd = new SqlCommand(Sql, sqlcon);
                 try
                {
                    sqlcon.Open();
                    cmd.ExecuteNonQuery();

                }
                catch(Exception ex)
                {
                    //

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
            return RedirectToAction("Index", "ManageQuestions");
        }

        // POST: ManageQuestions/Delete/5
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