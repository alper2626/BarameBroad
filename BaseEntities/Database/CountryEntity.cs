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
    public class MasterCountry : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public virtual List<CountryEntity> Childrens { get; set; }

        public virtual List<City> Cities { get; set; }
    }

    public class CountryEntity : LangEntity<MasterCountry>
    {

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Name { get; set; }

        public string FriendlyName { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Description { get; set; }
    }
}
