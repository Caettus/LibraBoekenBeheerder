using System.Data;
using LibraBoekenBeheerder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LibraDB;

public class BooksDAL
{
    private readonly IConfiguration _configuration;

    public BooksDAL(IConfiguration configuration)
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
            List<BooksDTO> mybooksList = new List<BooksDTO>();

            connection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Books", con);
            cmd.CommandType = CommandType.Text;
            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var book = new BooksDTO();

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
    }
}