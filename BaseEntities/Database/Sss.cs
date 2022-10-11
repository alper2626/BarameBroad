using BaseEntities.Concrete;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseEntities.Database
{
    public class MasterSss : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public virtual List<Sss> Childrens { get; set; }
    }

    public class Sss : LangEntity<MasterSss>
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Description { get; set; }
    }
}