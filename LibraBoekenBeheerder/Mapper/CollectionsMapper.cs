using LibraBoekenBeheerder.Models;
using LibraDTO;

namespace LibraLogic;

public class CollectionsMapper
{
    public Collection toClass(CollectionsModel collectionsModel)
    {
        return new Collection()
        {
            CollectionsID = collectionsModel.CollectionID,
            Name = collectionsModel.Name
        };
    }

    public CollectionsModel toModel(Collection collectionClass)
    {
        return new CollectionsModel()
        {
            CollectionID = collectionClass.CollectionsID,
            Name = collectionClass.Name
        };
    }
}