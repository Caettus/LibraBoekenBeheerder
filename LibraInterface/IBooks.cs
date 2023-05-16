using LibraDTO;
using System.Numerics;

namespace LibraInterface;

public interface IBooks
{
    bool CreateBook(BooksDTO booksDTO);
    bool EditBook(BooksDTO booksDto);
    int GetLastInsertedBookId();
    List<BooksDTO> GetAllBooks();
    BooksDTO GetABook(int id);
    bool DeleteBook(int id);
}