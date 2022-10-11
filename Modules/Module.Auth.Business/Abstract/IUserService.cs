using System;
using System.Collections.Generic;
using BaseEntities.Response;
using DevExtreme.AspNet.Mvc;
using Module.Auth.Entities;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.Business.Abstract
{
    public interface IUserService
    {
        DataResponse GridSource<T>(DataSourceLoadOptions loadOptions)
            where T : class, new();

        DataResponse UserEqualListGridSource<T>(DataSourceLoadOptions loadOptions, List<Guid> userIds) where T : class, new();

        DataResponse UserNotEqualListGridSource<T>(DataSourceLoadOptions loadOptions, List<Guid> userIds) where T : class, new();


        DataResponse GetById<T>(Guid id);

        DataResponse Create(PostUserCreateModel model);

        DataResponse Update(PostUserUpdateModel model);

        DataResponse Delete(Guid id);

        DataResponse Login(PostLoginModel model);

        DataResponse UserFromMailAddress(string mailAddress);

        DataResponse UserFromSocialId(string socialId);

    }
}