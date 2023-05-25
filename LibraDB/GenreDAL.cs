using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraInterface;
using LibraDTO;
using System.Data;

namespace LibraDB
{
    public class GenreDAL : IGenre
    {
        private readonly IConfiguration _configuration;

        public GenreDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection con;

        private void connection()
        {
            string connstring = _configuration.GetConnectionString("MyConnectionString");
            con = new SqlConnection(connstring);
        }

        public List<GenreDTO> GetAllGenres() 
        {
            List<GenreDTO> myGenreList = new List<GenreDTO>();

            connection();
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Genre", con);

            command.CommandType = CommandType.Text;
            con.Open();

            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                var genre = new GenreDTO();

                genre.GenreId = Convert.ToInt32(dataReader["GenreId"]);
                genre.GenreName = dataReader["GenreName"].ToString();
                myGenreList.Add(genre);

                if (genre != null)
                {
                    myGenreList.Add(genre);
                }
            }
            con.Close();
            return myGenreList;
        }

        public bool CreateGenre(GenreDTO genreDto)
        {
            connection();
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Genre] ([GenreName]) VALUES (@GenreName);", con);
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@GenreName", genreDto.GenreName);
            con.Open();
            var i = command.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}
