using LibraDTO;
using LibraInterface;

namespace LibraDB;

public class TestGenreDAL : IGenre
{
    public bool CreateGenre(GenreDTO genreDto)
    {
        return true;
    }
    public bool DeleteGenre(int id)
    {
        return true;
    }
    public List<GenreDTO> GetAllGenres()
    {
        GenreDTO genreDto = new GenreDTO();
        {
            genreDto.GenreId = 1;
            genreDto.GenreName = "TestGenre";
        }
        GenreDTO genreDto2 = new GenreDTO();
        {
            genreDto2.GenreId = 2;
            genreDto2.GenreName = "TestGenre2";
        }
        List<GenreDTO> genres = new List<GenreDTO>();
        genres.Add(genreDto);
        genres.Add(genreDto2);
        return genres;
    }
    public List<GenreDTO> GetGenresFromBook(int id)
    {
        GenreDTO genreDto = new GenreDTO();
        {
            genreDto.GenreId = 1;
            genreDto.GenreName = "TestGenre";
        }
        GenreDTO genreDto2 = new GenreDTO();
        {
            genreDto2.GenreId = 2;
            genreDto2.GenreName = "TestGenre2";
        }
        List<GenreDTO> genres = new List<GenreDTO>();
        genres.Add(genreDto);
        genres.Add(genreDto2);
        return genres;
    }
    public bool LinkGenreToBook(int GenreId, int BookId, BookGenresDTO bookGenresDto)
    {
        return true;
    }
}