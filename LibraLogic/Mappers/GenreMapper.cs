using LibraDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraLogic.Mappers
{
    public class GenreMapper
    {
        public GenreDTO toDTO(Genre genresClass)
        {
            return new GenreDTO()
            {
                GenreId = genresClass.GenreId,
                GenreName = genresClass.GenreName
            };
        }
    
        public Genre toClass(GenreDTO genreDTO) 
        {
            return new Genre()
            {
                GenreId = genreDTO.GenreId,
                GenreName = genreDTO.GenreName
            };
        }
    }
}
