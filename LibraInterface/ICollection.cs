﻿using LibraDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraInterface
{
    public interface ICollection
    {
        bool LinkBookToCollection(int CollectionID, int BookID, CollectionBooksDTO collectionBooksDTO);
       
        CollectionDTO GetACollection(int id);

        List<BooksDTO> GetBooksInCollection(int id);
        bool RemoveLinkBookFromCollection(int CollectionID, int BookId);
        bool DeleteCollection(int id);
    }
}
