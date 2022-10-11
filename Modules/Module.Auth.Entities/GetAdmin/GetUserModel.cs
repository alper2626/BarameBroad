using System;
using BaseEntities.Concrete;

namespace Module.Auth.Entities.GetAdmin
{
    public class GetUserModel : GetModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string LoginName { get; set; }

        public DateTime BirthDay { get; set; }
    }
}