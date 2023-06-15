using LibraDB;
using LibraDTO;
using LibraInterface;
using LibraLogic;
using LibraLogic.Mappers;
using Microsoft.Extensions.Configuration;

namespace LibraTest;

[TestClass]
public class BooktTest
{
    
    [TestMethod]
    public void CreateBookTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        IBooksCollection booksDALForCollectionMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        BooksCollection booksCollection = new BooksCollection(booksDALForCollectionMock, collectionDALMock, genreDALMock);
        
        Books booksClassMockData = new Books(booksDALMock, collectionDALMock, genreDALMock);
        {
            booksClassMockData.BookId = 1;
            booksClassMockData.Title = "TestTitle";
            booksClassMockData.Author = "TestAuthor";
            booksClassMockData.ISBNNumber = "TestISBNNumber";
            booksClassMockData.Pages = 1;
            booksClassMockData.PagesRead = 1;
            booksClassMockData.Summary = "TestSummary";
        }
        int selectedCollectionId = 1;
        int selectedGenreId = 1;

        // Act
        bool result = booksCollection.CreateBook(booksClassMockData, selectedCollectionId, selectedGenreId);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void GetAllBooksTest()
    {
        // Arrange
        
        IBooksCollection booksDALForCollectionMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        
        BooksCollection booksCollection = new BooksCollection(booksDALForCollectionMock, collectionDALMock, genreDALMock);
        
        // Act
        List<Books> result = booksCollection.ReturnAllBooks();

        // Assert
        Assert.AreEqual(2, result.Count);
    }
    
    [TestMethod]
    public void GetABookTest()
    {
        // Arranged
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        
        Books booksClass = new Books(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        Books result = booksClass.GetABook(1);

        // Assert
        Assert.AreEqual(1, result.BookId);
    }
    
    [TestMethod]
    public void EditBookTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        
        Books booksClassMockData = new Books(booksDALMock, collectionDALMock, genreDALMock);
        {
            booksClassMockData.BookId = 1;
            booksClassMockData.Title = "TestTitle";
            booksClassMockData.Author = "TestAuthor";
            booksClassMockData.ISBNNumber = "TestISBNNumber";
            booksClassMockData.Pages = 1;
            booksClassMockData.PagesRead = 1;
            booksClassMockData.Summary = "TestSummary";
        }
        int selectedCollectionId = 1;
        int bookId = 1;
        Books booksClass = new Books(booksDALMock, collectionDALMock, genreDALMock);

        // Act
        bool result = booksClass.EditBook(booksClassMockData, selectedCollectionId, bookId);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void DeleteBookTest()
    {
        // Arrange
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        
        Books booksClass = new Books(booksDALMock, collectionDALMock, genreDALMock);
        int bookId = 1;
        // Act
        bool result = booksClass.DeleteBook(bookId);
        // Assert
        Assert.IsTrue(result);
    }

    // [TestMethod]
    // public void FailCreateBookTest()
    // {
    //     // Arrange
    //     
    //     IBooks booksDALMock = new TestBooksDAL();
    //     ICollection collectionDALMock = new TestCollectionDAL();
    //     IGenre genreDALMock = new TestGenreDAL();
    //     BooksCollection booksCollection = new BooksCollection(booksDALMock, collectionDALMock, genreDALMock);
    //     
    //     Books booksClassMockData = new Books(booksDALMock, collectionDALMock, genreDALMock);
    //     {
    //         booksClassMockData.BookId = 1;
    //         booksClassMockData.Title = "";
    //         booksClassMockData.Author = "TestAuthor";
    //         booksClassMockData.ISBNNumber = "TestISBNNumber";
    //         booksClassMockData.Pages = 1;
    //         booksClassMockData.PagesRead = 1;
    //         booksClassMockData.Summary = "TestSummary";
    //     }
    //     int selectedCollectionId = 1;
    //     int selectedGenreId = 1;
    //
    //     // Act
    //     bool result = booksCollection.CreateBook(booksClassMockData, selectedCollectionId, selectedGenreId);
    //
    //     // Assert
    //     Assert.IsFalse(result);
    // }
    
    
    [TestMethod]
    public void GetAllBooksTest_Fails()
    {
        // Arrange
        IBooks booksDALMock = new TestBooksDAL();
        IBooksCollection booksDALForCollectionMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
    
        BooksCollection booksCollection = new BooksCollection(booksDALForCollectionMock, collectionDALMock, genreDALMock);
    
        // Act
        List<Books> result = booksCollection.ReturnAllBooks();

        // Assert
        bool assertionFailed = false;
        foreach (var book in result)
        {
            if (book.BookId == 0)
            {
                assertionFailed = true;
                break;
            }
        }

        if (assertionFailed)
        {
            Assert.Fail("The GetAllBooksTest failed because a book with ID 0 was found.");
        }
    }
    
    [TestMethod]
    public void GetABookTest_Fails()
    {
        // Arranged
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        
        Books booksClass = new Books(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        Books result = booksClass.GetABook(1);

        // Assert
        Assert.AreNotEqual(2, result.BookId);
    }
    
    // [TestMethod]
    // public void EditBookTest_Fails()
    // {
    //     // Arrange
    //     
    //     IBooks booksDALMock = new TestBooksDAL();
    //     ICollection collectionDALMock = new TestCollectionDAL();
    //     IGenre genreDALMock = new TestGenreDAL();
    //     IConfiguration configuration = new TestConfiguration();
    //     
    //     Books booksClassMockData = new Books(booksDALMock, collectionDALMock, genreDALMock);
    //     {
    //         booksClassMockData.BookId = 1;
    //         booksClassMockData.Title = "TestTitle";
    //         booksClassMockData.Author = "TestAuthor";
    //         booksClassMockData.ISBNNumber = "TestISBNNumber";
    //         booksClassMockData.Pages = 1;
    //         booksClassMockData.PagesRead = 1;
    //         booksClassMockData.Summary = "TestSummary";
    //     }
    //     int selectedCollectionId = 1;
    //     int bookId = 1;
    //     Books booksClass = new Books(booksDALMock, collectionDALMock, genreDALMock);
    //
    //     // Act
    //     bool result = booksClass.EditBook(booksClassMockData, selectedCollectionId, bookId);
    //
    //     // Assert
    //     Assert.IsTrue(result);
    // }

}