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



namespace Survey.Controllers
{
    public class UploadQuestionsController : Controller
    {
        readonly string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        
        // GET: UploadQuestions
        
        public ActionResult Index()
        {
            DataSet ds = BindSurveysDDl();
            ViewBag.Surveys = ds.Tables[0];
            List<SelectListItem> GetSurveys = new List<SelectListItem>();
            foreach (DataRow dr in ViewBag.Surveys.Rows)
            {
                GetSurveys.Add(new SelectListItem
                { Text = @dr["SurveyTitle"].ToString(), Value = @dr["SurveyID"].ToString() });
            }

            ViewBag.Surveylist = GetSurveys;

            
            
            return View();
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

        //public ActionResult Create(ManageQuestions ManageQuestions)
        //{
        //    try
        //    {
        //        string SurveyDDLValue = Request.Form["Surveylist"].ToString();
        //        //string QuestionGroupDDLValue = Request.Form["QuestionGrouplist"].ToString();
        //        //string QuestionTypeDDLValue = Request.Form["QuestionTypelist"].ToString();
        //        // TODO: Add insert logic here

        //        SqlConnection sqlcon = new SqlConnection(connString);
        //        var Sql = "INSERT INTO dbo.Questions " +
        //                  "(QuestionTypeId,QuestionID,QuestionEnglish,QuestionArabic,Answer1English,Answer2English,Answer3English,Answer4English,Answer5English,Answer1Arabic,Answer2Arabic,Answer3Arabic,Answer4Arabic,Answer5Arabic,SurveyID,QuestionGroupID) " +
        //                  "VALUES " +
        //                  "('" + ManageQuestions.QuestionTypeID +
        //                  "' ,(case when (Select max(QuestionID)+1 from [Questions] where SurveyID='" + SurveyDDLValue +
        //                  "') is null then 1 else (Select max(QuestionID)+1 from Questions where SurveyID='" +
        //                  SurveyDDLValue + "') end )" +
        //                  ",'" + ManageQuestions.QuestionE + "' ,N'" + ManageQuestions.QuestionA + "' ,'" +
        //                  ManageQuestions.Answer1E + "'  ,'" + ManageQuestions.Answer2E + "'  ,'" +
        //                  ManageQuestions.Answer3E + "'  " +
        //                  " ,'" + ManageQuestions.Answer4E + "','" + ManageQuestions.Answer5E + "' , N'" +
        //                  ManageQuestions.Answer1A + "'  ,N'" + ManageQuestions.Answer2A + "'  ,N'" +
        //                  ManageQuestions.Answer3A + "'  ,N'" + ManageQuestions.Answer4A + "'  ,N'" +
        //                  ManageQuestions.Answer5A + "'  ,'" + SurveyDDLValue + "','" +
        //                  ManageQuestions.QuestionGroupID + "')";
        //        var cmd = new SqlCommand(Sql, sqlcon);

        //        try
        //        {
        //            sqlcon.Open();
        //            cmd.ExecuteNonQuery();

        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        finally
        //        {
        //            sqlcon.Close();
        //            cmd.Dispose();
        //        }


        //        return RedirectToAction("Index", "ManageQuestions");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public void UploadfromExcel(int QuestiontypeId, int QuestionId, string QuestionEnglish, string QuestionArabic,
            string Answer1English, string Answer2English, string Answer3English, string Answer4English,
            string Answer5English, string Answer1Arabic, string Answer2Arabic, string Answer3Arabic,
            string Answer4Arabic,
            string Answer5Arabic, int surveyID, int QuestionGroupID)
        {
            try
            {

               
                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "INSERT INTO dbo.Questions " +
                          "(QuestionTypeId,QuestionID,QuestionEnglish,QuestionArabic,Answer1English,Answer2English,Answer3English,Answer4English,Answer5English,Answer1Arabic,Answer2Arabic,Answer3Arabic,Answer4Arabic,Answer5Arabic,SurveyID,QuestionGroupID) " +
                          "VALUES " +
                          "('" + QuestiontypeId + "', '"+ QuestionId+ "','" + QuestionEnglish + "' ,N'" + QuestionArabic + "' ,'" + Answer1English + "'  ,'" +
                          Answer2English + "'  ,'" + Answer3English + "'  " +
                          " ,'" + Answer4English + "','" + Answer5English + "' , N'" + Answer1Arabic + "'  ,N'" +
                          Answer2Arabic + "'  ,N'" + Answer3Arabic + "'  ,N'" + Answer4Arabic + "'  ,N'" +
                          Answer5Arabic + "'  ,'" + surveyID + "','" + QuestionGroupID + "')";
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
        public void UpdatefromExcel(int QuestiontypeId, int QuestionId, string QuestionEnglish, string QuestionArabic,
            string Answer1English, string Answer2English, string Answer3English, string Answer4English,
            string Answer5English, string Answer1Arabic, string Answer2Arabic, string Answer3Arabic,
            string Answer4Arabic,
            string Answer5Arabic, int surveyID, int QuestionGroupID)
        {
            try
            {


                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "Update Questions " +
                          "set  QuestionTypeId='" + QuestiontypeId + "',QuestionID='" + QuestionId + "',QuestionEnglish='" + QuestionEnglish + "'," +
                          "QuestionArabic=N'" + QuestionArabic + "',Answer1English='" + Answer1English + "',Answer2English='" + Answer2English + "'," +
                          "Answer3English='" + Answer3English + "',Answer4English='"+ Answer4English + "',Answer5English='"+ Answer5English + "'," +
                          "Answer1Arabic= N'"+ Answer1Arabic + "',Answer2Arabic=N'" + Answer2Arabic+ "N',Answer3Arabic=N'" + Answer3Arabic + "',Answer4Arabic=N'" + Answer4Arabic + "'," +
                          "Answer5Arabic=N'" + Answer5Arabic  + "',SurveyID='"+ surveyID + "',QuestionGroupID='"+ QuestionGroupID + "' where QuestionID='"+QuestionId+"' and SurveyID='"+surveyID+"' ";
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


        //[HttpPost]
        //public ActionResult fileUpload1(HttpPostedFileBase postedFile)
        //{
        //    string SurveyDDLValue = Request.QueryString["surveyId"];
        //    string filePath;
        //    // Session["SurveyDDLValue"].ToString();
        //    if (postedFile != null)
        //    {
        //        string path = Server.MapPath("~/Uploads/");
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        filePath = path + Path.GetFileName(postedFile.FileName);
        //        string extension = Path.GetExtension(postedFile.FileName);
        //        postedFile.SaveAs(filePath);

        //        string conString = string.Empty;
        //        switch (extension)
        //        {
        //            case ".xls": //Excel 97-03.
        //                conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
        //                break;
        //            case ".xlsx": //Excel 07 and above.
        //                conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
        //                break;
        //        }

        //        DataTable dt = new DataTable();
        //        conString = string.Format(conString, filePath);

        //        using (OleDbConnection connExcel = new OleDbConnection(conString))
        //        {
        //            using (OleDbCommand cmdExcel = new OleDbCommand())
        //            {
        //                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
        //                {
        //                    cmdExcel.Connection = connExcel;

        //                    //Get the name of First Sheet.
        //                    connExcel.Open();
        //                    DataTable dtExcelSchema;
        //                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        //                    connExcel.Close();

        //                    //Read Data from First Sheet.
        //                    connExcel.Open();
        //                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
        //                    odaExcel.SelectCommand = cmdExcel;
        //                    odaExcel.Fill(dt);
        //                    connExcel.Close();
        //                }
        //            }
        //        }

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            UploadfromExcel(Convert.ToInt32(dt.Rows[i]["QuestiontypeId"]),
        //                dt.Rows[i]["QuestionEnglish"].ToString(),
        //                dt.Rows[i]["QuestionArabic"].ToString(), dt.Rows[i]["Answer1English"].ToString(),
        //                dt.Rows[i]["Answer2English"].ToString(),
        //                dt.Rows[i]["Answer3English"].ToString(), dt.Rows[i]["Answer4English"].ToString(),
        //                dt.Rows[i]["Answer5English"].ToString(),
        //                dt.Rows[i]["Answer1Arabic"].ToString(), dt.Rows[i]["Answer2Arabic"].ToString(),
        //                dt.Rows[i]["Answer3Arabic"].ToString(),
        //                dt.Rows[i]["Answer4Arabic"].ToString(), dt.Rows[i]["Answer5Arabic"].ToString(),
        //                Convert.ToInt32(SurveyDDLValue),
        //                Convert.ToInt32(dt.Rows[i]["QuestionGroupID"]));
        //        }
        //        /*conString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        //        using (SqlConnection con = new SqlConnection(conString))
        //        {
        //            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
        //            {
        //                //Set the database table name.
        //                sqlBulkCopy.DestinationTableName = "dbo.Questions";

        //                //[OPTIONAL]: Map the Excel columns with that of the database table
        //                sqlBulkCopy.ColumnMappings.Add("QuestiontypeId", "QuestiontypeId");
        //                sqlBulkCopy.ColumnMappings.Add("QuestionID", "QuestionID");
        //                sqlBulkCopy.ColumnMappings.Add("QuestionEnglish", "QuestionEnglish");
        //                sqlBulkCopy.ColumnMappings.Add("QuestionArabic", "QuestionArabic");
        //                sqlBulkCopy.ColumnMappings.Add("Answer1English", "Answer1English");
        //                sqlBulkCopy.ColumnMappings.Add("Answer2English", "Answer2English");
        //                sqlBulkCopy.ColumnMappings.Add("Answer3English", "Answer3English");
        //                sqlBulkCopy.ColumnMappings.Add("Answer4English", "Answer4English");
        //                sqlBulkCopy.ColumnMappings.Add("Answer5English", "Answer5English");
        //                sqlBulkCopy.ColumnMappings.Add("Answer1Arabic", "Answer1Arabic");
        //                sqlBulkCopy.ColumnMappings.Add("Answer2Arabic", "Answer2Arabic");
        //                sqlBulkCopy.ColumnMappings.Add("Answer3Arabic", "Answer3Arabic");
        //                sqlBulkCopy.ColumnMappings.Add("Answer4Arabic", "Answer4Arabic");
        //                sqlBulkCopy.ColumnMappings.Add("Answer5Arabic", "Answer5Arabic");
        //                sqlBulkCopy.ColumnMappings.Add("SurveyID", "SurveyID");
        //                sqlBulkCopy.ColumnMappings.Add("QuestionGroupID", "QuestionGroupID");

        //                con.Open();
        //                sqlBulkCopy.WriteToServer(dt);
        //                con.Close();
        //            }
        //        }
        //    }*/

        //    }

        //    return RedirectToAction("Index", "ManageQuestions");
        //}

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            StringBuilder sbFail = new StringBuilder();
            StringBuilder sbSuccess = new StringBuilder();
            StringBuilder Updated = new StringBuilder();
            bool hasHeader = true;
            //if (string.IsNullOrEmpty(Request.QueryString["surveyId"]))
            //{
            //    return Redirect(Url.Action("Index", "UploadQuestions") + "?MessageSurvey=Kindly Select Survey");
            //}
            string SurveyDDLValue = Request.QueryString["surveyId"];
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
                        int QuestionTypeID = GetQuestionType(dt.Rows[i]["QuestiontypeId"].ToString());
                        int QuestionNo = GetQuestionNO(Convert.ToInt32(dt.Rows[i]["QuestionNo"]), Convert.ToInt32(SurveyDDLValue));



                        switch (QuestionTypeID)
                        {
                            case 1:
                                if ((dt.Rows[i]["QuestionEnglish"].ToString() == "" || dt.Rows[i]["QuestionArabic"].ToString() == "" || dt.Rows[i]["Answer1English"].ToString() == "" ||
                                    dt.Rows[i]["Answer2English"].ToString() == "" || dt.Rows[i]["Answer3English"].ToString() == "" || dt.Rows[i]["Answer4English"].ToString() == "" ||
                                    dt.Rows[i]["Answer5English"].ToString() == "" || dt.Rows[i]["Answer1Arabic"].ToString() == "" || dt.Rows[i]["Answer2Arabic"].ToString() == "" ||
                            dt.Rows[i]["Answer3Arabic"].ToString() == "" || dt.Rows[i]["Answer4Arabic"].ToString() == "" || dt.Rows[i]["Answer5Arabic"].ToString() == ""))
                                {
                                    error.Add(i + 1 + ",");


                                }
                                else
                                {
                                    if (QuestionNo == 0)
                                    {
                                        UploadfromExcel(QuestionTypeID,
                          Convert.ToInt32(dt.Rows[i]["QuestionNo"]),
                          dt.Rows[i]["QuestionEnglish"].ToString(),
                          dt.Rows[i]["QuestionArabic"].ToString(), dt.Rows[i]["Answer1English"].ToString(),
                          dt.Rows[i]["Answer2English"].ToString(),
                          dt.Rows[i]["Answer3English"].ToString(), dt.Rows[i]["Answer4English"].ToString(),
                          dt.Rows[i]["Answer5English"].ToString(),
                          dt.Rows[i]["Answer1Arabic"].ToString(), dt.Rows[i]["Answer2Arabic"].ToString(),
                          dt.Rows[i]["Answer3Arabic"].ToString(),
                          dt.Rows[i]["Answer4Arabic"].ToString(), dt.Rows[i]["Answer5Arabic"].ToString(),
                          Convert.ToInt32(SurveyDDLValue),
                          Convert.ToInt32(dt.Rows[i]["QuestionGroupID"]));
                                        add.Add(i + 1 + ",");

                                    }
                                    else
                                    {
                                        UpdatefromExcel(QuestionTypeID,
                         Convert.ToInt32(dt.Rows[i]["QuestionNo"]),
                         dt.Rows[i]["QuestionEnglish"].ToString(),
                         dt.Rows[i]["QuestionArabic"].ToString(), dt.Rows[i]["Answer1English"].ToString(),
                         dt.Rows[i]["Answer2English"].ToString(),
                         dt.Rows[i]["Answer3English"].ToString(), dt.Rows[i]["Answer4English"].ToString(),
                         dt.Rows[i]["Answer5English"].ToString(),
                         dt.Rows[i]["Answer1Arabic"].ToString(), dt.Rows[i]["Answer2Arabic"].ToString(),
                         dt.Rows[i]["Answer3Arabic"].ToString(),
                         dt.Rows[i]["Answer4Arabic"].ToString(), dt.Rows[i]["Answer5Arabic"].ToString(),
                         Convert.ToInt32(SurveyDDLValue),
                         Convert.ToInt32(dt.Rows[i]["QuestionGroupID"]));
                                        update.Add(i + 1 + ",");
                                    }


                                }
                                break;
                            case 2:
                                if (dt.Rows[i]["QuestionEnglish"].ToString() == "" || dt.Rows[i]["QuestionArabic"].ToString() == "" || dt.Rows[i]["Answer1English"].ToString() == "" ||
                                   dt.Rows[i]["Answer2English"].ToString() == "" || dt.Rows[i]["Answer3English"].ToString() == "" || dt.Rows[i]["Answer4English"].ToString() == "" ||
                                   dt.Rows[i]["Answer5English"].ToString() == "" || dt.Rows[i]["Answer1Arabic"].ToString() == "" || dt.Rows[i]["Answer2Arabic"].ToString() == "" ||
                           dt.Rows[i]["Answer3Arabic"].ToString() == "" || dt.Rows[i]["Answer4Arabic"].ToString() == "" || dt.Rows[i]["Answer5Arabic"].ToString() == "")
                                {
                                    error.Add(i + 1 + ",");
                                }
                                else
                                {
                                    if (QuestionNo==0)
                                    {
                                        UploadfromExcel(QuestionTypeID,
                                            Convert.ToInt32(dt.Rows[i]["QuestionNo"]),
                               dt.Rows[i]["QuestionEnglish"].ToString(),
                               dt.Rows[i]["QuestionArabic"].ToString(), dt.Rows[i]["Answer1English"].ToString(),
                               dt.Rows[i]["Answer2English"].ToString(),
                               dt.Rows[i]["Answer3English"].ToString(), dt.Rows[i]["Answer4English"].ToString(),
                               dt.Rows[i]["Answer5English"].ToString(),
                               dt.Rows[i]["Answer1Arabic"].ToString(), dt.Rows[i]["Answer2Arabic"].ToString(),
                               dt.Rows[i]["Answer3Arabic"].ToString(),
                               dt.Rows[i]["Answer4Arabic"].ToString(), dt.Rows[i]["Answer5Arabic"].ToString(),
                               Convert.ToInt32(SurveyDDLValue),
                               Convert.ToInt32(dt.Rows[i]["QuestionGroupID"]));
                                        add.Add(i + 1 + ",");
                                    }
                                    else
                                    {
                                        UpdatefromExcel(QuestionTypeID,
                                            Convert.ToInt32(dt.Rows[i]["QuestionNo"]),
                               dt.Rows[i]["QuestionEnglish"].ToString(),
                               dt.Rows[i]["QuestionArabic"].ToString(), dt.Rows[i]["Answer1English"].ToString(),
                               dt.Rows[i]["Answer2English"].ToString(),
                               dt.Rows[i]["Answer3English"].ToString(), dt.Rows[i]["Answer4English"].ToString(),
                               dt.Rows[i]["Answer5English"].ToString(),
                               dt.Rows[i]["Answer1Arabic"].ToString(), dt.Rows[i]["Answer2Arabic"].ToString(),
                               dt.Rows[i]["Answer3Arabic"].ToString(),
                               dt.Rows[i]["Answer4Arabic"].ToString(), dt.Rows[i]["Answer5Arabic"].ToString(),
                               Convert.ToInt32(SurveyDDLValue),
                               Convert.ToInt32(dt.Rows[i]["QuestionGroupID"]));
                                        update.Add(i + 1 + ",");
                                    }

                                }
                                break;
                            case 3:
                                if (dt.Rows[i]["QuestionEnglish"].ToString() == "" || dt.Rows[i]["QuestionArabic"].ToString() == "" || dt.Rows[i]["Answer1English"].ToString() == "" ||
                                   dt.Rows[i]["Answer2English"].ToString() == "" || dt.Rows[i]["Answer1Arabic"].ToString() == "" || dt.Rows[i]["Answer2Arabic"].ToString() == "")
                                {
                                    error.Add(i + 1 + ",");
                                }
                                else
                                {
                                    if ( QuestionNo == 0)
                                    {
                                        UploadfromExcel(QuestionTypeID,
                                            Convert.ToInt32(dt.Rows[i]["QuestionNo"]),
                               dt.Rows[i]["QuestionEnglish"].ToString(),
                               dt.Rows[i]["QuestionArabic"].ToString(), dt.Rows[i]["Answer1English"].ToString(),
                               dt.Rows[i]["Answer2English"].ToString(),
                               dt.Rows[i]["Answer3English"].ToString(), dt.Rows[i]["Answer4English"].ToString(),
                               dt.Rows[i]["Answer5English"].ToString(),
                               dt.Rows[i]["Answer1Arabic"].ToString(), dt.Rows[i]["Answer2Arabic"].ToString(),
                               dt.Rows[i]["Answer3Arabic"].ToString(),
                               dt.Rows[i]["Answer4Arabic"].ToString(), dt.Rows[i]["Answer5Arabic"].ToString(),
                               Convert.ToInt32(SurveyDDLValue),
                               Convert.ToInt32(dt.Rows[i]["QuestionGroupID"]));
                                        add.Add(i + 1 + ",");
                                    }
                                    else
                                    {
                                        UpdatefromExcel(QuestionTypeID,
                                           Convert.ToInt32(dt.Rows[i]["QuestionNo"]),
                              dt.Rows[i]["QuestionEnglish"].ToString(),
                              dt.Rows[i]["QuestionArabic"].ToString(), dt.Rows[i]["Answer1English"].ToString(),
                              dt.Rows[i]["Answer2English"].ToString(),
                              dt.Rows[i]["Answer3English"].ToString(), dt.Rows[i]["Answer4English"].ToString(),
                              dt.Rows[i]["Answer5English"].ToString(),
                              dt.Rows[i]["Answer1Arabic"].ToString(), dt.Rows[i]["Answer2Arabic"].ToString(),
                              dt.Rows[i]["Answer3Arabic"].ToString(),
                              dt.Rows[i]["Answer4Arabic"].ToString(), dt.Rows[i]["Answer5Arabic"].ToString(),
                              Convert.ToInt32(SurveyDDLValue),
                              Convert.ToInt32(dt.Rows[i]["QuestionGroupID"]));
                                        update.Add(i + 1 + ",");
                                    }

                                }
                                break;
                            case 5:
                                if (dt.Rows[i]["QuestionEnglish"].ToString() == "" || dt.Rows[i]["QuestionArabic"].ToString() == "")
                                {
                                    error.Add(i + 1 + ",");
                                }
                                else
                                {
                                    if (QuestionNo == 0)
                                    {


                                        UploadfromExcel(QuestionTypeID,
                                            Convert.ToInt32(dt.Rows[i]["QuestionNo"]),
                               dt.Rows[i]["QuestionEnglish"].ToString(),
                               dt.Rows[i]["QuestionArabic"].ToString(), dt.Rows[i]["Answer1English"].ToString(),
                               dt.Rows[i]["Answer2English"].ToString(),
                               dt.Rows[i]["Answer3English"].ToString(), dt.Rows[i]["Answer4English"].ToString(),
                               dt.Rows[i]["Answer5English"].ToString(),
                               dt.Rows[i]["Answer1Arabic"].ToString(), dt.Rows[i]["Answer2Arabic"].ToString(),
                               dt.Rows[i]["Answer3Arabic"].ToString(),
                               dt.Rows[i]["Answer4Arabic"].ToString(), dt.Rows[i]["Answer5Arabic"].ToString(),
                               Convert.ToInt32(SurveyDDLValue),
                               Convert.ToInt32(dt.Rows[i]["QuestionGroupID"]));
                                        add.Add(i + 1 + ",");
                                    }
                                    else
                                    {
                                        UpdatefromExcel(QuestionTypeID,
                                            Convert.ToInt32(dt.Rows[i]["QuestionNo"]),
                               dt.Rows[i]["QuestionEnglish"].ToString(),
                               dt.Rows[i]["QuestionArabic"].ToString(), dt.Rows[i]["Answer1English"].ToString(),
                               dt.Rows[i]["Answer2English"].ToString(),
                               dt.Rows[i]["Answer3English"].ToString(), dt.Rows[i]["Answer4English"].ToString(),
                               dt.Rows[i]["Answer5English"].ToString(),
                               dt.Rows[i]["Answer1Arabic"].ToString(), dt.Rows[i]["Answer2Arabic"].ToString(),
                               dt.Rows[i]["Answer3Arabic"].ToString(),
                               dt.Rows[i]["Answer4Arabic"].ToString(), dt.Rows[i]["Answer5Arabic"].ToString(),
                               Convert.ToInt32(SurveyDDLValue),
                               Convert.ToInt32(dt.Rows[i]["QuestionGroupID"]));
                                        update.Add(i + 1 + ",");
                                    }


                                }
                                break;
                        }
                    }

                    if(add.Count>0)
                    {
                        sbSuccess.Append(" Question No ");
                        for (int i = 0; i < add.Count; i++)
                        {
                            sbSuccess.Append(add[i].ToString());
                        }
                        sbSuccess.Append(" are Successfully Added.");
                    }
                    if(update.Count>0)
                    {
                        Updated.Append(" Question No ");
                        for (int i = 0; i < update.Count; i++)
                        {
                            Updated.Append(update[i].ToString() );
                        }
                        Updated.Append(" Successfully Updated.");
                    }
                    if(error.Count>0)
                    {
                        sbFail.Append(" Question No ");
                        for (int i = 0; i < error.Count; i++)
                        {
                            sbFail.Append(error[i].ToString());
                        }
                        sbFail.Append(" failed to Upload, Kindly fix and re Upload.");
                    }

                    //sbSuccess.Append(" Are Successfully Uploaded ");
                    //sbSuccess.Append(" Are Successfully Uploaded. ");
                    //sbSuccess.Append("and ");
                    //sbFail.Append(" Has Problem kindly fix them and re-submit ");




                    ViewBag.Message = sbSuccess.ToString() + sbFail.ToString();



                    TempData["Messages"] = sbSuccess.ToString() + sbFail.ToString();



                    Session["MessageSucces"] = sbSuccess.ToString() +" "+ Updated.ToString() ;

                    Session["MessageFailed"] = sbFail.ToString();


                    DataSet ds = BindSurveysDDl();

                    ViewBag.Surveys = ds.Tables[0];


                    List<SelectListItem> GetSurveys = new List<SelectListItem>();
                    foreach (DataRow dr in ViewBag.Surveys.Rows)
                    {
                        GetSurveys.Add(new SelectListItem
                        { Text = @dr["SurveyTitle"].ToString(), Value = @dr["SurveyID"].ToString() });
                    }
                    ViewBag.Surveylist = GetSurveys;
                }
            }

            return Redirect(Url.Action("Index") + "?Message=" + Session["MessageSucces"] + "&MessageFailed=" + Session["MessageFailed"] );

        }
        public int GetQuestionType(string QuestionType)
        {
            int questionTypeID = 0;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "SELECT  [QuestionTypeId] FROM [Survey].[dbo].[QuestionType]  where QuestionTypeEnglish = '"+ QuestionType + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            if(dt.Rows.Count>0)
            {
                questionTypeID = Convert.ToInt32(dt.Rows[0]["QuestionTypeId"]);
            }
            
            
            return questionTypeID;
        }
        public int GetQuestionNO(int QuestionId, int SurveyID)
        {
            int questionID = 0;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql = "SELECT  [QuestionID]   FROM [Survey].[dbo].[Questions]   where Questionid ='"+QuestionId+"' and SurveyId='"+SurveyID+"'";
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