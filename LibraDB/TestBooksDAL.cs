using LibraDTO;
using LibraInterface;

namespace LibraDB;

public class TestBooksDAL : IBooks
{
    public BooksDTO CreateBookTest()
    {
        BooksDTO booksDtoMockData = new BooksDTO
        {
            Title = "Mock Book Title",
            Author = "Mock Book Author",
            ISBNNumber = "1234567890",
            Pages = 200,
            PagesRead = 100,
            Summary = "Mock Book Summary"
        };
        return booksDtoMockData;
    }
    
    public bool CreateBookTrueTest()
    {
        return true;
    }
}