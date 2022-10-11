using System;
using BaseEntities.Abstract;
using Module.Auth.Entities.GetAdmin;

namespace Module.Auth.Entities.MailModel
{
    
    public class ResetPasswordMailModel : IPostModel
    {
        public string DirectUrl { get; set; }

        public string Code { get; set; }

        public GetUserModel GetUserModel { get; set; }

        public DateTime CreateTime => DateTime.Now;
    }


}