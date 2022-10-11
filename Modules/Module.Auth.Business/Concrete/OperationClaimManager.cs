using System.Collections.Generic;
using System.Linq;
using System.Net;
using BaseEntities.Concrete;
using BaseEntities.EnumTypes;
using BaseEntities.Response;
using Extensions.AutoMapperExtensions;
using Module.Auth.Business.Abstract;
using Module.Auth.DataAccess.Abstract;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private IOperationClaimDal _operationClaimDal;
        private IUserOperationClaimDal _userOperationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal, IUserOperationClaimDal userOperationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
            _userOperationClaimDal = userOperationClaimDal;
        }

        public DataResponse GetClaims()
        {
            return new DataResponse
            {
                Data = AutoMapperWrapper.Mapper.Map<List<SelectBoxModel>>(_operationClaimDal.GetList()),
                Success = true,
                Message = "Kullanıcı Rolleri Listesi",
                StatusCode = HttpStatusCode.OK
            };
        }

        public DataResponse GetClaimsForUser(SelectBoxModel model)
        {
            PostUserOperationClaimModel claimModel = new PostUserOperationClaimModel
            {
                UserId = model.Id,
                UserLoginName = model.DisplayValue,
                OperationClaimIds = AutoMapperWrapper.Mapper.Map<List<FormItemStatusModel>>(_operationClaimDal.GetList())
            };
            
            var userClaims = _userOperationClaimDal.GetList(w => w.UserId == model.Id).ToList();

            foreach (var claim in claimModel.OperationClaimIds)
            {
                var uc = userClaims.FirstOrDefault(w => w.OperationClaimId == claim.Id);
                if(uc==null)
                    continue;

                claim.OldStatus = FormItemStatus.AddedBefore;
                claim.NewStatus = FormItemStatus.AddedBefore;
                claim.Id = uc.Id;
            }

            return new DataResponse
            {
                Data = claimModel,
                Success = true,
                Message = "Kullanıcı Rolleri Listesi",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}