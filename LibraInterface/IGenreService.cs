using LibraDTO;

namespace LibraInterface;

public interface IGenreService
{
    List<GenreDTO> GetAllGenres();
    bool CreateGenre(GenreDTO genreDto);
    List<GenreDTO> GetGenresFromBook(int id);
}