using BaseEntities.Response;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        DataResponse SetUserOperationClaims(PostUserOperationClaimModel model);
    }
}