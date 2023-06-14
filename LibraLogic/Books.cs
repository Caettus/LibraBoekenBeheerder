using LibraDTO;
using LibraInterface;
using LibraFactory;
using Microsoft.Extensions.Configuration;
using System.Net;
using LibraLogic.Mappers;

namespace LibraLogic;

public class Books
{
    #region Properties
    public int BookId { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string ISBNNumber { get; set; }

    public int? Pages { get; set; }

    public int? PagesRead { get; set; }

    public string? Summary { get; set; }
    #endregion

    #region Constructors
    BooksMapper _booksMapper = new BooksMapper();
    private readonly IBooks _books;
    private readonly ICollection _collection;
    private readonly IGenre _genre;
    private readonly IConfiguration _configuration;

    
    public Books(IConfiguration configuration)
    {
        _configuration = configuration;
        _books = DALFactory.GetBooksDAL(configuration);
        _collection = DALFactory.GetCollectionDAL(configuration);
        _genre = DALFactory.GetGenreDAL(configuration);
    }

    public Books(IBooks books, ICollection collection, IGenre genre)
    {
        _books = books;
        _collection = collection;
        _genre = genre;
    }
    public Books()
    {
        
    }
    #endregion
    
    public bool DeleteBook(int id)
    {
        try
        {
            if (_books.DeleteBook(id))
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
            Console.WriteLine($"Book could not be deleted in logic: {e.Message}");
        }
        return false;
    }

    public bool EditBook(Books booksClass, int? selectedCollectionId, int bookId)
    {
        try
        {
            var dto = _booksMapper.toDTO(booksClass);

            if (_books.EditBook(dto, bookId))
            {
                if (selectedCollectionId.HasValue)
                {
                    CollectionBooksDTO collectionBooksDTO = new CollectionBooksDTO();
                    collectionBooksDTO.CollectionID = selectedCollectionId.Value;
                    collectionBooksDTO.BookId = bookId;

                    return _collection.LinkBookToCollection(selectedCollectionId.Value, bookId, collectionBooksDTO);
                }
                else
                {
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
        return false;
    }
    
    public Books GetABook(int id)
    {
        try
        {
            BooksDTO dto = _books.GetABook(id);

            if (dto != null)
            {
                Books book = _booksMapper.toClass(dto);
                return book;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Boek kon niet worden opgehaald :) : {e.Message}");
        }
        return null;
    }

    
}