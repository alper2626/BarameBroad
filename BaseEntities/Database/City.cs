using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseEntities.Database
{
    [Description("Master Şehir")]
    public class MasterCity : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public Guid CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual MasterCountry Country { get; set; }

        public virtual List<MasterSchool> Schools { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public virtual List<City> Childrens { get; set; }
    }

    [Description("Şehirler")]
    public class City : LangEntity<MasterCity>
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Name { get; set; }

        public string FriendlyName { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Description { get; set; }

    }

}