using LibraDTO;
using LibraFactory;
using LibraInterface;
using LibraLogic.Mappers;
using Microsoft.Extensions.Configuration;

namespace LibraLogic;

public class BooksCollection
{
    
    #region Constructors
    private readonly IConfiguration _configuration;
    BooksMapper _booksMapper = new BooksMapper();
    private readonly ICollection _collection;
    private readonly IGenre _genre;
    private readonly IBooksCollection _booksCollection;
    
    
    
    public BooksCollection(IConfiguration configuration)
    {
        _configuration = configuration;
        _collection = DALFactory.GetCollectionDAL(configuration);
        _genre = DALFactory.GetGenreDAL(configuration);
        _booksCollection = DALFactory.GetBooksDALForCollection(configuration);
    }
    
    public BooksCollection(IBooksCollection booksCollection, ICollection collection, IGenre genre)
    {
        _booksCollection = booksCollection;
        _collection = collection;
        _genre = genre;
    }
    #endregion
    
    public bool CreateBook(Books booksClass, int selectedCollectionId, int selectedGenreId)
    {
        try
        {
            var toDto = _booksMapper.toDTO(booksClass);
            
            if (booksClass.Title != " ")
            {
                if (_booksCollection.CreateBook(toDto))
                {
                    int bookId = _booksCollection.GetLastInsertedBookId();

                    //boek aan collectie toevoegen
                    CollectionBooksDTO collectionBooksDTO = new CollectionBooksDTO();
                    collectionBooksDTO.CollectionID = selectedCollectionId;
                    collectionBooksDTO.BookId = bookId;
                
                    if (_collection.LinkBookToCollection(selectedCollectionId, bookId, collectionBooksDTO))
                    {
                        //genre aan boek toevoegen
                        BookGenresDTO bookGenresDTO = new BookGenresDTO();
                        bookGenresDTO.GenreId = selectedGenreId;
                        bookGenresDTO.BookId = bookId;
                        if (_genre.LinkGenreToBook(selectedGenreId, selectedCollectionId, bookGenresDTO))
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("LinkGenreToBook check kon niet worden gepassed in de logic");
                        }
                    }
                    //iedereen blij
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oopsie woopsie! Something went wrong: {ex.Message}");
            return false;
        }

        return false;
    }
    
    
    public List<Books> ReturnAllBooks() 
    {
        try
        {

            List<BooksDTO> returnBooksDtoList = _booksCollection.GetAllBooks();

            List<Books> returnBooksList = returnBooksDtoList.Select(_booksMapper.toClass).ToList();

            return returnBooksList;
        }
        catch (Exception e)
        {
            Console.WriteLine($"ReturnAllBooks werkt niet: {e.Message}");
        }
        return new List<Books> { };
    }
    
    public int ReturntLastInsertedBookId()
    {
        try
        {
            return _booksCollection.GetLastInsertedBookId();
        }
        catch (Exception e)
        {
            Console.WriteLine($"GetLastInsertedBookId werkt niet: {e.Message}");
        }
        return 0;
    }
}