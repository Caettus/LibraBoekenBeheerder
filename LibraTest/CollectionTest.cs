using LibraDB;
using LibraInterface;
using LibraLogic;
using Microsoft.Extensions.Configuration;

namespace LibraTest;

[TestClass]
public class CollectionTest
{
    [TestMethod]
    public void CreateCollectionTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        CollectionService collectionService = new CollectionService(booksDALMock, collectionDALMock, genreDALMock);
        
        CollectionClass collectionClassMockData = new CollectionClass(booksDALMock, collectionDALMock, genreDALMock);
        {
            collectionClassMockData.CollectionsID = 1;
            collectionClassMockData.Name = "TestCollectionName";
        }

        // Act
        bool result = collectionService.CreateCollection(collectionClassMockData, configuration);

        // Assert
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void GetAllCollectionsTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        CollectionService collectionService = new CollectionService(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        List<CollectionClass> result = collectionService.ReturnAllCollections(configuration);

        // Assert
        Assert.AreEqual(2, result.Count);
    }
    
    [TestMethod]
    public void GetCollectionsContainingBookTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        CollectionService collectionService = new CollectionService(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        List<CollectionClass> result = collectionService.ReturnCollectionsContaintingBook(1, configuration);

        // Assert
        Assert.AreEqual(2, result.Count);
    }
    
    [TestMethod]
    public void GetCollectionsNotContainingBookTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        CollectionService collectionService = new CollectionService(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        List<CollectionClass> result = collectionService.ReturnCollectionsNotContaintingBook(1, configuration);

        // Assert
        Assert.AreEqual(2, result.Count);
    }
    
    [TestMethod]
    public void ReturnACollectionTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        CollectionClass collectionClass = new CollectionClass(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        CollectionClass result = collectionClass.ReturnACollection(configuration, 1);

        // Assert
        Assert.AreEqual(1, result.CollectionsID);
    }

    [TestMethod]
    public void RemoveLinkBookToCollectionTest()
    {
        // Arrange
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        CollectionClass collectionClass = new CollectionClass(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        bool result = collectionClass.RemoveLinkBookToCollection(1, 1, configuration);
        
        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ReturnBooksInCollectionTest()
    {
        // Arrange
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        CollectionClass collectionClass = new CollectionClass(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        List<Books> result = collectionClass.ReturnBooksInCollection(1, configuration);
        
        // Assert
        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public void DeleteCollectionTest()
    {
        // Arrange
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        IConfiguration configuration = new TestConfiguration();
        CollectionClass collectionClass = new CollectionClass(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        bool result = collectionClass.DeleteCollection(configuration, 1);
        
        // Assert
        Assert.IsTrue(result);
    }
}