using LibraBoekenBeheerder.Models;
using LibraDB;

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

    public CollectionsModel toModel(Collection collection)
    {
        return new CollectionsModel()
        {
            CollectionID = collection.CollectionsID,
            Name = collection.Name
        };
    }
}