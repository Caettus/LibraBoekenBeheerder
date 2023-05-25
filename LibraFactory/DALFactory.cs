using LibraDB;
using LibraDBWW;
using LibraDTO;
using LibraInterface;
using Microsoft.Extensions.Configuration;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraFactory
{

    public static class DALFactory
    {
        public static IBooks GetBooksDAL(IConfiguration configuration)
        {
            return new BooksDAL(configuration);
        }
        
        public static ICollection GetCollectionDAL(IConfiguration configuration)
        {
            return new CollectionDAL(configuration);
        }

        public static IGenre GetGenreDAL(IConfiguration configuration) 
        {
            return new GenreDAL(configuration);
        }
    }
}