using System;
using System.ComponentModel.DataAnnotations;
using BaseEntities.Abstract;
using BaseEntities.Concrete;

namespace Module.Auth.Entities.PostAdmin
{
    public class PostUserPasswordUpdateModel : UpdateModel, IUserAssignableModel
    {
        [Required(ErrorMessage = "Lütfen Eski Şifrenizi Girin")]
        public string OldPassword { get; set; }

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

        public string UserLoginName { get; set; }

        public Guid UserId { get; set; }
    }
}