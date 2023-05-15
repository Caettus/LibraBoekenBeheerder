using LibraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraLogic.Mappers
{
    public class BooksMapper
    {
        public BooksDTO toDTO(Books booksClass)
        {
            return new BooksDTO()
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
        public Books toClass(BooksDTO booksDto)
        {
            return new Books()
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
}
