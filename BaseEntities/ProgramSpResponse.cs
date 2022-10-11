using BaseEntities.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseEntities
{
    public class ProgramSpResponse
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public short EntityRelationType { get; set; }

        public EntityRelationType EntityRelationTypeEnum => (EntityRelationType)EntityRelationType;
    }
}
