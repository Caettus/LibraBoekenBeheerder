using LibraBoekenBeheerder.Models;
using LibraDB;

namespace LibraLogic;

public class CollectionsMapper
{
    public CollectionsDTO toDTO(CollectionsModel collectionsModel)
    {
        return new CollectionsDTO()
        {
            CollectionsID = collectionsModel.CollectionID,
            Name = collectionsModel.Name
        };
    }

    public CollectionsModel toModel(CollectionsDTO collectionsDto)
    {
        return new CollectionsModel()
        {
            CollectionID = collectionsDto.CollectionsID,
            Name = collectionsDto.Name
        };
    }
}