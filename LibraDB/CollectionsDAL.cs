using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using Microsoft.Extensions.Configuration;

namespace LibraDB;

public class CollectionsDAL
{
    private readonly IConfiguration _configuration;
    
    public CollectionsDAL(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private SqlConnection con;

    private void connection()
    {
        string connstring = _configuration.GetConnectionString("MyConnectionString");
        con = new SqlConnection(connstring);
    }

    public List<CollectionsDTO> GetAllCollections()
    {
        List<CollectionsDTO> mycollectionsList = new List<CollectionsDTO>();
        
        connection();
        SqlCommand command = new SqlCommand("SELECT * FROM dbo.Collection", con);

        command.CommandType = CommandType.Text;
        con.Open();

        SqlDataReader dataReader = command.ExecuteReader();
        while (dataReader.Read())
        {
            var collection = new CollectionsDTO();

            collection.CollectionsID = Convert.ToInt32(dataReader["CollectionID"]);
            collection.Name = dataReader["Name"].ToString();
            
            if (collection != null)
            {
                mycollectionsList.Add(collection);
            }
        }
        con.Close();
        return (mycollectionsList);
    }

    public bool CreateCollection(CollectionsDTO collectionsDto)
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
}