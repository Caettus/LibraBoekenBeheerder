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
        private readonly GenreMapper _genreMapper = new GenreMapper();
        private readonly BooksMapper _booksMapper = new BooksMapper();
    
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
        
        public List<Books> ReturnBooksInGenre(int id)
        {
            try
            {
                List<BooksDTO> returnBooksDtoList = _genre.GetBooksInGenre(id);
                List<Books> returnBooksList = 
                    returnBooksDtoList.Select(_booksMapper.toClass).ToList();

                return returnBooksList;
            }
            catch (Exception e )
            {
                Console.WriteLine($"Could not return books in CollectionClass.cs: {e.Message}");
            }
            return new List<Books>();
        }
        
        public Genre ReturnAGenre(int id)
        {
            try
            {
                GenreDTO dto = _genre.GetAGenre(id);
        
                if (dto != null)
                {
                    Genre genreClass = _genreMapper.toClass(dto);
                    return genreClass;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Genre kon niet worden opgehaald :) : {e.Message}");
            }
        
            return null;
        }
    }
}
