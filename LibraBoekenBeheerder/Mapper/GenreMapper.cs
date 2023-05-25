using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraBoekenBeheerder.Models;

namespace LibraLogic.Mappers
{
    public class GenreMapper
    {
        public GenreModel toModel(Genre genresClass)
        {
            return new GenreModel()
            {
                GenreId = genresClass.GenreId,
                GenreName = genresClass.GenreName
            };
        }

        public Genre toClass(GenreModel genreModel)
        {
            return new Genre()
            {
                GenreId = genreModel.GenreId,
                GenreName = genreModel.GenreName
            };
        }
    }
}
