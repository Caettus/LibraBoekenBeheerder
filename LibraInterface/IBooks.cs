using LibraDB;


namespace LibraInterface;

public interface IBooks
{
    bool CreateBook(BooksDTO booksDTO);
    int GetLastInsertedBookId();
    List<BooksDTO> GetAllBooks();
    BooksDTO GetABook(int id);
}