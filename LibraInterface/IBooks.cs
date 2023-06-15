using LibraDTO;
using System.Numerics;

namespace LibraInterface;

public interface IBooks
{
    
    bool EditBook(BooksDTO booksDto, int id);
    int GetLastInsertedBookId();
    BooksDTO GetABook(int id);
    bool DeleteBook(int id);
}