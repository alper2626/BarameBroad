using Core.DataAccess.Concrete;
using Module.Auth.DataAccess.Abstract;
using Module.Auth.Entities;

namespace Module.Auth.DataAccess.Concrete
{
    public class NewsletterDal : EntityRepositoryBase<Newsletter>, INewsletterDal
    {

    }
}