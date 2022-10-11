using BaseEntities.Concrete;
using BaseEntities.Response;

namespace Module.Auth.Business.Abstract
{
    public interface IOperationClaimService
    {
        DataResponse GetClaims();

        DataResponse GetClaimsForUser(SelectBoxModel model);
    }
}