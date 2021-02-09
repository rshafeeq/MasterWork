using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Quiz.Models;

namespace Quiz.Controllers
{
    public class UploadQuestionsController : Controller
    {
        readonly string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        // GET: UploadQuestions
        public ActionResult Index()
        {
            Utilities utilities = new Utilities();
            int rint = utilities.RandomNumber(1, 100);
            return View();
        }
        public void UploadfromExcel(int QuestionId,string Question,string Option1,string Option2,string Option3 , string Option4,int Points,string RightAnswer,
            string QuestionLevel,string GoldenQuestion,string QuestionArabic, string Option1Arabic, string Option2Arabic,string Option3Arabic,string Option4Arabic,string RightAnswerArabic)
        {
            try
            {
                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "INSERT INTO dbo.Questions (QuestionId,Question,Option1,Option2,Option3,Option4,Points," +
                    "RightAnswer,QuestionLevel,GoldenQuestion,QuestionArabic,Option1Arabic,Option2Arabic,Option3Arabic,Option4Arabic," +
                    "RightAnswerArabic)   " +
                    "VALUES ( '"+QuestionId+ "', '" + Question + "', '" + Option1 + "', '" + Option2 + "', '" + Option3 + "',  '" + Option4 + "', '" + Points + "', '" + RightAnswer + "'" +
                    ", '" + QuestionLevel + "', '" + GoldenQuestion + "', N'" +QuestionArabic + "', N'"+ Option1Arabic+"',  N'" + Option2Arabic +"', N'"+ Option3Arabic+"', " +
                    "N'"+Option4Arabic+"',N'"+ RightAnswerArabic+"')";
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
        }
        public void UpdatefromExcel(int QuestionId, string Question, string Option1, string Option2, string Option3, string Option4, int Points, string RightAnswer,
            string QuestionLevel, string GoldenQuestion, string QuestionArabic, string Option1Arabic, string Option2Arabic, string Option3Arabic, string Option4Arabic, string RightAnswerArabic)
        {
            try
            {


                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "Update Questions " +
                          "set  QuestionId='" + QuestionId + "',Question='" + Question + "',Option1='" + Option1 + "'," +
                          "Option2=N'" + Option2 + "',Option3='" + Option3 + "',Option4='" + Option4 + "'," +
                          "Points='" + Points + "',RightAnswer='" + RightAnswer + "',QuestionLevel='" + QuestionLevel + "'," +
                          "GoldenQuestion= N'" + GoldenQuestion + "',QuestionArabic=N'" + QuestionArabic + "N',Option1Arabic=N'" + Option1Arabic + "',Option2Arabic=N'" + Option2Arabic + "'," +
                          "Option3Arabic=N'" + Option3Arabic + "',Option4Arabic=N'" + Option4Arabic + "',RightAnswerArabic=N'" + RightAnswerArabic + "' where QuestionId='" + QuestionId + "' ";
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
        }


        

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            StringBuilder sbFail = new StringBuilder();
            StringBuilder sbSuccess = new StringBuilder();
            StringBuilder Updated = new StringBuilder();
            bool hasHeader = true;
           
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                string filePath;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var pck = new OfficeOpenXml.ExcelPackage())
                {

                    using (var stream = System.IO.File.OpenRead(filePath))
                    {
                        pck.Load(stream);
                    }

                    var ws = pck.Workbook.Worksheets.First();
                    DataTable dt = new DataTable();
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        dt.Columns.Add(hasHeader
                            ? firstRowCell.Text
                            : string.Format("Column {0}", firstRowCell.Start.Column));
                    }

                    var startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = dt.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }

                    ArrayList add = new ArrayList();
                    ArrayList update = new ArrayList();
                    ArrayList error = new ArrayList();



                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                       
                        int QuestionNo = GetQuestionNO(Convert.ToInt32(dt.Rows[i]["QuestionId"]));

