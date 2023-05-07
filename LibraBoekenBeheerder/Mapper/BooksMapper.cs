using LibraBoekenBeheerder.Models;
using LibraDB;

namespace LibraLogic;

public class BooksMapper
{
    public BooksDTO toDTO(BooksModel booksModel)
    {
        return new BooksDTO()
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
    public BooksModel toModel(BooksDTO booksDto)
    {
        return new BooksModel()
        {
            BookId = booksDto.BookId,
            Title = booksDto.Title,
            Author = booksDto.Author,
            ISBNNumber = booksDto.ISBNNumber,
            Pages = booksDto.Pages,
            PagesRead = booksDto.PagesRead,
            Summary = booksDto.Summary
        };
    }
}