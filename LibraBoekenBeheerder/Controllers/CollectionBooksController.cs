using LibraDB;
using LibraLogic;
using Microsoft.AspNetCore.Mvc;

namespace LibraBoekenBeheerder.Controllers;

public class CollectionBooksController
{
    private readonly CollectionBooksDAL _collectionBooksDal;

    public CollectionBooksController(IConfiguration configuration)
    {
        _collectionBooksDal = new CollectionBooksDAL(configuration);
    }

    CollectionBooksMapper _collectionBooksMapper = new CollectionBooksMapper();
    

}