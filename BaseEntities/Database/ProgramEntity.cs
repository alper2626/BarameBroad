using BaseEntities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseEntities.Database
{
    public class MasterProgram : Entity
    {

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public int Order { get; set; }

        public Guid? MasterProgramId { get; set; }

        [ForeignKey("MasterProgramId")]
        public virtual MasterProgram MasterProgramEntity { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public virtual List<ProgramEntity> Childrens { get; set; }

        public virtual List<ProgramRelation> ProgramRelations { get; set; }
    }

    public class ProgramEntity : LangEntity<MasterProgram>
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Description { get; set; }

        public string FriendlyName { get; set; }
    }

    public class ProgramDto
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public MasterProgram MasterProgram { get; set; }

        public List<Guid> Countries { get; set; }

        public List<Guid> Cities { get; set; }

        public List<Guid> Schools { get; set; }
    }
}
