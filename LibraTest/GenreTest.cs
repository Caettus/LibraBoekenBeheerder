using LibraDB;
using LibraInterface;
using LibraLogic;
using Microsoft.Extensions.Configuration;

namespace LibraTest;

[TestClass]
public class GenreTest
{
    [TestMethod]
    public void CreateGenreTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        GenreService genreService = new GenreService(booksDALMock, collectionDALMock, genreDALMock);
        
        Genre genreClassMockData = new Genre(genreDALMock);
        {
            genreClassMockData.GenreId = 1;
            genreClassMockData.GenreName = "TestGenreName";
        }

        // Act
        bool result = genreService.CreateGenre(genreClassMockData, configuration);

        // Assert
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void GetAllGenresTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        GenreService genreService = new GenreService(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        List<Genre> result = genreService.ReturnAllGenres(configuration);

        // Assert
        Assert.AreEqual(2, result.Count);
    }
    
    [TestMethod]
    public void GetGenresFromBookTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        GenreService genreService = new GenreService(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        List<Genre> result = genreService.ReturnGenresFromBook(1, configuration);

        // Assert
        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public void DeleteGenre()
    {
        // Arrange
        
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        Genre genreClass = new Genre(genreDALMock);
        
        // Act
        bool result = genreClass.DeleteGenre(1);
        
        // Assert
        Assert.IsTrue(result);
    }
}