using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;
using Module.Auth.Entities.GetAdmin;
using Module.Auth.Entities.PostAdmin;
using Newtonsoft.Json;

namespace Module.Auth.Entities
{
    [Description("Kullanıcılar")]
    public class User : Entity
    {
        [PropertyName("Ad")]
        public string FirstName { get; set; }

        [PropertyName("Soyad")]
        public string LastName { get; set; }

        [PropertyName("E-Posta")]
        public string MailAddress { get; set; }

        [PropertyName("Telefon")]
        public string PhoneNumber { get; set; }

        [PropertyName("K. Adi")]
        public string LoginName { get; set; }

        [PropertyName("Doğum T.")]
        public DateTime BirthDay { get; set; }

        public string SocialRef { get; set; }

        [InverseProperty("User")]
        [JsonIgnore]
        public virtual ICollection<UserOperationClaim> UserClaims { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserPassword> UserPasswords { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserValidationCode> UserValidationCodes { get; set; }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetLoggedUserWithClaimModel>()
                .ForMember(w => w.UserClaims, a => a.MapFrom(c => c.UserClaims));

            CreateMap<User, PostUserUpdateModel>()
                .ForMember(w => w.UserLoginName, q => q.Ignore())
                .ForMember(w => w.UserId, q => q.MapFrom(x => x.Id));

            CreateMap<PostUserUpdateModel,User>()
                .ForMember(w=>w.Id,q=>q.MapFrom(c=>c.UserId));

            CreateMap<User,GetUserModel>();

            CreateMap<PostUserCreateModel,User>()
                .ForMember(w=>w.UserClaims,e=>e.Ignore())
                .ForMember(w => w.UserPasswords, e => e.Ignore());
        }
    }
}