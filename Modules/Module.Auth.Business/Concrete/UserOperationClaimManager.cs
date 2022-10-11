using BaseEntities.Response;
using Module.Auth.Business.Abstract;
using Module.Auth.DataAccess.Abstract;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        public DataResponse SetUserOperationClaims(PostUserOperationClaimModel model)
        {
             return _userOperationClaimDal.SetUserOperationClaims(model);
        }
    }
}