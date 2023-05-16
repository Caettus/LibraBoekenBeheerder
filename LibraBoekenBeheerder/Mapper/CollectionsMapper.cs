using LibraBoekenBeheerder.Models;
using LibraDTO;

namespace LibraLogic;

public class CollectionsMapper
{
    public CollectionClass toClass(CollectionsModel collectionsModel)
    {
        return new CollectionClass()
        {
            CollectionsID = collectionsModel.CollectionID,
            Name = collectionsModel.Name
        };
    }

    public CollectionsModel toModel(CollectionClass collectionClassClass)
    {
        return new CollectionsModel()
        {
            CollectionID = collectionClassClass.CollectionsID,
            Name = collectionClassClass.Name
        };
    }
}