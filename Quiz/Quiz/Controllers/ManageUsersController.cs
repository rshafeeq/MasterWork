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
    public class ManageUsersController : Controller
    {
        readonly string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        // GET: ManageUsers
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql =
                    "SELECT UserID,UserName,Password,ltrim(rtrim(FirstName)) +' ' + ltrim(rtrim(LastName)) FullName,Email,Phone,Roles " +
                    "FROM Users Inner Join UsersRoles on Users.RoleId=UsersRoles.RoleID  ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            return View(dt);
        }

        // GET: ManageUsers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManageUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageUsers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection,ManageUsers manageUsers)
        {
            try
            {
                // TODO: Add insert logic here

                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "INSERT INTO [dbo].[Users] " +
                          "([RoleId],[UserName],[Password],[FirstName],[LastName],[Email],[Phone]) " +
                          "VALUES " +
                          "(@RoleId,@UserName,@Password,@FirstName,@LastName,@Email,@Phone)";
                var cmd = new SqlCommand(Sql, sqlcon);
                cmd.Parameters.AddWithValue("@RoleId", manageUsers.RoleId);
                cmd.Parameters.AddWithValue("@UserName", manageUsers.UserName);
                cmd.Parameters.AddWithValue("@Password", manageUsers.Password);
                cmd.Parameters.AddWithValue("@FirstName", manageUsers.FirstName);
                cmd.Parameters.AddWithValue("@LastName", manageUsers.LastName);
                cmd.Parameters.AddWithValue("@Email", manageUsers.Email);
                cmd.Parameters.AddWithValue("@Phone", manageUsers.Phone);
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


                return RedirectToAction("Index", "ManageUsers");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageUsers/Edit/5
        public ActionResult Edit(int id)
        {
            ManageUsers manageUsers = new ManageUsers();
            DataTable datatable = new DataTable();
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string sql =
                    "SELECT * from Users where UserID='" + id + "' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(datatable);
            }

            if (datatable.Rows.Count == 1)
            {
                manageUsers.RoleId = Convert.ToInt32(datatable.Rows[0]["RoleId"]);
                manageUsers.UserName = datatable.Rows[0]["UserName"].ToString();
                manageUsers.Password = datatable.Rows[0]["Password"].ToString();
                manageUsers.FirstName = datatable.Rows[0]["FirstName"].ToString();
                manageUsers.LastName = datatable.Rows[0]["LastName"].ToString();
                manageUsers.Email = datatable.Rows[0]["Email"].ToString();
                manageUsers.Phone = datatable.Rows[0]["Phone"].ToString();

                return View(manageUsers);

            }

            return RedirectToAction("Index", "ManageUsers");
        }

        // POST: ManageUsers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection,ManageUsers manageUsers)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(connString))
                {

                    string sql = "UPDATE dbo.Users    " +
                                 "SET RoleId =@RoleID, " +
                                 "UserName =@UserName, " +
                                 "Password =@Password, " +
                                 "FirstName =@FirstName," +
                                 "LastName =@LastName, " +
                                 "Email =@Email, " +
                                 "Phone =@Phone WHERE " +
                                 "UserID='" + id + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@RoleId", manageUsers.RoleId);
                    cmd.Parameters.AddWithValue("@UserName", manageUsers.UserName);
                    cmd.Parameters.AddWithValue("@Password", manageUsers.Password);
                    cmd.Parameters.AddWithValue("@FirstName", manageUsers.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", manageUsers.LastName);
                    cmd.Parameters.AddWithValue("@Email", manageUsers.Email);
                    cmd.Parameters.AddWithValue("@Phone", manageUsers.Phone);


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

                return RedirectToAction("Index", "ManageUsers");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageUsers/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add insert logic here

                SqlConnection sqlcon = new SqlConnection(connString);
                var Sql = "Delete from [dbo].[Users] where UserID=@UserID";
                var cmd = new SqlCommand(Sql, sqlcon);
                cmd.Parameters.AddWithValue("@UserID", id);
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
            return RedirectToAction("Index", "ManageUsers");
        }

        // POST: ManageUsers/Delete/5
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
        public ActionResult BindRoles()
        {
            string connString = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);
            string sql = "Select '0' [RoleID],'--Select--' [Roles] union Select [RoleID],[Roles] " +
                             "from UsersRoles order by RoleID ";
            SqlCommand cmd = new SqlCommand(sql, con);
            List<SelectListItem> rolenames = new List<SelectListItem>();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                con.Open();


                DataSet ds = new DataSet();
                da.Fill(ds);
                var RoleList = ds.Tables[0].DefaultView;

            }
            finally
            {
                con.Close();
                da.Dispose();
            }

            return View();
        }

        private static List<SelectListItem> PopulateRoles()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string connString = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                string sql = "Select '0' [RoleID],'--Select--' [Roles] union Select [RoleID],[Roles] " +
                             "from UsersRoles order by RoleID ";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["Roles"].ToString(),
                                Value = sdr["RoleID"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
    }
}
