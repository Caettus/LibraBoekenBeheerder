using LibraDTO;


namespace LibraInterface;

public interface IBooks
{
    bool CreateBook(BooksDTO booksDTO);
    bool EditBook(BooksDTO booksDto);
    int GetLastInsertedBookId();
    List<BooksDTO> GetAllBooks();
    BooksDTO GetABook(int id);
}