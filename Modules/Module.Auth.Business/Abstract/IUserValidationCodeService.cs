
using System;
using BaseEntities.Response;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.Business.Abstract
{
    public interface IUserValidationCodeService
    {
        DataResponse Create(PostUserValidationCodeCreateModel model);

        DataResponse ValidateAndSetUsed(PostResetPasswordCodeModel model);
    }
}