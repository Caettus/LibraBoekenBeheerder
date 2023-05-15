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
        public CollectionDTO toDTO(Collection collectionClass)
        {
            return new CollectionDTO()
            {
                CollectionsID = collectionClass.CollectionsID,
                Name = collectionClass.Name
            };
        }

        public Collection toClass(CollectionDTO collectionsDto)
        {
            return new Collection()
            {
                CollectionsID = collectionsDto.CollectionsID,
                Name = collectionsDto.Name
            };
        }
    }
}
