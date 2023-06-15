using LibraDTO;

namespace LibraInterface;

public interface ICollectionService
{
    bool CreateCollection(CollectionDTO collectionsDto);
    List<CollectionDTO> GetAllCollections();
    List<CollectionDTO> GetCollectionsNotContainingBook(int id);
    List<CollectionDTO> GetCollectionsContainingBook(int id);
}