using LibraDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraInterface
{
    public interface IGenre
    {
        List<GenreDTO> GetAllGenres();
        bool CreateGenre(GenreDTO genreDto);
        bool DeleteGenre(int id);
        bool LinkGenreToBook(int GenreId, int BookId, BookGenresDTO bookGenresDto);
        List<GenreDTO> GetGenresFromBook(int id);
    }
}
