using System;  
using System.Collections.Generic;
using System.Data;
using LibraBoekenBeheerder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace LibraBoekenBeheerder.Controllers
{
    public class BooksController : Controller
    {
        private readonly IConfiguration _configuration;

        public BooksController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult Index()  
        {  
            List<Books> mybooksList = new List<Books>();
            string connstring = _configuration.GetConnectionString("MyConnectionString");

            using (SqlConnection con = new SqlConnection(connstring))  
            {  
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Books", con);  
                cmd.CommandType = CommandType.Text;  
                con.Open();  
   
                SqlDataReader rdr = cmd.ExecuteReader();  
                while (rdr.Read())
                {
                    var book = new Books(); 
   
                    book.BookId = Convert.ToInt32(rdr["BookId"]);  
                    book.Title = rdr["title"].ToString();  
                    book.Author = rdr["author"].ToString();  
                    book.ISBNNumber = rdr["isbnnumber"].ToString();   
                    book.Pages = Convert.ToInt32(rdr["pages"]);   
                    book.PagesRead = Convert.ToInt32(rdr["pagesread"]);   
                    book.Summary =rdr["summary"].ToString(); 
                    mybooksList.Add(book);  
                }  
            }  
            return View(mybooksList);   
        }

        [HttpPost]
        public ActionResult CreateBook(Books bookmodel)
        {
            if (ModelState.IsValid)
            {
                //TODO: SubscribeUser(model.Email);
            }

            return View("Index", bookmodel);
        }
    }
}