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
    
        private readonly IConfiguration _configuration;
        BooksMapper _booksMapper = new BooksMapper();
        private readonly IBooks _book;
        private readonly ICollection _collection;
        private readonly IGenre _genre;   
        
        public CollectionClass(IBooks books, ICollection collection, IGenre genre)
        {
            _book = books;
            _collection = collection;
            _genre = genre;
        }
        
        public CollectionClass(IConfiguration configuration)
        {
            _configuration = configuration;
            _book = DALFactory.GetBooksDAL(configuration);
            _collection = DALFactory.GetCollectionDAL(configuration);
            _genre = DALFactory.GetGenreDAL(configuration);
        }
        
        public CollectionClass()
        {
            
        }

        public CollectionClass ReturnACollection(IConfiguration configuration, int id)
        {
            try
            {
                CollectionDTO dto = _collection.GetACollection(id);
        
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

        public List<Books> ReturnBooksInCollection(int id, IConfiguration configuration)
        {
            try
            {
                List<BooksDTO> returnBooksDtoList = _collection.GetBooksInCollection(id);
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

        public bool RemoveLinkBookToCollection(int CollectionID, int BookId, IConfiguration configuration)
        {
            try
            {
                if (_collection.RemoveLinkBookFromCollection(CollectionID, BookId))
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
                if (_collection.DeleteCollection(id))
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
