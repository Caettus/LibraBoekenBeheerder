using LibraDB;
using LibraInterface;
using LibraFactory;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace LibraLogic;

public class Books
{
    public int BookId { get; private set; }

    public string Title { get; private set; }

    public string Author { get; private set; }

    public string ISBNNumber { get; private set; }

    public int? Pages { get; private set; }

    public int? PagesRead { get; private set; }

    public string? Summary { get; private set; }



    public bool CreateBook(BooksDTO booksDto, int selectedCollectionId, IConfiguration configuration)
    {
        try
        {
            IBooks createBook = DALFactory.GetBooksDAL(configuration);
            if (createBook.CreateBook(booksDto))
            {
                IBooks lastInsertedBookId = DALFactory.GetBooksDAL(configuration);
                int bookId = lastInsertedBookId.GetLastInsertedBookId();

                ICollectionBooks collectionBooksLink = DALFactory.GetLinkBookToCollection(configuration);
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

    public bool EditBook(BooksDTO booksDTO, int selectedCollectionId, int bookId, IConfiguration configuration)
    {
        try
        {
            IBooks editBook = DALFactory.GetBooksDAL(configuration);

            ICollectionBooks collectionBooksLink = DALFactory.GetLinkBookToCollection(configuration);
            CollectionBooksDTO collectionBooksDTO = new CollectionBooksDTO();
            collectionBooksDTO.CollectionID = selectedCollectionId;
            collectionBooksDTO.BookId = bookId;

            if (editBook.EditBook(booksDTO) && collectionBooksLink.LinkBookToCollection(selectedCollectionId, bookId, collectionBooksDTO))
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