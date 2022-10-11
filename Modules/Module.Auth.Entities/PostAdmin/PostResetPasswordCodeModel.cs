using System;
using System.ComponentModel.DataAnnotations;
using BaseEntities.Concrete;

namespace Module.Auth.Entities.PostAdmin
{
    public class PostResetPasswordCodeModel : CreateModel
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Lütfen Mail Adresinizi Girin")]
        public string MailAddress { get; set; }

        [Required(ErrorMessage = "Lütfen Doğrulama Kodu Girin")]
        [MaxLength(6, ErrorMessage = "Doğrulama Kodu Hatalı")]
        [MinLength(6, ErrorMessage = "Doğrulama Kodu Hatalı")]
        public string Code { get; set; }

        public PostUserPasswordUpdateModel PostUserPasswordUpdateModel { get; set; }
    }
}