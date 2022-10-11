using Microsoft.AspNetCore.Mvc;
using BaseEntities.Concrete;
using BaseEntities.EnumTypes;
using Core.Attributes;
using Module.Auth.Business.Abstract;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.UI.Areas.Auth.Controllers
{
    [Area("Auth")]
    [InRole(UserRole.Admin)]
    public class OperationClaimController : Controller
    {
        private IOperationClaimService _operationClaimService;
        private IUserOperationClaimService _userOperationClaimService;

        public OperationClaimController(IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService)
        {
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
        }

        public IActionResult OperationClaimSelectBoxForCreateUser()
        {
            var response = _operationClaimService.GetClaims();
            if (response.Success)
                return PartialView("Partials/_OperationClaimSelectBoxForCreateUser", response.Data);

            return PartialView("Partials/_ErrorPartial", response);
        }

        public IActionResult UpdateUserOperationClaimModal(SelectBoxModel model)
        {
            var response = _operationClaimService.GetClaimsForUser(model);
            if (response.Success)
                return PartialView("Partials/_UpdateUserOperationClaimModal", response.Data);

            return PartialView("Partials/_ErrorPartial", response);
        }

        [HttpPost]
        public IActionResult UpdateClaims(PostUserOperationClaimModel model)
        {
            return Json(_userOperationClaimService.SetUserOperationClaims(model));
        }
    }
}
