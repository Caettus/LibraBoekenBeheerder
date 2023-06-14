using LibraDTO;
using LibraInterface;

namespace LibraDB;

public class TestCollectionDAL : ICollection
{
    public bool LinkBookToCollection(int CollectionID, int BookID, CollectionBooksDTO collectionBooksDTO)
    {
        return true;
    }
    public List<CollectionDTO> GetAllCollections()
    {
        CollectionDTO collectionDto = new CollectionDTO();
        {
            collectionDto.CollectionsID = 1;
            collectionDto.Name = "Test Collection";
        }
        CollectionDTO collectionDto2 = new CollectionDTO();
        {
            collectionDto.CollectionsID = 2;
            collectionDto.Name = "Test Collection2";
        }
        List<CollectionDTO> collections = new List<CollectionDTO>();
        collections.Add(collectionDto);
        collections.Add(collectionDto2);
        return collections;
    }
    public CollectionDTO GetACollection(int id)
    {
        CollectionDTO collectionDto = new CollectionDTO();
        {
            collectionDto.CollectionsID = 1;
            collectionDto.Name = "Test Collection";
        }
        return collectionDto;
    }
    public bool CreateCollection(CollectionDTO collectionsDto)
    {
        return true;
    }
    public List<CollectionDTO> GetCollectionsNotContainingBook(int id)
    {
        CollectionDTO collectionDto = new CollectionDTO();
        {
            collectionDto.CollectionsID = 1;
            collectionDto.Name = "Test Collection";
        }
        CollectionDTO collectionDto2 = new CollectionDTO();
        {
            collectionDto.CollectionsID = 2;
            collectionDto.Name = "Test Collection2";
        }
        List<CollectionDTO> collections = new List<CollectionDTO>();
        collections.Add(collectionDto);
        collections.Add(collectionDto2);
        return collections;
    }
    public List<CollectionDTO> GetCollectionsContainingBook(int id)
    {
        CollectionDTO collectionDto = new CollectionDTO();
        {
            collectionDto.CollectionsID = 1;
            collectionDto.Name = "Test Collection";
        }
        CollectionDTO collectionDto2 = new CollectionDTO();
        {
            collectionDto.CollectionsID = 2;
            collectionDto.Name = "Test Collection2";
        }
        List<CollectionDTO> collections = new List<CollectionDTO>();
        collections.Add(collectionDto);
        collections.Add(collectionDto2);
        return collections;
    }

    public List<BooksDTO> GetBooksInCollection(int id)
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
    public bool RemoveLinkBookFromCollection(int CollectionID, int BookId)
    {
        return true;
    }
    public bool DeleteCollection(int id)
    {
        return true;
    }
}