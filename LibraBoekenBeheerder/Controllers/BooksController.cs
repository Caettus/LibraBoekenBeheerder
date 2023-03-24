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


        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }
 
        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(BooksModel booksModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BooksController sdb = new BooksController(_configuration);
                    if (sdb.CreateBook(booksModel))
                    {
                        ViewBag.Message = "Book has been Added Successfully";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        private SqlConnection con;
        private void connection()
        {
            string connstring = _configuration.GetConnectionString("MyConnectionString");
            con = new SqlConnection(connstring);
        }


        public ActionResult Index()
        {
            List<BooksModel> mybooksList = new List<BooksModel>();

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

                // add explicit null check
                if (book != null)
                {
                    mybooksList.Add(book);
                }
            }

            return View(mybooksList);
        }


        public bool CreateBook(BooksModel booksModel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Books] ([Title], [Author], [ISBNNumber]) VALUES ('@Title', '@Author', '@ISBNNumber');", con);
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

        // public bool MayBookBeCreated(string title)
        // {
        //     bool result = false;
        //     if (!string.IsNullOrEmpty(title))
        //     {
        //         result = true;
        //     }
        //
        //     return result;
        // }

    }
}