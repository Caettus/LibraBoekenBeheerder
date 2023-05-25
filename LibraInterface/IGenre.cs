﻿using LibraDTO;
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
    }
}
