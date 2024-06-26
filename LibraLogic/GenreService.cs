using LibraDTO;
using LibraFactory;
using LibraInterface;
using LibraLogic.Mappers;
using Microsoft.Extensions.Configuration;

namespace LibraLogic;

public class GenreService
{
    private readonly IConfiguration _configuration;
    GenreMapper _genreMapper = new GenreMapper();
    private readonly IGenreService _genreService;
    
    public GenreService(IConfiguration configuration)
    {
        _configuration = configuration;
        _genreService = DALFactory.GetGenreServiceDAL(configuration);
    }
    
    public GenreService(IGenreService genreService)
    {
        _genreService = genreService;
    }
    
    public List<Genre> ReturnAllGenres()
    {
        try
        {
            List<GenreDTO> returnGenreDtoList = _genreService.GetAllGenres();

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
        var genreDTO = _genreMapper.toDTO(genreClass);
        try
        {
            if (genreClass.GenreName != " ")
            {
                if (_genreService.CreateGenre(genreDTO))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"new Genre could not be created: {e.Message}");
        }

        return false;
    }
    
    public List<Genre> ReturnGenresFromBook(int id)
    {
        try
        {
            List<GenreDTO> returnGenreDtoList = _genreService.GetGenresFromBook(id);
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