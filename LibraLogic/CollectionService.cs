using LibraDTO;
using LibraFactory;
using LibraInterface;
using LibraLogic.Mappers;
using Microsoft.Extensions.Configuration;

namespace LibraLogic;

public class CollectionService
{
    CollectionMapper _collectionMapper = new CollectionMapper();
    
    private readonly IConfiguration _configuration;
    private readonly IBooks _book;
    private readonly ICollection _collection;
    private readonly IGenre _genre;
    private readonly ICollectionService _collectionService;

    public CollectionService(IConfiguration configuration)
    {
        _configuration = configuration;
        _book = DALFactory.GetBooksDAL(configuration);
        _collection = DALFactory.GetCollectionDAL(configuration);
        _genre = DALFactory.GetGenreDAL(configuration);
    }
    
    public CollectionService(IBooks books, ICollection collection, IGenre genre)
    {
        _book = books;
        _collection = collection;
        _genre = genre;
    }
    
    public bool CreateCollection(CollectionClass collectionClass)
    {
        var collectionDTO = _collectionMapper.toDTO(collectionClass);
        try
        {
            if (_collectionService.CreateCollection(collectionDTO))
            {
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"CollectionClass could not be created: {e.Message}");
        }

        return false;
    }
    
    public List<CollectionClass> ReturnAllCollections(IConfiguration configuration)
    {
        try
        {

            List<CollectionDTO> returnCollectionDtoList = _collectionService.GetAllCollections();

            List<CollectionClass> returnCollectionList =
                returnCollectionDtoList.Select(_collectionMapper.toClass).ToList();

            return returnCollectionList;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not return all collections: {e.Message}");
        }

        return new List<CollectionClass>();
    }
    
    public List<CollectionClass> ReturnCollectionsContaintingBook(int id, IConfiguration configuration)
    {
        try
        {

            List<CollectionDTO> returnCollectionDtoList = _collectionService.GetCollectionsContainingBook(id);

            List<CollectionClass> returnCollectionList =
                returnCollectionDtoList.Select(_collectionMapper.toClass).ToList();

            return returnCollectionList;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not return collections: {e.Message}");
        }

        return new List<CollectionClass>();
    }
    
    public List<CollectionClass> ReturnCollectionsNotContaintingBook(int id, IConfiguration configuration)
    {
        try
        {

            List<CollectionDTO> returnCollectionDtoList = _collectionService.GetCollectionsNotContainingBook(id);

            List<CollectionClass> returnCollectionList =
                returnCollectionDtoList.Select(_collectionMapper.toClass).ToList();

            return returnCollectionList;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not return collections: {e.Message}");
        }

        return new List<CollectionClass>();
    }
}