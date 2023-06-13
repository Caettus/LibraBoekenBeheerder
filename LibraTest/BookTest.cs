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
        Books booksClass = new Books(booksDALMock);
        
        
        
        BooksMapper booksMapper = new BooksMapper();
        booksMapper.toClass(booksDALMock);

        // Act
        bool result = booksClass.CreateBook(booksClassMockData, selectedCollectionId, selectedGenreId, configurationMock.Object);

        // Assert
        Assert.IsTrue(result);
    }
}