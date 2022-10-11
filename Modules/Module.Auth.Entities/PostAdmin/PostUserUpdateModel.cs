using System;
using System.ComponentModel.DataAnnotations;
using BaseEntities.Abstract;
using BaseEntities.Concrete;

namespace Module.Auth.Entities.PostAdmin
{
    public class PostUserUpdateModel : UpdateModel,IUserAssignableModel
    {
        public string UserLoginName { get; set; }

        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Lütfen İsim Girin")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lütfen Soyisim Girin")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Lütfen Mail Adresi Girin")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen Geçerli Mail Adresi Girin")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Bu Alan Boşluk İçeremez")]
        public string MailAddress { get; set; }

        [Required(ErrorMessage = "Lütfen Telefon Numarası Girin")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Lütfen Sisteme Giriş İçin Kullanılacak Ad Girin")]
        [RegularExpression(@"^[a-zA-Z0-9_.-@]*$", ErrorMessage = "Bu Alan Harf Rakam ve (.-_) karakterlerinden Oluşabilir. Türkçe karakter içeremez.")]
        [MinLength(5, ErrorMessage = "Kullanıcı Adınız En az 5 karakter olmalıdır.")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Lütfen Doğum Günü Girin")]
        public DateTime BirthDay { get; set; }
    }
}