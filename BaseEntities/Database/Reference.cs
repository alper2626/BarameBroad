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
    public class Reference : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string ReferenceName { get; set; }
    }
}
