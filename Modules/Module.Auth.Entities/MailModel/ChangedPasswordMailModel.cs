using System;
using BaseEntities.Abstract;
using Module.Auth.Entities.GetAdmin;

namespace Module.Auth.Entities.MailModel
{
    public class ChangedPasswordMailModel : IPostModel
    {
        public GetUserModel GetUserModel { get; set; }

        public DateTime CreateTime => DateTime.Now;
    }
}