using System;
using System.Net;
using BaseEntities.Response;
using Extensions.AutoMapperExtensions;
using Microsoft.EntityFrameworkCore;
using Module.Auth.Business.Abstract;
using Module.Auth.DataAccess.Abstract;
using Module.Auth.Entities;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.Business.Concrete
{
    public class UserValidationCodeManager : IUserValidationCodeService
    {
        private IUserValidationCodeDal _userValidationCodeDal;
        private string _tableName = "Dogrulama Kodu";
        public UserValidationCodeManager(IUserValidationCodeDal passwordValidationCodeDal)
        {
            _userValidationCodeDal = passwordValidationCodeDal;
        }


        public DataResponse Create(PostUserValidationCodeCreateModel model)
        {
            if (IsNotValidCode(model))
                return new DataResponse
                {
                    Success = false,
                    Message = $"{model.ExpiresDate} tarihine kadar yeni bir istekte bulunamazsınız",
                    StatusCode = HttpStatusCode.BadRequest
                };
            var ent = AutoMapperWrapper.Mapper.Map<UserValidationCode>(model);
            if (_userValidationCodeDal.SetState(ent, EntityState.Added))
                return new DataResponse
                {
                    Message = $"{_tableName} Oluşturuldu",
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = ent.Code
                };

            return new DataResponse
            {
                Success = false,
                Message = $"{_tableName} Oluşturulurken Hata Oluştu",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public DataResponse ValidateAndSetUsed(PostResetPasswordCodeModel model)
        {
            var ent = _userValidationCodeDal.Get(w => w.Code == model.Code && w.UserId == model.UserId && !w.IsUsed);
            if (ent == null)
                return new DataResponse
                {
                    Success = false,
                    Message = "Doğrulama Kodu Geçersiz",
                    StatusCode = HttpStatusCode.BadRequest
                };
            ent.IsUsed = true;
            ent.UpdateTime = DateTime.Now;
            if(_userValidationCodeDal.SetState(ent, EntityState.Modified))
                return new DataResponse
                {
                    Success = true,
                    Message = "Kod Doğrulandı. Şifreniz Değiştirildi",
                    StatusCode = HttpStatusCode.OK
                };

            return new DataResponse
            {
                Success = false,
                Message = "Doğrulama Kodu Geçersiz",
                StatusCode = HttpStatusCode.BadRequest
            };
        }


        private bool IsNotValidCode(PostUserValidationCodeCreateModel model)
        {
            return _userValidationCodeDal.Any(w =>
                w.UserId == model.UserId && w.ValidationCodeType == model.ValidationCodeType &&
                w.ExpiresDate > DateTime.Now);
        }

    }
}