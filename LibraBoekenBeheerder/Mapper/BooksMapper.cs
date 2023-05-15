using LibraBoekenBeheerder.Models;
using LibraDTO;

namespace LibraLogic;

public class BooksMapper
{
    public Books toClass(BooksModel booksModel)
    {
        return new Books()
        {
            BookId = booksModel.BookId,
            Title = booksModel.Title,
            Author = booksModel.Author,
            ISBNNumber = booksModel.ISBNNumber,
            Pages = booksModel.Pages,
            PagesRead = booksModel.PagesRead,
            Summary = booksModel.Summary
        };
    }
    public BooksModel toModel(Books booksClass)
    {
        return new BooksModel()
        {
            BookId = booksClass.BookId,
            Title = booksClass.Title,
            Author = booksClass.Author,
            ISBNNumber = booksClass.ISBNNumber,
            Pages = booksClass.Pages,
            PagesRead = booksClass.PagesRead,
            Summary = booksClass.Summary
        };
    }
}