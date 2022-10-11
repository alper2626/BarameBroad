using BaseEntities.Response;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.Business.Abstract
{
    public interface IUserPasswordService
    {
        DataResponse Update(PostUserPasswordUpdateModel model);

        DataResponse UpdateWithoutValidate(PostUserPasswordUpdateModel model);
    }
}