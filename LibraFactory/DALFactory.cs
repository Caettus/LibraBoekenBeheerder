using LibraDB;
using LibraInterface;
using Microsoft.Extensions.Configuration;

namespace LibraFactory
{
    public static class DALFactory
    {
        public static IBooks GetCreateBook()
        {
            return new BooksDAL();
        }
        public static IBooks GetLastInsertedBookId()
        {
            return new BooksDAL();
        }
        public static IBooks GetAllBooks()
        {
            return new BooksDAL();
        }
        public static IBooks GetABook()
        {
            return new BooksDAL();
        }
        
        public static ICollectionBooks GetLinkBookToCollection()
        {
            return new CollectionBooksDAL();
        }
    }
}