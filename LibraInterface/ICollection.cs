using LibraDB;
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
        List<CollectionDTO> GetAllCollections();
        bool CreateCollection(CollectionDTO collectionsDto);
        List<CollectionDTO> GetCollectionsNotContainingBook(int id);
        List<CollectionDTO> GetCollectionsContainingBook(int id);
    }
}
