using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseEntities.Database
{

    public class MasterSiteConfig : Entity
    {
        public string FirstCounterText { get; set; }

        public string SecondCounterText { get; set; }

        public string ThirdCounterText { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public int FirstCounterValue { get; set; } = 0;

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public int SecondCounterValue { get; set; } = 0;

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public int ThirdCounterValue { get; set; } = 0;

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public virtual List<SiteConfig> Childrens { get; set; }
    }

    public class SiteConfig : LangEntity<MasterSiteConfig>
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string SiteName { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string AboutUs { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public string PhoneNumberFormat
        {
            get
            {
                var number = PhoneNumber.Replace("-", "");
                number = number.Replace(" ", "");
                number = number.Replace("(", "");
                number = number.Replace(")", "");
                return "+90" + number;
            }
        }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string MailAddress { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Address { get; set; }

    }

}