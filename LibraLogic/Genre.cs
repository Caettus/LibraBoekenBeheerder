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

        GenreMapper _genreMapper = new GenreMapper();

        public List<Genre> ReturnAllGenres(IConfiguration configuration)
        {
            try
            {
                IGenre getAllGenres = DALFactory.GetGenreDAL(configuration);

                List<GenreDTO> returnGenreDtoList = getAllGenres.GetAllGenres();

                List<Genre> returnGenresList = returnGenreDtoList.Select(_genreMapper.toClass).ToList();

                return returnGenresList;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ReturnAllGenres werkt niet: {e.Message}");
            }
            return new List<Genre> { };
        }

        public bool CreateGenre(Genre genreClass, IConfiguration configuration)
        {
            IGenre createGenre = DALFactory.GetGenreDAL(configuration);
            var genreDTO = _genreMapper.toDTO(genreClass);
            try
            {
                if (createGenre.CreateGenre(genreDTO))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"new Genre could not be created: {e.Message}");
            }

            return false;
        }
        public bool DeleteGenre(IConfiguration configuration, int id)
        {
            try
            {
                IGenre Genre = DALFactory.GetGenreDAL(configuration);
                if (Genre.DeleteGenre(id))
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
        
        public List<Genre> ReturnGenresFromBook(int id, IConfiguration configuration)
        {
            try
            {
                IGenre getGenresFromBook = DALFactory.GetGenreDAL(configuration);
                List<GenreDTO> returnGenreDtoList = getGenresFromBook.GetGenresFromBook(id);
                List<Genre> returnGenreList = 
                    returnGenreDtoList.Select(_genreMapper.toClass).ToList();

                return returnGenreList;
            }
            catch (Exception e )
            {
                Console.WriteLine($"Could not return books in CollectionClass.cs: {e.Message}");
            }
            return new List<Genre>();
        }
    }
}