                                if ((dt.Rows[i]["QuestionId"].ToString() == "" || dt.Rows[i]["Question"].ToString() == "" || dt.Rows[i]["Option1"].ToString() == "" ||
                                    dt.Rows[i]["Option2"].ToString() == "" || dt.Rows[i]["Option3"].ToString() == "" || dt.Rows[i]["Option4"].ToString() == "" ||
                                    dt.Rows[i]["Points"].ToString() == "" || dt.Rows[i]["RightAnswer"].ToString() == "" || dt.Rows[i]["QuestionLevel"].ToString() == "" ||
                            dt.Rows[i]["GoldenQuestion"].ToString() == "" || dt.Rows[i]["QuestionArabic"].ToString() == "" || dt.Rows[i]["Option1Arabic"].ToString() == ""
                            || dt.Rows[i]["Option2Arabic"].ToString() == "" || dt.Rows[i]["Option3Arabic"].ToString() == "" || dt.Rows[i]["Option4Arabic"].ToString() == "" 
                            || dt.Rows[i]["RightAnswerArabic"].ToString() == ""))
                                {
                                    error.Add(i + 1 + ",");


                                }
                                else
                                {
                                    if (QuestionNo == 0)
                                    {
                                        UploadfromExcel(Convert.ToInt32(dt.Rows[i]["QuestionId"]),
                          dt.Rows[i]["Question"].ToString(),
                          dt.Rows[i]["Option1"].ToString(),
                          dt.Rows[i]["Option2"].ToString(), 
                          dt.Rows[i]["Option3"].ToString(),
                          dt.Rows[i]["Option4"].ToString(),
                          Convert.ToInt32( dt.Rows[i]["Points"]), 
                          dt.Rows[i]["RightAnswer"].ToString(),
                          dt.Rows[i]["QuestionLevel"].ToString(),
                          dt.Rows[i]["GoldenQuestion"].ToString(), 
                          dt.Rows[i]["QuestionArabic"].ToString(),
                          dt.Rows[i]["Option1Arabic"].ToString(),
                          dt.Rows[i]["Option2Arabic"].ToString(), 
                          dt.Rows[i]["Option3Arabic"].ToString(),
                          dt.Rows[i]["Option4Arabic"].ToString(),
                          dt.Rows[i]["RightAnswerArabic"].ToString());
                                        add.Add(i + 1 + ",");

                                    }
                                    else
                                    {
                                        UpdatefromExcel(Convert.ToInt32(dt.Rows[i]["QuestionId"]),
                          dt.Rows[i]["Question"].ToString(),
                          dt.Rows[i]["Option1"].ToString(),
                          dt.Rows[i]["Option2"].ToString(),
                          dt.Rows[i]["Option3"].ToString(),
                          dt.Rows[i]["Option4"].ToString(),
                          Convert.ToInt32(dt.Rows[i]["Points"]),
                          dt.Rows[i]["RightAnswer"].ToString(),
                          dt.Rows[i]["QuestionLevel"].ToString(),
                          dt.Rows[i]["GoldenQuestion"].ToString(),
                          dt.Rows[i]["QuestionArabic"].ToString(),
                          dt.Rows[i]["Option1Arabic"].ToString(),
                          dt.Rows[i]["Option2Arabic"].ToString(),
                          dt.Rows[i]["Option3Arabic"].ToString(),
                          dt.Rows[i]["Option4Arabic"].ToString(),
                          dt.Rows[i]["RightAnswerArabic"].ToString());
                                        update.Add(i + 1 + ",");
                                    }


                                }
                               
                        
                    }

                    if (add.Count > 0)
                    {
                        sbSuccess.Append(" Question No ");
                        for (int i = 0; i < add.Count; i++)
                        {
                            sbSuccess.Append(add[i].ToString());
                        }
                        sbSuccess.Append(" are Successfully Added.");
                    }
                    if (update.Count > 0)
                    {
                        Updated.Append(" Question No ");
                        for (int i = 0; i < update.Count; i++)
                        {
                            Updated.Append(update[i].ToString());
                        }
                        Updated.Append(" Successfully Updated.");
                    }
                    if (error.Count > 0)
                    {
                        sbFail.Append(" Question No ");
                        for (int i = 0; i < error.Count; i++)
                        {
                            sbFail.Append(error[i].ToString());
                        }
                        sbFail.Append(" failed to Upload, Kindly fix and re Upload.");
                    }

                   
                    ViewBag.Message = sbSuccess.ToString() + sbFail.ToString();

                    TempData["Messages"] = sbSuccess.ToString() + sbFail.ToString();
                    Session["MessageSucces"] = sbSuccess.ToString() + " " + Updated.ToString();
                    Session["MessageFailed"] = sbFail.ToString();


                   
                }
            }

            return Redirect(Url.Action("Index") + "?Message=" + Session["MessageSucces"] + "&MessageFailed=" + Session["MessageFailed"]);

        }
        public int GetQuestionNO(int QuestionId )
        {
            int questionID = 0;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "SELECT  [QuestionID]   FROM [Questions]   where Questionid ='" + QuestionId + "' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                questionID = Convert.ToInt32(dt.Rows[0]["QuestionId"]);
            }


            return questionID;
        }
    }
}