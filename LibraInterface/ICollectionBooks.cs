using LibraDB;

namespace LibraInterface;

public interface ICollectionBooks
{
    bool LinkBookToCollection(int CollectionID, int BookID, CollectionBooksDTO collectionBooksDTO);
}