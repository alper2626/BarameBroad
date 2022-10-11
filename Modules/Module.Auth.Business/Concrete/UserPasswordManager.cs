using System;
using System.Linq;
using System.Net;
using BaseEntities.Response;
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Module.Auth.Business.Abstract;
using Module.Auth.DataAccess.Abstract;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.Business.Concrete
{
    public class UserPasswordManager : IUserPasswordService
    {
        private IUserPasswordDal _userPasswordDal;
        private readonly string tableName = "Şifre";

        public UserPasswordManager(IUserPasswordDal userPasswordDal)
        {
            _userPasswordDal = userPasswordDal;
        }

        public DataResponse Update(PostUserPasswordUpdateModel model)
        {
            var password = _userPasswordDal.Get(w =>
                w.UserId == model.UserId);
            if (password == null || !HashingHelper.VerifyPasswordHash(model.OldPassword, password.HashedPassword, password.PasswordSalt))
                return new DataResponse
                {
                    Success = false,
                    Message = $"Hatalı {tableName} Girdiniz",
                    StatusCode = HttpStatusCode.BadRequest
                };

            byte[] newPasswordHash, newPasswordSalt;
            HashingHelper.CreatePasswordHash(model.Password, out newPasswordHash, out newPasswordSalt);
            password.PasswordSalt = newPasswordSalt;
            password.HashedPassword = newPasswordHash;
            if (_userPasswordDal.SetState(password, EntityState.Modified))
            {
                return new DataResponse
                {
                    Success = true,
                    Message = $"{tableName} Güncellendi",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            return new DataResponse
            {
                Success = false,
                Message = $"{tableName} Düzenlenirken Hata Oluştu",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public DataResponse UpdateWithoutValidate(PostUserPasswordUpdateModel model)
        {
            var password = _userPasswordDal.Get(w => w.UserId == model.UserId);
            if (password == null)
                return new DataResponse
                {
                    Success = false,
                    Message = $"Hatalı {tableName} Girdiniz",
                    StatusCode = HttpStatusCode.BadRequest
                };
            byte[] newPasswordHash, newPasswordSalt;
            HashingHelper.CreatePasswordHash(model.Password, out newPasswordHash, out newPasswordSalt);
            password.PasswordSalt = newPasswordSalt;
            password.HashedPassword = newPasswordHash;
            if (_userPasswordDal.SetState(password, EntityState.Modified))
                return new DataResponse
                {
                    Success = true,
                    Message = $"{tableName} Güncellendi",
                    StatusCode = HttpStatusCode.BadRequest
                };
            return new DataResponse
            {
                Success = false,
                Message = $"{tableName} Düzenlenirken Hata Oluştu",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

    }
}