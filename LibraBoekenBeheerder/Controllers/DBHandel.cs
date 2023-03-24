using System;  
using System.Collections.Generic;
using System.Data;
using LibraBoekenBeheerder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
namespace LibraBoekenBeheerder.Controllers;

public class DBHandel
{
    private readonly IConfiguration _configuration;
        public DBHandel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection con;
        private void connection()
        {
            string connstring = _configuration.GetConnectionString("MyConnectionString");
            con = new SqlConnection(connstring);
        }


        public ActionResult Index()
        {  
            List<BooksController> mybooksList = new List<BooksController>();

            connection();   
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Books", con);  
                cmd.CommandType = CommandType.Text;  
                con.Open();  
   
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var book = new BooksModel();

                    book.BookId = Convert.ToInt32(rdr["BookId"]);
                    book.Title = rdr["title"].ToString();
                    book.Author = rdr["author"].ToString();
                    book.ISBNNumber = rdr["isbnnumber"].ToString();
                    book.Pages = Convert.ToInt32(rdr["pages"]);
                    book.PagesRead = Convert.ToInt32(rdr["pagesread"]);
                    book.Summary = rdr["summary"].ToString();
                    mybooksList.Add(book);
                }
                return View(mybooksList);   
        }
        
        public bool CreateBook(BooksModel booksModel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Books values (@Title, @Author, @ISBNNumber)", con);
            cmd.CommandType = CommandType.StoredProcedure;
 
            cmd.Parameters.AddWithValue("@Title", booksModel.Title);
            cmd.Parameters.AddWithValue("@Author", booksModel.Author);
            cmd.Parameters.AddWithValue("@ISBNNumber", booksModel.ISBNNumber);
 
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
 
            if (i >= 1)
                return true;
            else
                return false;
        }
}