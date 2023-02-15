using CSVMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Globalization;
using System.Reflection;

namespace CSVMVC.Controllers
{
    public class UploadController : Controller
    {
        string connectionString = @"Data Source = ACER\SQLEXPRESS; Initial Catalog = CSVMVCdb; Integrated Security=True";
        public ActionResult Index()
        {
            DataTable dtblTable_2 = new DataTable();
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter($"SELECT * FROM Employees ORDER BY Surname ASC;", sqlCon);
                sqlDa.Fill(dtblTable_2);
            }
            var model = new UploadTableModel();
            foreach (var t in dtblTable_2.Rows)
            {
                var row = t as DataRow;
                var item = new UploadModel
                {
                    ID = row.Field<int>("ID"),
                    Payroll_Number = row.Field<string>("Payroll_Number"),
                    Forenames = row.Field<string>("Forenames"),
                    Surname = row.Field<string>("Surname"),
                    Date_of_Birth = row.Field<DateTime>("Date_of_Birth"),
                    Telephone = row.Field<int>("Telephone"),
                    Mobile = row.Field<int>("Mobile"),
                    Address = row.Field<string>("Address"),
                    Address_2 = row.Field<string>("Address_2"),
                    Postcode = row.Field<string>("Postcode"),
                    EMail_Home = row.Field<string>("EMail_Home"),
                    Start_Date = row.Field<DateTime>("Start_Date")
                };
                model.Rows.Add(item);
            }
            return View(model);
        }

        private bool IsValidFile(HttpPostedFileBase postedFile)
        {
            var supportedTypes = new[] { "csv" };
            var extension = Path.GetExtension(postedFile.FileName).Substring(1);

            return supportedTypes.Contains(extension);
        }        
        
        private bool IsEmptyFile(HttpPostedFileBase postedFile)
        {
            if(postedFile is null)
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase postedFile, object sender, EventArgs e)
        {
            if (IsEmptyFile(postedFile))
            {
                Session["FileIsEmpty"] = true;
                Session["ErrorEmptyMessage"] = "No file uploaded!";

                return RedirectToAction("Index");
            }           
            
            if (!IsValidFile(postedFile))
            {
                Session["FileIsInvalid"] = true;
                Session["ErrorMessage"] = "Invalid file extension ";

                return RedirectToAction("Index");
            }            
            
            string filePath = string.Empty;
            if (postedFile != null)
            {

                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);
            }
            SqlCommand command = null;

            var lineNumber = 0;

            using (var sqlCon = new SqlConnection(connectionString))
            {
                // conn.Open();
                using (StreamReader reader = new StreamReader(filePath))
                {
                            sqlCon.Open();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        if (lineNumber != 0)
                        {
                            var values = line.Split(',');

                            using (command = new SqlCommand("INSERT INTO[dbo].[Employees] VALUES(@Payroll_Number, @Forenames, @Surname, @Date_of_Birth, @Telephone, @Mobile, @Address, @Address_2, @Postcode, @EMail_Home, @Start_Date)", sqlCon))
                            {
                                command.Parameters.Add(new SqlParameter("@Payroll_Number", values[0].ToString()));
                                command.Parameters.Add(new SqlParameter("@Forenames", values[1].ToString()));
                                command.Parameters.Add(new SqlParameter("@Surname", values[2].ToString()));
                                command.Parameters.Add(new SqlParameter("@Date_of_Birth", Convert.ToDateTime(values[3], CultureInfo.GetCultureInfo("uz-Latn-UZ"))));
                                command.Parameters.Add(new SqlParameter("@Telephone", Convert.ToInt32(values[4])));
                                command.Parameters.Add(new SqlParameter("@Mobile", Convert.ToInt32(values[5])));
                                command.Parameters.Add(new SqlParameter("@Address", values[6].ToString()));
                                command.Parameters.Add(new SqlParameter("@Address_2", values[7].ToString()));
                                command.Parameters.Add(new SqlParameter("@Postcode", values[8].ToString()));
                                command.Parameters.Add(new SqlParameter("@EMail_Home", values[9].ToString()));
                                command.Parameters.Add(new SqlParameter("@Start_Date", Convert.ToDateTime(values[10], CultureInfo.GetCultureInfo("uz-Latn-UZ"))));
                                command.Connection = sqlCon;

                                command.ExecuteNonQuery();
                            }
                        }

                        lineNumber++;
                    }
                    Session["Message"] = lineNumber - 1 + " rows were successfully processed!";
                }

                sqlCon.Close();
            }
            
            return RedirectToAction("Index");
        }


        // GET: Balance/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM Employees WHere ID = @ID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ID", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Balance/Edit/5
        public ActionResult Edit(int ID)
        {
            UploadModel uploadModel = new UploadModel();
            DataTable dtblUpload = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Employees Where ID = @ID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ID", ID);
                sqlDa.Fill(dtblUpload);
            }
            if (dtblUpload.Rows.Count == 1)
            {
                uploadModel.ID = Convert.ToInt32(dtblUpload.Rows[0][0].ToString());
                uploadModel.Payroll_Number = dtblUpload.Rows[0][1].ToString();
                uploadModel.Forenames = dtblUpload.Rows[0][2].ToString();
                uploadModel.Surname = dtblUpload.Rows[0][3].ToString();
                uploadModel.Date_of_Birth = Convert.ToDateTime(dtblUpload.Rows[0][4].ToString());
                uploadModel.Telephone = Convert.ToInt32(dtblUpload.Rows[0][5].ToString());
                uploadModel.Mobile = Convert.ToInt32(dtblUpload.Rows[0][6].ToString());
                uploadModel.Address = dtblUpload.Rows[0][7].ToString();
                uploadModel.Address_2 = dtblUpload.Rows[0][8].ToString();
                uploadModel.Postcode = dtblUpload.Rows[0][9].ToString();
                uploadModel.EMail_Home = dtblUpload.Rows[0][10].ToString();
                uploadModel.Start_Date = Convert.ToDateTime(dtblUpload.Rows[0][11].ToString());

                return View(uploadModel);
            }
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(UploadModel uploadModel)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "UPDATE Employees SET Payroll_Number = @Payroll_Number, Forenames = @Forenames, Surname = @Surname, Date_of_Birth = @Date_of_Birth, Telephone = @Telephone, Mobile = @Mobile, Address = @Address, Address_2 = @Address_2, Postcode = @Postcode, EMail_Home = @EMail_Home, Start_Date = @Start_Date WHERE ID = @ID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", uploadModel.ID);
                command.Parameters.AddWithValue("@Payroll_Number", uploadModel.Payroll_Number);
                command.Parameters.AddWithValue("@Forenames", uploadModel.Forenames);
                command.Parameters.AddWithValue("@Surname", uploadModel.Surname);
                command.Parameters.AddWithValue("@Date_of_Birth", uploadModel.Date_of_Birth);
                command.Parameters.AddWithValue("@Telephone", uploadModel.Telephone);
                command.Parameters.AddWithValue("@Mobile", uploadModel.Mobile);
                command.Parameters.AddWithValue("@Address", uploadModel.Address);
                command.Parameters.AddWithValue("@Address_2", uploadModel.Address_2);
                command.Parameters.AddWithValue("@Postcode", uploadModel.Postcode);
                command.Parameters.AddWithValue("@EMail_Home", uploadModel.EMail_Home);
                command.Parameters.AddWithValue("@Start_Date", uploadModel.Start_Date);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

    }
}