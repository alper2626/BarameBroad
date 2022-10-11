using System.ComponentModel;
using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;

namespace Module.Auth.Entities
{
    [Description("Abonelik")]
    public class Newsletter : Entity
    {
        [PropertyName("Mail Adresi")]
        public string MailAddress { get; set; }

        public bool Registered { get; set; }
    }
}