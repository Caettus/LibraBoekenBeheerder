using LibraDTO;
using LibraFactory;
using LibraInterface;
using LibraLogic.Mappers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraLogic
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        private readonly IConfiguration _configuration;
        private readonly IGenre _genre;
    
        public Genre(IConfiguration configuration)
        {
            _configuration = configuration;
            _genre = DALFactory.GetGenreDAL(configuration);
        }
    
        public Genre(IGenre genre)
        {
            _genre = genre;
        }
        
        public Genre()
        {
            
        }

        
        public bool DeleteGenre(int id)
        {
            try
            {
                if (_genre.DeleteGenre(id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Genre could not be deleted in logic: {e.Message}");
            }
            return false;
        }
        
        
    }
}
