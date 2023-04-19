using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LibraDB;

public class CollectionBooksDAL
{
    #region configuratie en shit
    private readonly IConfiguration _configuration;

    public CollectionBooksDAL(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public CollectionBooksDAL()
    {

    }
    private SqlConnection con;
        
    private void connection()
    {
        string connstring = _configuration.GetConnectionString("MyConnectionString");
        con = new SqlConnection(connstring);
    }
    #endregion


    public bool LinkBookToCollection(int CollectionID, int BookID, CollectionBooksDTO collectionBooksDTO)
    {
        connection();
        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[CollectionBooks] ([CollectionID], [BookId]) VALUES (@CollectionID, @BookId);", con);
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@CollectionID", collectionBooksDTO.CollectionID);
        cmd.Parameters.AddWithValue("@BookId", collectionBooksDTO.BookId);

        con.Open();
        var i = cmd.ExecuteNonQuery();
        con.Close();

        if (i >= 1)
            return true;
        else
            return false;
    }
}