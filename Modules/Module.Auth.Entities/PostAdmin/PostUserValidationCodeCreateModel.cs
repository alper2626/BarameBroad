using System;
using BaseEntities.Concrete;
using Extensions.Generators;
using Module.Auth.Entities.EnumTypes;

namespace Module.Auth.Entities.PostAdmin
{
    public class PostUserValidationCodeCreateModel : CreateModel
    {
        public PostUserValidationCodeCreateModel(TimeSpan expiresMinute)
        {
            this.ExpiresDate = DateTime.Now.AddMinutes(expiresMinute.Minutes);
        }
        public Guid UserId { get; set; }

        public string Code => RandomGenerator.CreateNumber(6).ToString();

        public DateTime ExpiresDate { get; }

        public bool IsUsed => false;

        public ValidationCodeType ValidationCodeType { get; set; }
    }
}