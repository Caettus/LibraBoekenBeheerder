using LibraDTO;
using LibraInterface;

namespace LibraDB;

public class TestGenreDAL : IGenre, IGenreService
{
    public bool CreateGenre(GenreDTO genreDto)
    {
        return true;
    }
    public bool DeleteGenre(int id)
    {
        return true;
    }
    public List<GenreDTO> GetAllGenres()
    {
        GenreDTO genreDto = new GenreDTO();
        {
            genreDto.GenreId = 1;
            genreDto.GenreName = "TestGenre";
        }
        GenreDTO genreDto2 = new GenreDTO();
        {
            genreDto2.GenreId = 2;
            genreDto2.GenreName = "TestGenre2";
        }
        List<GenreDTO> genres = new List<GenreDTO>();
        genres.Add(genreDto);
        genres.Add(genreDto2);
        return genres;
    }
    public List<GenreDTO> GetGenresFromBook(int id)
    {
        GenreDTO genreDto = new GenreDTO();
        {
            genreDto.GenreId = 1;
            genreDto.GenreName = "TestGenre";
        }
        GenreDTO genreDto2 = new GenreDTO();
        {
            genreDto2.GenreId = 2;
            genreDto2.GenreName = "TestGenre2";
        }
        List<GenreDTO> genres = new List<GenreDTO>();
        genres.Add(genreDto);
        genres.Add(genreDto2);
        return genres;
    }
    public bool LinkGenreToBook(int GenreId, int BookId, BookGenresDTO bookGenresDto)
    {
        return true;
    }
    
    public List<BooksDTO> GetBooksInGenre(int id)
    {
        BooksDTO booksDto = new BooksDTO();
        {
            booksDto.BookId = 1;
            booksDto.Title = "Test Book";
            booksDto.Author = "Test Author";
            booksDto.ISBNNumber = "Test ISBNNumber";
            booksDto.Pages = 1;
            booksDto.PagesRead = 1;
            booksDto.Summary = "Test Summary";
        }
        BooksDTO booksDto2 = new BooksDTO();
        {
            booksDto.BookId = 2;
            booksDto.Title = "Test Book2";
            booksDto.Author = "Test Author2";
            booksDto.ISBNNumber = "Test ISBNNumber2";
            booksDto.Pages = 2;
            booksDto.PagesRead = 2;
            booksDto.Summary = "Test Summary2";
        }
        List<BooksDTO> books = new List<BooksDTO>();
        books.Add(booksDto);
        books.Add(booksDto2);
        return books;
    }
    
    public GenreDTO GetAGenre(int id)
    {
        GenreDTO genreDto = new GenreDTO();
        {
            genreDto.GenreId = 1;
            genreDto.GenreName = "Test Genre";
        }
        return genreDto;
    }
}