using LibraDTO;
using LibraInterface;

namespace LibraDB;

public class TestBooksDAL : IBooks, IBooksCollection
{
    public bool CreateBook(BooksDTO booksDto)
    {
        return true;
    }

    public bool DeleteBook(int id)
    {
        return true;
    }
    
    public bool EditBook(BooksDTO booksDto, int id)
    {
        return true;
    }
    
    public int GetLastInsertedBookId()
    {
        return 1;
    }
    
    public List<BooksDTO> GetAllBooks()
    {
        BooksDTO booksDTO = new BooksDTO();
        {
            booksDTO.BookId = 1;
            booksDTO.Title = "TestTitle";
            booksDTO.Author = "TestAuthor";
            booksDTO.ISBNNumber = "TestISBNNumber";
            booksDTO.Pages = 1;
            booksDTO.PagesRead = 1;
            booksDTO.Summary = "TestSummary";
        }
        BooksDTO booksDTO2 = new BooksDTO();
        {
            booksDTO2.BookId = 2;
            booksDTO2.Title = "TestTitle2";
            booksDTO2.Author = "TestAuthor2";
            booksDTO2.ISBNNumber = "TestISBNNumber2";
            booksDTO2.Pages = 2;
            booksDTO2.PagesRead = 2;
            booksDTO2.Summary = "TestSummary2";
        }
        List<BooksDTO> booksDTOs = new List<BooksDTO>();
        booksDTOs.Add(booksDTO);
        booksDTOs.Add(booksDTO2);
        return booksDTOs;
    }
    
    public BooksDTO GetABook(int id)
    {
        BooksDTO booksDTO = new BooksDTO();
        {
            booksDTO.BookId = 1;
            booksDTO.Title = "TestTitle";
            booksDTO.Author = "TestAuthor";
            booksDTO.ISBNNumber = "TestISBNNumber";
            booksDTO.Pages = 1;
            booksDTO.PagesRead = 1;
            booksDTO.Summary = "TestSummary";
        }
        return booksDTO;
    }
}