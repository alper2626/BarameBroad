using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;

namespace BaseEntities.Database
{
    [Description("Sosyal Medya")]
    public class SocialNetworkEntity : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Icon { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Link { get; set; }
    }

}