using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraDB;

namespace DAL_Interface
{
    internal interface IBooks
    {
        bool CreateBook(BooksDTO booksDTO);
    }
}
