using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Survey.Models;
using System.Collections;

namespace Survey.Controllers
{
    public class SurveyQuestionsArabicController : Controller
    {
        readonly string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        // GET: SurveyQuestionsArabic
        public ActionResult Index()
        {
            StringBuilder sb = new StringBuilder();
            string surveyid = Request.QueryString["surveyid"];
            Session["surveyid"] = surveyid;
            DataSet ds = BindQuestions(Convert.ToInt32(surveyid));
            List<string> ConTrolId = new List<string>();
            sb.Append("<Div class=\"\"> <table class=\"table table - bordered\">");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string str1 = ds.Tables[0].Rows[i]["Answer1Arabic"].ToString();
                string ID1 = string.Concat(str1.Replace(" ", ""), ds.Tables[0].Rows[i]["QuestionNumber"]);
                string str2 = ds.Tables[0].Rows[i]["Answer2Arabic"].ToString();
                string ID2 = string.Concat(str2.Replace(" ", ""), ds.Tables[0].Rows[i]["QuestionNumber"]);
                string str3 = ds.Tables[0].Rows[i]["Answer3Arabic"].ToString();
                string ID3 = string.Concat(str3.Replace(" ", ""), ds.Tables[0].Rows[i]["QuestionNumber"]);
                string str4 = ds.Tables[0].Rows[i]["Answer4Arabic"].ToString();
                string ID4 = string.Concat(str4.Replace(" ", ""), ds.Tables[0].Rows[i]["QuestionNumber"]);
                string str5 = ds.Tables[0].Rows[i]["Answer5Arabic"].ToString();
                string ID5 = string.Concat(str5.Replace(" ", ""), ds.Tables[0].Rows[i]["QuestionNumber"]);

                string textArea = "TxtArea" + ds.Tables[0].Rows[i]["QuestionNumber"];

                string QuestionNumber = ds.Tables[0].Rows[i]["QuestionID"].ToString();

                switch (ds.Tables[0].Rows[i]["QuestiontypeId"])
                {

                    case 1:
                        ConTrolId.Add(QuestionNumber);
                        //ConTrolId.Add(QuestionNumber);
                        //ConTrolId.Add(QuestionNumber);
                        //ConTrolId.Add(QuestionNumber);
                        //ConTrolId.Add(QuestionNumber);
                        sb.Append(
                            "<tr>" +
                            "<td> " + ds.Tables[0].Rows[i]["QuestionNumber"] + ". " + ds.Tables[0].Rows[i]["QuestionArabic"] + "</td></tr>" + " " +
                            " <tr><td>&nbsp; <input  type=\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer1Arabic"] + "\" id=" + ID1 + "> " + ds.Tables[0].Rows[i]["Answer1Arabic"] + " " +
                            "   &nbsp;  <input type=\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer2Arabic"] + "\" id=" + ID2 + "> " + ds.Tables[0].Rows[i]["Answer2Arabic"] + " " +
                            " &nbsp;<input  type=\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer3Arabic"] + "\" id=" + ID3 + "> " + ds.Tables[0].Rows[i]["Answer3Arabic"] + " " +
                            "&nbsp;<input  type=\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer4Arabic"] + "\" id=" + ID4 + "> " + ds.Tables[0].Rows[i]["Answer4Arabic"] + " " +
                            "&nbsp; <input  type=\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer5Arabic"] + "\" id=" + ID5 + "> " + ds.Tables[0].Rows[i]["Answer5Arabic"] + "</td>" +
                            "</tr>");
                        break;
                    case 2:
                        ConTrolId.Add(QuestionNumber);
                        //ConTrolId.Add(QuestionNumber);
                        //ConTrolId.Add(QuestionNumber);
                        //ConTrolId.Add(QuestionNumber);
                        //ConTrolId.Add(QuestionNumber);
                        sb.Append("<tr>" +
                            "<td> " + ds.Tables[0].Rows[i]["QuestionNumber"] + ". " + ds.Tables[0].Rows[i]["QuestionArabic"] + "</td></tr>" + " " +
                            "<tr><td>&nbsp; <input  type =\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer1Arabic"] + "\" id=" + ID1 + "> " + ds.Tables[0].Rows[i]["Answer1Arabic"] + " " +
                            "   &nbsp;  <input  type=\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer2Arabic"] + "\" id=" + ID2 + "> " + ds.Tables[0].Rows[i]["Answer2Arabic"] + " " +
                            " &nbsp;<input  type=\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer3Arabic"] + "\" id=" + ID3 + "> " + ds.Tables[0].Rows[i]["Answer3Arabic"] + " " +
                            "&nbsp;<input  type=\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer4Arabic"] + "\" id=" + ID4 + "> " + ds.Tables[0].Rows[i]["Answer4Arabic"] + " " +
                            "&nbsp;<input  type=\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer5Arabic"] + "\" id=" + ID5 + "> " + ds.Tables[0].Rows[i]["Answer5Arabic"] + "</td>" +
                            "</tr>");
                        break;
                    case 5:
                        ConTrolId.Add(QuestionNumber);
                        sb.Append(
                            "<tr>" +
                            "<td> " + ds.Tables[0].Rows[i]["QuestionNumber"] + ". " + ds.Tables[0].Rows[i]["QuestionArabic"] + "</td>" + "</tr> " +
                            "<tr><td>&nbsp; <textarea class=\"form-control\" name =" + QuestionNumber + " cols=\"100\" rows=\"2\" id=" + textArea + "></textarea>" +
                             "</td></tr>" +
                            "</tr>");
                        break;
                    case 3:
                        ConTrolId.Add(QuestionNumber);
                        sb.Append("<tr>" +
                                  "<td> " + ds.Tables[0].Rows[i]["QuestionNumber"] + ". " + ds.Tables[0].Rows[i]["QuestionArabic"] + "</td></tr>" + " " +
                                  "<tr><td>&nbsp; <input  type =\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer1Arabic"] + "\" id=" + ID1 + "> " + ds.Tables[0].Rows[i]["Answer1Arabic"] + " " +
                                  "   &nbsp;  <input  type=\"radio\" name=" + QuestionNumber + " value=\"" + ds.Tables[0].Rows[i]["Answer2Arabic"] + "\" id=" + ID2 + "> " + ds.Tables[0].Rows[i]["Answer2Arabic"] + " " +
                                  "</tr>");
                        break;


                }

            }

            sb.Append("</Table></Div>");

            ViewData["myInnerHtml"] = sb.ToString();
            Session["ControlsIds"] = ConTrolId;
            return View();
        }
        public DataSet BindQuestions(int SurveyID)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "SELECT  ROW_NUMBER() OVER (ORDER BY Questions.QuestionGroupID) QuestionNumber,  " +
                    "QuestionID ,QuestionArabic, QuestiontypeId,Answer1Arabic,Answer2Arabic,Answer3Arabic," +
                    "Answer4Arabic,Answer5Arabic,Answer1Arabic,Answer2Arabic,Answer3Arabic,Answer4Arabic,Answer5Arabic," +
                    "TextQuestionArabic,TextQuestionArabic, [QuestionGroup].QuestionGroupNameE FROM Questions  " +
                    "Inner Join  [QuestionGroup] On Questions.[QuestionGroupID]=[QuestionGroup].[QuestionGroupID] " +
                    "where Questions.SurveyID ='" + SurveyID + "'  order by Questions.QuestionGroupID,QuestionID ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(ds);
            }
            return ds;

        }
        public DataTable BindQuestionsType()
        {
            DataTable ds = new DataTable();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "SELECT  QuestionTypeId  ,QuestionTypeArabic ,QuestionTypeArabic FROM QuestionType ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(ds);
            }
            return ds;

        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            List<string> controlsId = Session["ControlsIds"] != null ? (List<string>)Session["ControlsIds"] : null;

            if (controlsId != null)
            {
                foreach (var items in controlsId)
                {
                    var value = collection.Get(items.ToString());
                    if (value != null)
                    {
                        InsertSurveyAnswers(Convert.ToInt32(items), Convert.ToInt32(Session["surveyid"]), value, "", "", "", Convert.ToInt32(Session["UserID"]));
                    }
                }
                UpdateSurveyStatus(Convert.ToInt32(Session["UserID"]), Convert.ToInt32(Session["surveyid"]), 1);
            }

            //var value  = collection.Get("Agree1");
            // string QueryString = "?Submitted=Thank you for Participation";
            // return RedirectToAction("Index" +"/" + QueryString ,  "SurveyQuestions");
            //return RedirectToAction("Index", "SurveyQuestions");
            return Redirect(Url.Action("Index", "SurveyQuestions") + "?Message=Thank you for participating in Survey");
        }
        public void InsertSurveyAnswers(int QuestionID, int Surveyid, string Answer1Arabic, string TextQuestionArabic, string Answer1English, string TextQuestionEnglish, int userId)
        {
            try
            {
                // TODO: Add insert logic here

                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "INSERT INTO dbo.Answers(QuestionId,Answer1English,TextQuestionEnglish,Answer1Arabic,TextQuestionArabic,SurveyID, UserId) VALUES('" + QuestionID + "' ,N'" + Answer1Arabic + "' ,'" + TextQuestionArabic + "' ,N'" + Answer1English + "','" + TextQuestionArabic + "' ,'" + Surveyid + "','" + userId + "')";
                var cmd = new SqlCommand(Sql, sqlcon);

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
        }
        public void UpdateSurveyStatus(int UserId, int SurveyId, int Status)
        {
            try
            {
                // TODO: Add insert logic here

                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "UPDATE dbo.UserSurvey    SET Status = '" + Status + "'  WHERE UserID = '" + UserId + "' and SurveyID = '" + SurveyId + "'";
                var cmd = new SqlCommand(Sql, sqlcon);

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
        }
    }
}