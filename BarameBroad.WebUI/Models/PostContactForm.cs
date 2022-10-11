using BaseEntities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace BarameBroad.WebUI.Models
{
    public class PostContactForm : IPostModel
    {
        [Required(ErrorMessage = "Lütfen Tam Adınızı Girin.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Lütfen E-posta Adresinizi Girin.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Lütfen Konu Girin.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Lütfen MEsajınızı Girin.")]
        public string Message { get; set; }
    }
}
