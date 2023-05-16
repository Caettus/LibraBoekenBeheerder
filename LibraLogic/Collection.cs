using LibraDTO;
using LibraFactory;
using LibraInterface;
using LibraLogic.Mappers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraLogic
{
    public class Collection

    {
        public int CollectionsID { get; set; }
        public string Name { get; set; }

        CollectionMapper _collectionMapper = new CollectionMapper();

        public List<Collection> ReturnAllCollections(IConfiguration configuration)
        {
            try
            {
                ICollection getAllCollections = DALFactory.GetCollectionDAL(configuration);

                List<CollectionDTO> returnCollectionDtoList = getAllCollections.GetAllCollections();

                List<Collection> returnCollectionList =
                    returnCollectionDtoList.Select(_collectionMapper.toClass).ToList();

                return returnCollectionList;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not return all collections: {e.Message}");
            }

            return new List<Collection>();
        }

        public Collection ReturnACollection(IConfiguration configuration, int id)
        {
            try
            {
                ICollection getACollection = DALFactory.GetCollectionDAL(configuration);
                CollectionDTO dto = getACollection.GetACollection(id);
        
                if (dto != null)
                {
                    Collection collection = _collectionMapper.toClass(dto);
                    return collection;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Boek kon niet worden opgehaald :) : {e.Message}");
            }
        
            return null;
        }


        public List<Collection> ReturnCollectionsNotContaintingBook(int id, IConfiguration configuration)
        {
            try
            {
                ICollection getCollections = DALFactory.GetCollectionDAL(configuration);

                List<CollectionDTO> returnCollectionDtoList = getCollections.GetCollectionsNotContainingBook(id);

                List<Collection> returnCollectionList =
                    returnCollectionDtoList.Select(_collectionMapper.toClass).ToList();

                return returnCollectionList;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not return collections: {e.Message}");
            }

            return new List<Collection>();
        }

        public List<Collection> ReturnCollectionsContaintingBook(int id, IConfiguration configuration)
        {
            try
            {
                ICollection getCollections = DALFactory.GetCollectionDAL(configuration);

                List<CollectionDTO> returnCollectionDtoList = getCollections.GetCollectionsContainingBook(id);

                List<Collection> returnCollectionList =
                    returnCollectionDtoList.Select(_collectionMapper.toClass).ToList();

                return returnCollectionList;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not return collections: {e.Message}");
            }

            return new List<Collection>();
        }

        public bool CreateCollection(Collection collection, IConfiguration configuration)
        {
            ICollection createCollection = DALFactory.GetCollectionDAL(configuration);
            var collectionDTO = _collectionMapper.toDTO(collection);
            try
            {
                if (createCollection.CreateCollection(collectionDTO))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Collection could not be created: {e.Message}");
            }

            return false;
        }
    }
}
