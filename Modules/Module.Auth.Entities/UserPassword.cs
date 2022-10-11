using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;

namespace Module.Auth.Entities
{
    [Description("Kullanıcı Şifreleri")]
    public class UserPassword : Entity
    {
        [ForeignValue("Users-LoginName")]
        [PropertyName("Üyelik")]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public byte[] HashedPassword { get; set; }

        public byte[] PasswordSalt { get; set; }
    }

}