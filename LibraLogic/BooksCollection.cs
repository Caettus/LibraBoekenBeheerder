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
    private readonly IBooks _book;
    private readonly ICollection _collection;
    private readonly IGenre _genre;
    
    
    
    public BooksCollection(IConfiguration configuration)
    {
        _configuration = configuration;
        _book = DALFactory.GetBooksDAL(configuration);
        _collection = DALFactory.GetCollectionDAL(configuration);
        _genre = DALFactory.GetGenreDAL(configuration);
    }
    
    public BooksCollection(IBooks books, ICollection collection, IGenre genre)
    {
        _book = books;
        _collection = collection;
        _genre = genre;
    }
    #endregion
    
    public bool CreateBook(Books booksClass, int selectedCollectionId, int selectedGenreId)
    {
        try
        {
            var toDto = _booksMapper.toDTO(booksClass);


            if (_book.CreateBook(toDto))
            {
                int bookId = _book.GetLastInsertedBookId();

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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oopsie woopsie! Something went wrong: {ex.Message}");
            return false;
        }

        return false;
    }
    
    
    public List<Books> ReturnAllBooks(IConfiguration configuration) 
    {
        try
        {

            List<BooksDTO> returnBooksDtoList = _book.GetAllBooks();

            List<Books> returnBooksList = returnBooksDtoList.Select(_booksMapper.toClass).ToList();

            return returnBooksList;
        }
        catch (Exception e)
        {
            Console.WriteLine($"ReturnAllBooks werkt niet: {e.Message}");
        }
        return new List<Books> { };
    }
}