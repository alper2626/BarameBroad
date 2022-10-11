using System;
using System.ComponentModel.DataAnnotations;
using BaseEntities.Concrete;

namespace Module.Auth.Entities.PostAdmin
{
    public class PostUserPasswordCreateModel : CreateModel
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Lütfen Şifre Girin")]
        [Compare(nameof(PasswordConfirm), ErrorMessage = "Şifreler Eşleşmiyor")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Bu Alan Boşluk İçeremez")]
        [MinLength(5, ErrorMessage = "Şifreniz En az 5 karakter olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Tekrar Girin")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler Eşleşmiyor")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Bu Alan Boşluk İçeremez")]
        [MinLength(5, ErrorMessage = "Şifreniz En az 5 karakter olmalıdır.")]
        public string PasswordConfirm { get; set; }
    }
}