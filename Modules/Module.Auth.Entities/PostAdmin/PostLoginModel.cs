using System.ComponentModel.DataAnnotations;
using BaseEntities.Abstract;

namespace Module.Auth.Entities.PostAdmin
{
    public class PostLoginModel : IPostModel
    {
        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Girin.")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Girin.")]
        public string Password { get; set; }

        

    }
}