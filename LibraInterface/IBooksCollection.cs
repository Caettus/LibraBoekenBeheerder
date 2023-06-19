using LibraDTO;

namespace LibraInterface;

public interface IBooksCollection
{
    bool CreateBook(BooksDTO booksDTO);
    
    List<BooksDTO> GetAllBooks();
    
    int GetLastInsertedBookId();
}