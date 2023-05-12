using LibraDB;
using LibraInterface;
using LibraFactory;
using Microsoft.Extensions.Configuration;

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
            IBooks createBook = DALFactory.GetCreateBook(configuration);
            if (createBook.CreateBook(booksDto))
            {
                IBooks lastInsertedBookId = DALFactory.GetLastInsertedBookId(configuration);
                int bookId = lastInsertedBookId.GetLastInsertedBookId();


                ICollectionBooks collectionBooksLink = DALFactory.GetLinkBookToCollection(configuration);
                CollectionBooksDTO collectionBooksDTO = new CollectionBooksDTO();
                if (collectionBooksLink.LinkBookToCollection(selectedCollectionId, bookId, collectionBooksDTO))
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oopsie woopsie! het is stukkie wukkie OwO: {ex.Message}");
            return false;
        }

        return false;
    }
}