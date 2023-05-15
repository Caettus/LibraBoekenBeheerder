using LibraDB;
using LibraInterface;
using LibraFactory;
using Microsoft.Extensions.Configuration;
using System.Net;
using LibraLogic.Mappers;

namespace LibraLogic;

public class Books
{
    public int BookId { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string ISBNNumber { get; set; }

    public int? Pages { get; set; }

    public int? PagesRead { get; set; }

    public string? Summary { get; set; }


    BooksMapper _booksMapper = new BooksMapper();

    public bool CreateBook(Books booksClass, int selectedCollectionId, IConfiguration configuration)
    {
        try
        {
            IBooks Book = DALFactory.GetBooksDAL(configuration);
            var dto = _booksMapper.toDTO(booksClass);


            if (Book.CreateBook(dto))
            {
                int bookId = Book.GetLastInsertedBookId();

                ICollection collectionBooksLink = DALFactory.GetCollectionDAL(configuration);
                CollectionBooksDTO collectionBooksDTO = new CollectionBooksDTO();
                collectionBooksDTO.CollectionID = selectedCollectionId;
                collectionBooksDTO.BookId = bookId;

                if (collectionBooksLink.LinkBookToCollection(selectedCollectionId, bookId, collectionBooksDTO))
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oopsie woopsie! Something went wrong: {ex.Message}");
            return false;
        }

        return false;
    }

    public bool EditBook(Books booksClass, int selectedCollectionId, int bookId, IConfiguration configuration)
    {
        try
        {
            IBooks editBook = DALFactory.GetBooksDAL(configuration);

            ICollection collectionBooksLink = DALFactory.GetCollectionDAL(configuration);
            CollectionBooksDTO collectionBooksDTO = new CollectionBooksDTO();
            collectionBooksDTO.CollectionID = selectedCollectionId;
            collectionBooksDTO.BookId = bookId;

            var dto = _booksMapper.toDTO(booksClass);

            if (editBook.EditBook(dto) && collectionBooksLink.LinkBookToCollection(selectedCollectionId, bookId, collectionBooksDTO))
            {
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Shits fucked mate: {e.Message}");
            return false;
        }
        return false;
    }

    public bool GetABook(int id, IConfiguration configuration)
    {
        try
        {
            IBooks getABook = DALFactory.GetBooksDAL(configuration);
            if (getABook.GetABook(id))
            {
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Boek kon niet worden opgehaald :) : {e.Message}");
            return false;
        }
    }
}