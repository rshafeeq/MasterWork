using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using Survey.Models;

namespace Survey.Controllers
{
    public class SurveyReportController : Controller
    {
        readonly string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        // GET: SurveyReport
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            dt = BindSurveysReport();

            return View(dt);
        }
        [HttpPost]
        public ActionResult Index(int id)
        {

            CreateExcelFile(id);
            return View();
        }
        public ActionResult CreateExcelFile(int id)
        {
            try
            {
                
                using (DataSet ds = BindSurveysResults(id))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        using (ExcelPackage xp = new ExcelPackage())
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                ExcelWorksheet ws = xp.Workbook.Worksheets.Add(dt.TableName);

                                int rowstart = 2;
                                int colstart = 2;
                                int rowend = rowstart;
                                int colend = colstart + dt.Columns.Count;

                                ws.Cells[rowstart, colstart, rowend, colend].Merge = true;
                                ws.Cells[rowstart, colstart, rowend, colend].Value = dt.Rows[0]["SurveyTitle"];
                                ws.Cells[rowstart, colstart, rowend, colend].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Font.Bold = true;
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                                rowstart += 2;
                                rowend = rowstart + dt.Rows.Count;
                                ws.Cells[rowstart, colstart].LoadFromDataTable(dt, true);
                                int i = 1;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    i++;
                                    if (dc.DataType == typeof(decimal))
                                        ws.Column(i).Style.Numberformat.Format = "#0.00";
                                }
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();



                                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                            }
                            Response.AddHeader("content-disposition", "attachment;filename=" + ds.Tables[0].Rows[0]["SurveyTitle"] + ".xlsx");
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.BinaryWrite(xp.GetAsByteArray());
                            Response.End();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            DataTable dt1 = new DataTable();
            dt1 = BindSurveysReport();

            return View(dt1);
        }



        public DataTable BindSurveysReport()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "SELECT distinct Surveys.SurveyID, [SurveyTitle],[SurveyTitleArabic]  " +
                    "FROM [Survey].[dbo].[Surveys] Inner Join Answers on Surveys .SurveyID = Answers.SurveyID ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            return dt;
        }
        public DataSet BindSurveysResults(int SurveyId)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "SELECT Surveys.SurveyTitle, Answers.[QuestionId], Questions.QuestionEnglish,Questions.QuestionArabic  ," +
                    "Answers.[Answer1English] Result ,Answers.[TextQuestionEnglish] [Result Text] ,Answers.[Answer1Arabic] [Result Arabic] ," +
                    "Answers.[TextQuestionArabic] [Result Text Arabic] , Users.UserName  FROM [Survey].[dbo].[Answers] " +
                    "left outer Join Questions on Answers.Questionid= Questions.QuestionID and Answers.SurveyID= Questions.SurveyID " +
                    "left outer Join Users on answers.UserId= users.UserID left Outer Join Surveys on Answers.SurveyID=Surveys.SurveyID where Answers.SurveyID='"+SurveyId+"'";


                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(ds);
            }
            return ds;
        }
    }
}