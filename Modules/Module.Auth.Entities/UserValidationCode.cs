using System;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using BaseEntities.Concrete;
using Module.Auth.Entities.EnumTypes;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.Entities
{
    public class UserValidationCode : Entity
    {
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public DateTime ExpiresDate { get; set; }

        public string Code { get; set; }

        public bool IsUsed { get; set; }

        public ValidationCodeType ValidationCodeType { get; set; }
    }

    public class UserValidationCodeProfile : Profile
    {
        public UserValidationCodeProfile()
        {
            CreateMap<PostUserValidationCodeCreateModel, UserValidationCode>();
        }
    }
}