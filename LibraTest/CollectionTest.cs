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
        ICollectionService collectionServiceDALMock = new TestCollectionDAL();
        CollectionService collectionService = new CollectionService(booksDALMock, genreDALMock,  collectionServiceDALMock);
        
        CollectionClass collectionClassMockData = new CollectionClass(booksDALMock, collectionDALMock, genreDALMock);
        {
            collectionClassMockData.CollectionsID = 1;
            collectionClassMockData.Name = "TestCollectionName";
        }

        // Act
        bool result = collectionService.CreateCollection(collectionClassMockData);

        // Assert
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void CreateCollectionTest_Fails()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        ICollection collectionDALMock = new TestCollectionDAL();
        IGenre genreDALMock = new TestGenreDAL();
        ICollectionService collectionServiceDALMock = new TestCollectionDAL();
        CollectionService collectionService = new CollectionService(booksDALMock, genreDALMock,  collectionServiceDALMock);
        
        CollectionClass collectionClassMockData = new CollectionClass(booksDALMock, collectionDALMock, genreDALMock);
        {
            collectionClassMockData.CollectionsID = 1;
            collectionClassMockData.Name = " ";
        }

        // Act
        bool result = collectionService.CreateCollection(collectionClassMockData);

        // Assert
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void GetAllCollectionsTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        IGenre genreDALMock = new TestGenreDAL();
        ICollectionService collectionServiceDALMock = new TestCollectionDAL();
        CollectionService collectionService = new CollectionService(booksDALMock, genreDALMock,  collectionServiceDALMock);
        
        // Act
        List<CollectionClass> result = collectionService.ReturnAllCollections();

        // Assert
        Assert.AreEqual(2, result.Count);
    }
    
    [TestMethod]
    public void GetCollectionsContainingBookTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        IGenre genreDALMock = new TestGenreDAL();
        ICollectionService collectionServiceDALMock = new TestCollectionDAL();
        CollectionService collectionService = new CollectionService(booksDALMock, genreDALMock,  collectionServiceDALMock);
        
        // Act
        List<CollectionClass> result = collectionService.ReturnCollectionsContaintingBook(1);

        // Assert
        Assert.AreEqual(2, result.Count);
    }
    
    [TestMethod]
    public void GetCollectionsNotContainingBookTest()
    {
        // Arrange
        
        IBooks booksDALMock = new TestBooksDAL();
        IGenre genreDALMock = new TestGenreDAL();
        ICollectionService collectionServiceDALMock = new TestCollectionDAL();
        CollectionService collectionService = new CollectionService(booksDALMock, genreDALMock,  collectionServiceDALMock);
        
        // Act
        List<CollectionClass> result = collectionService.ReturnCollectionsNotContaintingBook(1);

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
        CollectionClass collectionClass = new CollectionClass(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        CollectionClass result = collectionClass.ReturnACollection(1);

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
        CollectionClass collectionClass = new CollectionClass(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        bool result = collectionClass.RemoveLinkBookToCollection(1, 1);
        
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
        CollectionClass collectionClass = new CollectionClass(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        List<Books> result = collectionClass.ReturnBooksInCollection(1);
        
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
        CollectionClass collectionClass = new CollectionClass(booksDALMock, collectionDALMock, genreDALMock);
        
        // Act
        bool result = collectionClass.DeleteCollection(1);
        
        // Assert
        Assert.IsTrue(result);
    }
}