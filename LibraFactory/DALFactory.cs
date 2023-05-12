using LibraDB;
using LibraInterface;
using Microsoft.Extensions.Configuration;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraFactory
{

    public static class DALFactory
    {
        public static IBooks GetCreateBook(IConfiguration configuration)
        {
            return new BooksDAL(configuration);
        }
        public static IBooks GetLastInsertedBookId(IConfiguration configuration)
        {
            return new BooksDAL(configuration);
        }
        public static IBooks GetAllBooks(IConfiguration configuration)
        {
            return new BooksDAL(configuration);
        }
        public static IBooks GetABook(IConfiguration configuration)
        {
            return new BooksDAL(configuration);
        }
        
        public static ICollectionBooks GetLinkBookToCollection(IConfiguration configuration)
        {
            return new CollectionBooksDAL(configuration);
        }
    }
}