using BaseEntities.Concrete;
using BaseEntities.EnumTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseEntities.Database
{
    public class ProgramRelation : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public Guid ProgramId { get; set; }

        [ForeignKey("ProgramId")]
        public virtual MasterProgram MasterProgram { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public Guid RelatedId { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public EntityRelationType EntityRelationType { get; set; }
    }
}
