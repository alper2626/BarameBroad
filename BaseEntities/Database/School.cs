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
    public class MasterSchool : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public Guid CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual MasterCity City { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public virtual List<School> Childrens { get; set; }
    }

    public class School : LangEntity<MasterSchool>
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Name { get; set; }

        public string FriendlyName { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Description { get; set; }
    }
}
