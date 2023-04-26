using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using Microsoft.Extensions.Configuration;

namespace LibraDB;

public class BooksDAL
{
    private readonly IConfiguration _configuration;

    public BooksDAL(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public BooksDAL()
    {

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
        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Books] ([Title], [Author], [ISBNNumber], [Summary], [Pages], [PagesRead], [CollectionID]) VALUES (@Title, @Author, @ISBNNumber, @Summary, @Pages, @PagesRead, @CollectionID);", con);
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@Title", booksDTO.Title);
        cmd.Parameters.AddWithValue("@Author", booksDTO.Author);
        cmd.Parameters.AddWithValue("@ISBNNumber", booksDTO.ISBNNumber);
        cmd.Parameters.AddWithValue("@Summary", booksDTO.Summary);
        cmd.Parameters.AddWithValue("@Pages", booksDTO.Pages);
        cmd.Parameters.AddWithValue("@PagesRead", booksDTO.PagesRead);
        cmd.Parameters.AddWithValue("@CollectionID", booksDTO.CollectionID);

        con.Open();
        var i = cmd.ExecuteNonQuery();
        con.Close();

        if (i >= 1)
            return true;
        else
            return false;
    }

    public List<CollectionsDTO> GetCollectionsNotContainingBook(int id)
    {
        List<CollectionsDTO> collectionlist = new List<CollectionsDTO>();
        connection();
        SqlCommand cmd = new SqlCommand(
                   "SELECT c.CollectionID, c.Name " +
                   "FROM Collection c " +
                   "LEFT JOIN CollectionBooks cb ON c.CollectionID = cb.CollectionID AND cb.BookId = @bookId " +
                   "WHERE cb.BookId IS NULL", con);
        cmd.Parameters.AddWithValue("@bookId", id);
        cmd.CommandType = CommandType.Text;
        con.Open();
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            var collection = new CollectionsDTO();

            collection.CollectionsID = Convert.ToInt32(rdr["CollectionsID"]);
            collection.Name = rdr["Name"].ToString();
            collectionlist.Add(collection);

        }
        con.Close();
        return collectionlist;
    }
}