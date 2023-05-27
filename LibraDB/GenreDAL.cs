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
        
        public bool DeleteGenre(int id) 
        {
            int i = 0;
            connection();
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Genre WHERE GenreId = @GenreId", con);
                cmd.Parameters.AddWithValue("@GenreId", id);
                cmd.CommandType = CommandType.Text;

                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Haha sucks to suck bitch {e.Message}");
            }
            finally { con.Close(); }
            return i >= 1;
        }
        
        public bool LinkGenreToBook(int GenreId, int BookId, BookGenresDTO bookGenresDto)
        {
            connection();
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (@BookId, @GenreId);", con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@BookId", bookGenresDto.BookId);
            cmd.Parameters.AddWithValue("@GenreId", bookGenresDto.GenreId);

            con.Open();
            var i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        public List<GenreDTO> GetGenresFromBook(int id)
        {
            List<GenreDTO> genreList = new List<GenreDTO>();
            connection();
            SqlCommand cmd = new SqlCommand(
                "SELECT g.GenreId, g.GenreName " +
                "FROM Genre g " +
                "LEFT JOIN BookGenres bg ON g.GenreId = bg.GenreId AND bg.BookId = @BookId " +
                "WHERE bg.BookId = @BookId", con);
            cmd.Parameters.AddWithValue("@BookId", id);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var genre = new GenreDTO();

                genre.GenreId = Convert.ToInt32(rdr["GenreId"]);
                genre.GenreName = rdr["GenreName"].ToString();
                genreList.Add(genre);

            }
            con.Close();
            return genreList;
        }
    }
}
