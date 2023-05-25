using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using LibraDTO;
using Microsoft.Extensions.Configuration;
using LibraInterface;

namespace LibraDBWW;

public class BooksDAL : IBooks
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


    public List<BooksDTO> GetAllBooks()
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

            if (book != null)
            {
                mybooksList.Add(book);
            }
        }
        con.Close();
        return mybooksList;
    }

    public BooksDTO GetABook(int id)
    {
        connection();
        SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Books WHERE BookId = @id", con);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.CommandType = CommandType.Text;
        con.Open();

        SqlDataReader rdr = cmd.ExecuteReader();
        BooksDTO book = null;
        if (rdr.Read())
        {
            book = new BooksDTO();
            book.BookId = Convert.ToInt32(rdr["BookId"]);
            book.Title = rdr["title"].ToString();
            book.Author = rdr["author"].ToString();
            book.ISBNNumber = rdr["isbnnumber"].ToString();
            book.Pages = Convert.ToInt32(rdr["pages"]);
            book.PagesRead = Convert.ToInt32(rdr["pagesread"]);
            book.Summary = rdr["summary"].ToString();
        }
        con.Close();
        return book;
    }


    public bool CreateBook(BooksDTO booksDTO)
    {
        connection();
        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Books] ([Title], [Author], [ISBNNumber], [Summary], [Pages], [PagesRead]) VALUES (@Title, @Author, @ISBNNumber, @Summary, @Pages, @PagesRead);", con);
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@Title", booksDTO.Title);
        cmd.Parameters.AddWithValue("@Author", booksDTO.Author);
        cmd.Parameters.AddWithValue("@ISBNNumber", booksDTO.ISBNNumber);
        cmd.Parameters.AddWithValue("@Summary", booksDTO.Summary);
        cmd.Parameters.AddWithValue("@Pages", booksDTO.Pages);
        cmd.Parameters.AddWithValue("@PagesRead", booksDTO.PagesRead);

        con.Open();
        var i = cmd.ExecuteNonQuery();
        con.Close();

        if (i >= 1)
            return true;
        else
            return false;
    }

    public bool EditBook(BooksDTO booksDTO, int id)
    {
        connection();
        SqlCommand cmd = new SqlCommand("UPDATE dbo.Books SET Title = @Title, Author = @Author, ISBNNumber = @ISBNNumber, Summary = @Summary, Pages = @Pages, PagesRead = @PagesRead WHERE BookId = @BookId;", con);
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@BookId", id);
        cmd.Parameters.AddWithValue("@Title", booksDTO.Title);
        cmd.Parameters.AddWithValue("@Author", booksDTO.Author);
        cmd.Parameters.AddWithValue("@ISBNNumber", booksDTO.ISBNNumber);
        cmd.Parameters.AddWithValue("@Summary", booksDTO.Summary);
        cmd.Parameters.AddWithValue("@Pages", booksDTO.Pages);
        cmd.Parameters.AddWithValue("@PagesRead", booksDTO.PagesRead);

        con.Open();
        int affectedRows = 0;
        try
        {
            affectedRows = cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            if (ex.Number == 547) // Check for foreign key constraint violation
            {
                // Handle the error when the foreign key constraint is violated
                // You can provide appropriate error message or perform any necessary actions
                Console.WriteLine("Invalid CollectionID. The specified collection does not exist.");
            }
            else
            {
                // Handle other types of SQL exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        con.Close();

        return affectedRows >= 1;
    }

    public bool DeleteBook(int id) 
    {
        int i = 0;
        connection();
        try
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Books WHERE BookId = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.Text;

            con.Open();
            i = cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Haha sucks to suck {e.Message}");
        }
        finally { con.Close(); }
        return i >= 1;
    }

    public int GetLastInsertedBookId()
    {
        connection();
        SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('dbo.Books')", con);
        cmd.CommandType = CommandType.Text;
        con.Open();

        int lastInsertedId = Convert.ToInt32(cmd.ExecuteScalar());

        con.Close();

        return lastInsertedId;
    }

}