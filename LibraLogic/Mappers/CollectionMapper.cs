using LibraDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraLogic.Mappers
{
    internal class CollectionMapper
    {
        public CollectionDTO toDTO(CollectionClass collectionClassClass)
        {
            return new CollectionDTO()
            {
                CollectionsID = collectionClassClass.CollectionsID,
                Name = collectionClassClass.Name
            };
        }

        public CollectionClass toClass(CollectionDTO collectionsDto)
        {
            return new CollectionClass()
            {
                CollectionsID = collectionsDto.CollectionsID,
                Name = collectionsDto.Name
            };
        }
    }
}
