using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using BaseEntities.Concrete;
using CrossCuttingConcerns.Attributes;
using Module.Auth.Entities.GetAdmin;

namespace Module.Auth.Entities
{
    [Description("Kullanıcı Rolleri")]
    public class UserOperationClaim : Entity
    {
        [PropertyName("Üyelik")]
        [ForeignValue("Users-LoginName")]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [PropertyName("Rol")]
        [ForeignValue("OperationClaims-DisplayName")]
        public Guid OperationClaimId { get; set; }

        [ForeignKey("OperationClaimId")]
        public virtual OperationClaim OperationClaim { get; set; }

    }

    public class UserOperationClaimProfile : Profile
    {
        public UserOperationClaimProfile()
        {
            CreateMap<UserOperationClaim, GetOperationClaimModel>()
                .ForMember(w => w.Id, e => e.MapFrom(q => q.OperationClaimId))
                .ForMember(w => w.Name, e => e.MapFrom(q => q.OperationClaim.Name));
        }
    }

}