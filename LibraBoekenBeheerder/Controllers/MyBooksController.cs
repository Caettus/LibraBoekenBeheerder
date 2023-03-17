using System;  
using System.Collections.Generic;  
using System.Configuration;  
using System.Data;  
using System.Data.SqlClient;  
using System.Web.Mvc; 
using LibraBoekenBeheerder.Models;

namespace LibraBoekenBeheerder.Controllers;

public class MyBooksController
{
    public class BooksController : Controller
    {
        public ActionResult Index()  
        {  
            List<Books> employeeList = new List<Books>();  
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;  
            using (SqlConnection con = new SqlConnection(CS))  
            {  
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", con);  
                cmd.CommandType = CommandType.Text;  
                con.Open();  
   
                SqlDataReader rdr = cmd.ExecuteReader();  
                while (rdr.Read())  
                {  
                    var employee = new Emplyee();  
   
                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);  
                    employee.Name = rdr["Name"].ToString();  
                    employee.Gender = rdr["Gender"].ToString();  
                    employee.Age = Convert.ToInt32(rdr["Age"]);  
                    employee.Position = rdr["Position"].ToString();  
                    employee.Office = rdr["Office"].ToString();  
                    employee.HireDate =Convert.ToDateTime(rdr["HireDate"]);  
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);  
                    employeeList.Add(employee);  
                }  
            }  
            return View(employeeList);   
        }  
    }

}