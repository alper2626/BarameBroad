using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BaseEntities.Abstract;

namespace Notification.Entities.Concrete
{
    public class SendSiteMailModel : IPostModel
    {
        [Required(ErrorMessage = "Lütfen Ad Soyad Bilgisi Girin")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lütfen İrtibat Numarınızı Girin")]
        public string PhoneNumber { get; set; }

        public string MailAddress { get; set; }

        [Required(ErrorMessage = "Lütfen Mesaj İçeriği Girin")]
        public string Content { get; set; }

        public string Extra { get; set; }
    }

}