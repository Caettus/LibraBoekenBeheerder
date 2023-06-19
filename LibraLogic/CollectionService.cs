using LibraDTO;
using LibraFactory;
using LibraInterface;
using LibraLogic.Mappers;
using Microsoft.Extensions.Configuration;

namespace LibraLogic;

public class CollectionService
{
    CollectionMapper _collectionMapper = new CollectionMapper();
    
    private readonly ICollectionService _collectionService;
    private readonly IConfiguration _configuration;

    public CollectionService(IConfiguration configuration)
    {
        _configuration = configuration;
        _collectionService = DALFactory.GetCollectionServiceDAL(configuration);
    }
    
    public CollectionService(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }
    
    public bool CreateCollection(CollectionClass collectionClass)
    {
        var collectionDTO = _collectionMapper.toDTO(collectionClass);
        try
        {
            if (collectionClass.Name != " ")
            {
                if (_collectionService.CreateCollection(collectionDTO))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"CollectionClass could not be created: {e.Message}");
        }

        return false;
    }
    
    public List<CollectionClass> ReturnAllCollections()
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
    
    public List<CollectionClass> ReturnCollectionsContaintingBook(int id)
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
    
    public List<CollectionClass> ReturnCollectionsNotContaintingBook(int id)
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