using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using LibraInterface;
using Microsoft.Extensions.Configuration;

namespace LibraDB;

public class CollectionDAL : ICollection
{

    #region configuratie
    private readonly IConfiguration _configuration;
    
    public CollectionDAL(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private SqlConnection con;

    private void connection()
    {
        string connstring = _configuration.GetConnectionString("MyConnectionString");
        con = new SqlConnection(connstring);
    }
    #endregion

    public List<CollectionDTO> GetAllCollections()
    {
        List<CollectionDTO> mycollectionsList = new List<CollectionDTO>();
        
        connection();
        SqlCommand command = new SqlCommand("SELECT * FROM dbo.Collection", con);

        command.CommandType = CommandType.Text;
        con.Open();

        SqlDataReader dataReader = command.ExecuteReader();
        while (dataReader.Read())
        {
            var collection = new CollectionDTO();

            collection.CollectionsID = Convert.ToInt32(dataReader["CollectionID"]);
            collection.Name = dataReader["Name"].ToString();
            mycollectionsList.Add(collection);

            if (collection != null)
            {
                mycollectionsList.Add(collection);
            }
        }
        con.Close();
        return mycollectionsList;
    }

    public bool CreateCollection(CollectionDTO collectionsDto)
    {
        connection();
        SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Collection] ([Name]) VALUES (@Name);", con);
        command.CommandType = CommandType.Text;

        command.Parameters.AddWithValue("@Name", collectionsDto.Name);
        con.Open();
        var i = command.ExecuteNonQuery();
        con.Close();

        if (i >= 1)
            return true;
        else
            return false;
    }

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

    public List<CollectionDTO> GetCollectionsNotContainingBook(int id)
    {
        List<CollectionDTO> collectionlist = new List<CollectionDTO>();
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
            var collection = new CollectionDTO();

            collection.CollectionsID = Convert.ToInt32(rdr["CollectionID"]);
            collection.Name = rdr["Name"].ToString();
            collectionlist.Add(collection);

        }
        con.Close();
        return collectionlist;
    }

    public List<CollectionDTO> GetCollectionsContainingBook(int id)
    {
        List<CollectionDTO> collectionlist = new List<CollectionDTO>();
        connection();
        SqlCommand cmd = new SqlCommand(
                   "SELECT c.CollectionID, c.Name " +
                   "FROM Collection c " +
                   "LEFT JOIN CollectionBooks cb ON c.CollectionID = cb.CollectionID AND cb.BookId = @bookId " +
                   "WHERE cb.BookId = @bookId", con);
        cmd.Parameters.AddWithValue("@bookId", id);
        cmd.CommandType = CommandType.Text;
        con.Open();
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            var collection = new CollectionDTO();

            collection.CollectionsID = Convert.ToInt32(rdr["CollectionID"]);
            collection.Name = rdr["Name"].ToString();
            collectionlist.Add(collection);

        }
        con.Close();
        return collectionlist;
    }
}