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
 
        [HttpPost]
        public ActionResult Create(BooksModel booksModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BooksController bookcontroller = new BooksController(_configuration);
                    if (bookcontroller.CreateBook(booksModel))
                    {
                        ViewBag.Message = "Book has been Added Successfully";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "Error occurred while creating the book";
                    }
                }
                else if (!ModelState.IsValid)
                {
                    ViewBag.Message = "Fucking kutding";
                }
            }
            catch
            {
                ViewBag.Message = "Error occurred while creating the book";
            }

            // Return the view with the booksModel object passed as parameter
            return View(booksModel);
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
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Books] ([Title], [Author], [ISBNNumber], [Summary], [Pages], [PagesRead]) VALUES (@Title, @Author, @ISBNNumber, @Summary, @Pages, @PagesRead);", con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Title", booksModel.Title);
            cmd.Parameters.AddWithValue("@Author", booksModel.Author);
            cmd.Parameters.AddWithValue("@ISBNNumber", booksModel.ISBNNumber);
            cmd.Parameters.AddWithValue("@Summary", booksModel.Summary);
            cmd.Parameters.AddWithValue("@Pages", booksModel.Pages);
            cmd.Parameters.AddWithValue("@PagesRead", booksModel.PagesRead);

            con.Open();
            var i = cmd.ExecuteNonQuery();
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