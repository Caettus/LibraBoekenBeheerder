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
    public class CollectionClass

    {
        public int CollectionsID { get; set; }
        public string Name { get; set; }

        CollectionMapper _collectionMapper = new CollectionMapper();
        BooksMapper _booksMapper = new BooksMapper();   

        public List<CollectionClass> ReturnAllCollections(IConfiguration configuration)
        {
            try
            {
                ICollection getAllCollections = DALFactory.GetCollectionDAL(configuration);

                List<CollectionDTO> returnCollectionDtoList = getAllCollections.GetAllCollections();

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


        public CollectionClass ReturnACollection(IConfiguration configuration, int id)
        {
            try
            {
                ICollection getACollection = DALFactory.GetCollectionDAL(configuration);
                CollectionDTO dto = getACollection.GetACollection(id);
        
                if (dto != null)
                {
                    CollectionClass collectionClass = _collectionMapper.toClass(dto);
                    return collectionClass;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Boek kon niet worden opgehaald :) : {e.Message}");
            }
        
            return null;
        }


        public List<CollectionClass> ReturnCollectionsNotContaintingBook(int id, IConfiguration configuration)
        {
            try
            {
                ICollection getCollections = DALFactory.GetCollectionDAL(configuration);

                List<CollectionDTO> returnCollectionDtoList = getCollections.GetCollectionsNotContainingBook(id);

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

        public List<Books> ReturnBooksInCollection(int id, IConfiguration configuration)
        {
            try
            {
                ICollection getBooksinCollection = DALFactory.GetCollectionDAL(configuration);
                List<BooksDTO> returnBooksDtoList = getBooksinCollection.GetBooksInCollection(id);
                List<Books> returnBooksList = 
                    returnBooksDtoList.Select(_booksMapper.toClass).ToList();

                return returnBooksList;
            }
            catch (Exception e )
            {
                Console.WriteLine($"Could not return books in CollectionClass.cs: {e.Message}");
            }
            return new List<Books>();
        }

        public List<CollectionClass> ReturnCollectionsContaintingBook(int id, IConfiguration configuration)
        {
            try
            {
                ICollection getCollections = DALFactory.GetCollectionDAL(configuration);

                List<CollectionDTO> returnCollectionDtoList = getCollections.GetCollectionsContainingBook(id);

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

        public bool CreateCollection(CollectionClass collectionClass, IConfiguration configuration)
        {
            ICollection createCollection = DALFactory.GetCollectionDAL(configuration);
            var collectionDTO = _collectionMapper.toDTO(collectionClass);
            try
            {
                if (createCollection.CreateCollection(collectionDTO))
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

        public bool RemoveLinkBookToCollection(int CollectionID, int BookId, IConfiguration configuration)
        {
            ICollection collectionDAL = DALFactory.GetCollectionDAL(configuration);
            try
            {
                if (collectionDAL.RemoveLinkBookFromCollection(CollectionID, BookId))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Book could not be removed from Collection: {e.Message}");
            }
            return false;
        }
        
        public bool DeleteCollection(IConfiguration configuration, int id)
        {
            try
            {
                ICollection Collection = DALFactory.GetCollectionDAL(configuration);
                if (Collection.DeleteCollection(id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Collection could not be deleted in logic: {e.Message}");
            }
            return false;
        }
    }
}
