using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseEntities.Database
{
    public class UserComment : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public DateTime CommentDate { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Description { get; set; }
    }

}
