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
        ICollection collectionDALMock = new TestCollectionDAL();
        ICollectionService collectionServiceDALMock = new TestCollectionDAL();
        CollectionService collectionService = new CollectionService(collectionServiceDALMock);
        
        CollectionClass collectionClassMockData = new CollectionClass(collectionDALMock);
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
        ICollection collectionDALMock = new TestCollectionDAL();
        ICollectionService collectionServiceDALMock = new TestCollectionDAL();
        CollectionService collectionService = new CollectionService(collectionServiceDALMock);
        
        CollectionClass collectionClassMockData = new CollectionClass(collectionDALMock);
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
        ICollectionService collectionServiceDALMock = new TestCollectionDAL();
        CollectionService collectionService = new CollectionService(collectionServiceDALMock);
        
        // Act
        List<CollectionClass> result = collectionService.ReturnAllCollections();

        // Assert
        Assert.AreEqual(2, result.Count);
    }
    
    [TestMethod]
    public void GetCollectionsContainingBookTest()
    {
        // Arrange
        ICollectionService collectionServiceDALMock = new TestCollectionDAL();
        CollectionService collectionService = new CollectionService(collectionServiceDALMock);
        
        // Act
        List<CollectionClass> result = collectionService.ReturnCollectionsContaintingBook(1);

        // Assert
        Assert.AreEqual(2, result.Count);
    }
    
    [TestMethod]
    public void GetCollectionsNotContainingBookTest()
    {
        // Arrange
        ICollectionService collectionServiceDALMock = new TestCollectionDAL();
        CollectionService collectionService = new CollectionService(collectionServiceDALMock);
        
        // Act
        List<CollectionClass> result = collectionService.ReturnCollectionsNotContaintingBook(1);

        // Assert
        Assert.AreEqual(2, result.Count);
    }
    
    [TestMethod]
    public void ReturnACollectionTest()
    {
        // Arrange
        ICollection collectionDALMock = new TestCollectionDAL();
        CollectionClass collectionClass = new CollectionClass(collectionDALMock);
        
        // Act
        CollectionClass result = collectionClass.ReturnACollection(1);

        // Assert
        Assert.AreEqual(1, result.CollectionsID);
    }

    [TestMethod]
    public void RemoveLinkBookToCollectionTest()
    {
        // Arrange
        ICollection collectionDALMock = new TestCollectionDAL();
        CollectionClass collectionClass = new CollectionClass(collectionDALMock);
        
        // Act
        bool result = collectionClass.RemoveLinkBookToCollection(1, 1);
        
        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ReturnBooksInCollectionTest()
    {
        // Arrange
        ICollection collectionDALMock = new TestCollectionDAL();
        CollectionClass collectionClass = new CollectionClass(collectionDALMock);
        
        // Act
        List<Books> result = collectionClass.ReturnBooksInCollection(1);
        
        // Assert
        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public void DeleteCollectionTest()
    {
        // Arrange
        ICollection collectionDALMock = new TestCollectionDAL(); 
        CollectionClass collectionClass = new CollectionClass(collectionDALMock);
        
        // Act
        bool result = collectionClass.DeleteCollection(1);
        
        // Assert
        Assert.IsTrue(result);
    }
}