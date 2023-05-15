using LibraBoekenBeheerder.Models;
using LibraDTO;

namespace LibraLogic;

public class CollectionBooksMapper
{
    public CollectionBooksDTO toDTO(CollectionBooksModel collectionsBooksModel)
    {
        return new CollectionBooksDTO()
        {
            CollectionID = collectionsBooksModel.CollectionID,
            BookId = collectionsBooksModel.BookId
        };
    }

    public CollectionBooksModel toModel(CollectionBooksDTO collectionBooksDto)
    {
        return new CollectionBooksModel()
        {
            CollectionID = collectionBooksDto.CollectionID,
            BookId = collectionBooksDto.BookId
        };
    }
}