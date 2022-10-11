using Core.DataAccess.Abstract;
using Module.Auth.Entities;

namespace Module.Auth.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepositoryBase<User>
    {
    }
}