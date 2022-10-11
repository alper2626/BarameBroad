using BaseEntities.Response;
using Core.DataAccess.Abstract;
using Module.Auth.Entities;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.DataAccess.Abstract
{
    public interface IUserOperationClaimDal : IEntityRepositoryBase<UserOperationClaim>
    {
        DataResponse SetUserOperationClaims(PostUserOperationClaimModel model);
    }
}