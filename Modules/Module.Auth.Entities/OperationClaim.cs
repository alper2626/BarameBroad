using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using BaseEntities.Concrete;
using BaseEntities.EnumTypes;
using CrossCuttingConcerns.Attributes;
using Module.Auth.Entities.GetAdmin;

namespace Module.Auth.Entities
{
    [Description("Roller")]
    public class OperationClaim : Entity
    {
        public string Name { get; set; }

        [PropertyName("Rol")]
        public string DisplayName { get; set; }

        [InverseProperty("OperationClaim")]
        public virtual ICollection<UserOperationClaim> OperationClaimUsers { get; set; }

    }

    public class OperationClaimProfile : Profile
    {
        public OperationClaimProfile()
        {
            CreateMap<OperationClaim, GetOperationClaimModel>();
            CreateMap<OperationClaim, FormItemStatusModel>()
                .ForMember(w => w.OldStatus, c => c.MapFrom(r => FormItemStatus.NotChange))
                .ForMember(w => w.NewStatus, c => c.MapFrom(r => FormItemStatus.NotChange));
            
            CreateMap<OperationClaim, SelectBoxModel>()
                .ForMember(w => w.DisplayValue, q => q.MapFrom(x => x.DisplayName));
        }
    }

}