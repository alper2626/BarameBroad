using System;
using System.Linq;
using System.Net;
using BaseEntities.EnumTypes;
using BaseEntities.Response;
using Core.Core;
using Core.DataAccess.Concrete;
using Core.LogModule.Jobs;
using Microsoft.EntityFrameworkCore;
using Module.Auth.DataAccess.Abstract;
using Module.Auth.Entities;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.DataAccess.Concrete
{
    public class UserOperationClaimDal : EntityRepositoryBase<UserOperationClaim>, IUserOperationClaimDal
    {
        public DataResponse SetUserOperationClaims(PostUserOperationClaimModel model)
        {
            var response = new DataResponse
            {
                Success = true,
                Message = "Kullanıcı Rolleri Güncellendi",
                StatusCode = HttpStatusCode.OK
            };
            if (model.OperationClaimIds == null)
                return response;
            try
            {
                using (var context = new CustomDbContext())
                {
                    var u = context.Set<User>().FirstOrDefault(w => w.Id == model.UserId);
                    if (u == null)
                    {
                        response.Success = false;
                        response.Message = "Kullanıcı Bulunamadı";
                        return response;
                    }
                    var c = context.Set<UserOperationClaim>();
                    foreach (var claim in model.OperationClaimIds)
                    {
                        if (claim.NewStatus == FormItemStatus.Created)
                        {
                            var e = new UserOperationClaim
                            {
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                OperationClaimId = claim.Id,
                                UserId = model.UserId
                            };
                            c.Add(e);
                        }
                        else if (claim.NewStatus == FormItemStatus.Deleted)
                        {
                            c.Remove(new UserOperationClaim { Id = claim.Id });
                        }
                    }

                    context.SaveChanges();
                }
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message = "Roller Düzenlenirken Hata Oluştu";
                return response;
            }
        }
    }
}