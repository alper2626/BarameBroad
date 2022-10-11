using AutoMapper;
using BaseEntities.Database;
using BaseEntities.Poco;

namespace BarameBroad.WebUI.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BlogEntity, GetBlogModel>()
                .ForMember(w => w.BlogCategoryName, q => q.MapFrom(e => e.Master.BlogCategory.Name));
        }
    }
}
