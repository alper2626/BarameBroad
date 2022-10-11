using BaseEntities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace BaseEntities.Database
{
    public class MasterSiteSection : Entity
    {
        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public int Index { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string PartialName { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Name { get; set; }
    }

    public class SiteSection : LangEntity<MasterSiteSection>
    {

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur !")]
        public string Description { get; set; }
    }
}
