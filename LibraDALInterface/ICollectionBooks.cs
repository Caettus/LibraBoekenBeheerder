using LibraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Interface
{
    internal interface ICollectionBooks
    {
        bool LinkBookToCollection(int CollectionID, int BookID, CollectionBooksDTO collectionBooksDTO);
    }
}
