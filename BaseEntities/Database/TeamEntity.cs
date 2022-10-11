using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;

namespace BaseEntities.Database
{
    public class MasterTeam : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string LastName { get; set; }

        public string Facebook { get; set; }

        public string Instagram { get; set; }

        public string Linkedn { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public virtual List<TeamEntity> Childrens { get; set; }
    }

    public class TeamEntity : LangEntity<MasterTeam>
    {

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Duty { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Description { get; set; }
    }

}