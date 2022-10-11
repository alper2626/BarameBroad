using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BaseEntities.EnumTypes;
using BaseEntities.Response;
using Core.DataAccess.Abstract;
using Core.LogModule.Jobs;
using Core.Security.Hashing;
using DevExtreme.AspNet.Mvc;
using Extensions.AutoMapperExtensions;
using Microsoft.EntityFrameworkCore;
using Module.Auth.Business.Abstract;
using Module.Auth.DataAccess.Abstract;
using Module.Auth.Entities;
using Module.Auth.Entities.GetAdmin;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private IQueryableRepositoryBase<User> _queryable;
        private readonly string tableName = "Kullanıcı";

        public UserManager(IUserDal userDal, IQueryableRepositoryBase<User> queryable)
        {
            _userDal = userDal;
            _queryable = queryable;
        }

        public DataResponse GridSource<T>(DataSourceLoadOptions loadOptions)
        where T : class, new()
        {
            return new DataResponse
            {
                Data = _queryable.DxGridList<T>(loadOptions, _queryable.Table),
                Success = true,
                Message = $"{tableName} Listesi",
                StatusCode = HttpStatusCode.OK
            };
        }

        public DataResponse UserEqualListGridSource<T>(DataSourceLoadOptions loadOptions, List<Guid> userIds) where T : class, new()
        {
            var table = _queryable.Table;
            var query = table
                .Where(w => userIds.Contains(w.Id));
            var data = _queryable.DxQueryableGridList<T>(loadOptions, query, table);
            return new DataResponse
            {
                Data = data,
                Success = true,
                Message = $"{tableName} Listesi",
                StatusCode = HttpStatusCode.OK
            };
        }

        public DataResponse UserNotEqualListGridSource<T>(DataSourceLoadOptions loadOptions, List<Guid> userIds) where T : class, new()
        {
            var table = _queryable.Table;
            var query = table
                .Where(w => !userIds.Contains(w.Id));
            var data = _queryable.DxQueryableGridList<T>(loadOptions, query,table);
            return new DataResponse
            {
                Data = data,
                Success = true,
                Message = $"{tableName} Listesi",
                StatusCode = HttpStatusCode.OK
            };
        }

        public DataResponse GetById<T>(Guid id)
        {
            var record = _userDal.Get(w => w.Id == id);
            if (record == null)
                return new DataResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"{tableName} Bulunamadı"
                };
            return new DataResponse
            {
                Data = AutoMapperWrapper.Mapper.Map<T>(record),
                Success = true,
                StatusCode = HttpStatusCode.BadRequest,
                Message = $"{tableName} Detayı"
            };


        }

        public DataResponse Create(PostUserCreateModel model)
        {
            var valRes = ValidateAccountInfo(model.LoginName, model.MailAddress, model.PhoneNumber);
            if (!valRes.Success)
                return valRes;

            var record = AutoMapperWrapper.Mapper.Map<User>(model);
            CreateClaimsForUser(model.ClaimIds, record);
            CreatePasswordForUser(model.PasswordModel, record);
            
            if (_userDal.CreateWithSubEntities(record))
            {

                return new DataResponse
                {
                    Data = record.Id,
                    Success = true,
                    Message = $"{tableName} Tablosuna Kayıt Yapıldı.",
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new DataResponse
            {
                Success = false,
                Message = $"{tableName} Tablosuna Kayıt Yapılırken Hata Oluştu.",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public DataResponse Update(PostUserUpdateModel model)
        {
            var valRes = ValidateAccountInfo(model.LoginName, model.MailAddress, model.PhoneNumber, model.UserId, true);
            if (!valRes.Success)
                return valRes;
            var record = _userDal.Get(w => w.Id == model.Id);
            if (record == null)
                return new DataResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"{tableName} Bulunamadı"
                };
            var social = record.SocialRef;

            record = AutoMapperWrapper.Mapper.Map<User>(model);
            record.SocialRef = social;

            if (_userDal.SetState(record, EntityState.Modified))
                return new DataResponse
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = $"{tableName} Düzenlendi",
                    Data = record.Id,
                };

            return new DataResponse
            {
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Message = $"{tableName} Düzenlenirken Hata Oluştu"
            };
        }

        public DataResponse Delete(Guid id)
        {
            var record = _userDal.Get(w => w.Id == id);
            if (record == null)
                return new DataResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = $"{tableName} Bulunamadı"
                };
            if (_userDal.SetState(record, EntityState.Deleted))
                return new DataResponse
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = $"{tableName} Silindi"
                };

            return new DataResponse
            {
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Message = $"{tableName} Silinirken Hata Oluştu"
            };
        }

        public DataResponse Login(PostLoginModel model)
        {
            var userToCheck =
                _queryable.Table
                .Include(w => w.UserPasswords)
                .Include(w => w.UserClaims)
                .ThenInclude(w => w.OperationClaim)
                .FirstOrDefault(u => u.LoginName == model.LoginName);

            if (userToCheck == null || !HashingHelper.VerifyPasswordHash(model.Password, userToCheck.UserPasswords.First().HashedPassword, userToCheck.UserPasswords.First().PasswordSalt))
            {
                return new DataResponse
                {
                    Success = false,
                    Message = "Kullanıcı Adı veya Şifre Hatalı",
                    StatusCode = HttpStatusCode.Unauthorized,
                };
            }

            var fullName = $"{userToCheck.FirstName} {userToCheck.LastName} ({userToCheck.LoginName})";
            LoggerJob.OperationLog(userToCheck, OperationType.Login, fullName);
            _queryable.Dispose(_queryable.Table);
            return new DataResponse
            {
                Success = true,
                Message = string.Format($"Hoşgeldin {model.LoginName}"),
                Data = AutoMapperWrapper.Mapper.Map<GetLoggedUserWithClaimModel>(userToCheck),
                StatusCode = HttpStatusCode.OK
            };
        }

        public DataResponse UserFromMailAddress(string mailAddress)
        {
            var user = _userDal.Get(w => w.MailAddress == mailAddress);
            if (user == null)
                return new DataResponse
                {
                    Success = false,
                    Message = "Kullanıcı Bulunamadı",
                    StatusCode = HttpStatusCode.BadRequest
                };
            return new DataResponse
            {
                Data = AutoMapperWrapper.Mapper.Map<GetUserModel>(user),
                Success = true,
                Message = "Kullanıcı Bilgileri",
                StatusCode = HttpStatusCode.OK
            };

        }

        public DataResponse UserFromSocialId(string socialId)
        {
            var userToCheck =
                _queryable.Table
                    .Include(w => w.UserPasswords)
                    .Include(w => w.UserClaims)
                    .ThenInclude(w => w.OperationClaim)
                    .FirstOrDefault(u => u.SocialRef == socialId);
            if (userToCheck == null)
                return new DataResponse
                {
                    Success = false,
                    Message = "Kullanıcı Adı veya Şifre Hatalı",
                    StatusCode = HttpStatusCode.Unauthorized,
                };

            var fullName = $"{userToCheck.FirstName} {userToCheck.LastName} ({userToCheck.LoginName})";
            LoggerJob.OperationLog(userToCheck, OperationType.Login, fullName);
            _queryable.Dispose(_queryable.Table);
            return new DataResponse
            {
                Success = true,
                Message = string.Format($"Hoşgeldin {userToCheck.LoginName}"),
                Data = AutoMapperWrapper.Mapper.Map<GetLoggedUserWithClaimModel>(userToCheck),
                StatusCode = HttpStatusCode.OK
            };
        }

        private void CreateClaimsForUser(List<Guid> ids, User user)
        {
            user.UserClaims = new List<UserOperationClaim>();
            if (ids == null)
                return;
            foreach (var id in ids)
                user.UserClaims.Add(new UserOperationClaim
                {
                    OperationClaimId = id
                });
        }

        private void CreatePasswordForUser(PostUserPasswordCreateModel passwordModel, User user)
        {
            user.UserPasswords = new List<UserPassword>();
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(passwordModel.Password, out passwordHash, out passwordSalt);
            user.UserPasswords.Add(new UserPassword
            {
                CreateTime = DateTime.Now,
                PasswordSalt = passwordSalt,
                HashedPassword = passwordHash
            });
        }

        private DataResponse ValidateAccountInfo(string loginName, string mailAddress, string phoneNumber, Guid? userId = null, bool updateReq = false)
        {

            if (IsExistLoginName(loginName, userId, updateReq))
                return new DataResponse
                {
                    Success = false,
                    Message = "Kullanıcı Adı Önceden Kayıt Edilmiş",
                    StatusCode = HttpStatusCode.Conflict,
                };

            if (IsExistPhoneNumber(phoneNumber, userId, updateReq))
                return new DataResponse
                {
                    Success = false,
                    Message = "Telefon Numarası Daha Önceden Kayıt Edilmiş",
                    StatusCode = HttpStatusCode.Conflict,
                };

            if (IsExistMailAddress(mailAddress, userId, updateReq))
                return new DataResponse
                {
                    Success = false,
                    Message = "Mail Adresi Daha Önceden Kayıt Edilmiş",
                    StatusCode = HttpStatusCode.Conflict,
                };

            return new DataResponse
            {
                Success = true,
                Message = "Eşleşme Yok.",
                StatusCode = HttpStatusCode.OK,
            };
        }

        private bool IsExistLoginName(string loginName, Guid? userId = null, bool updateReq = false)
        {
            if (updateReq)
                return _userDal.Any(q => q.LoginName == loginName && q.Id != userId);
            return _userDal.Any(q => q.LoginName == loginName);
        }

        private bool IsExistMailAddress(string mailAddress, Guid? userId = null, bool updateReq = false)
        {
            if (updateReq)
                return _userDal.Any(q => q.MailAddress == mailAddress && q.Id != userId);
            return _userDal.Any(q => q.MailAddress == mailAddress);
        }

        private bool IsExistPhoneNumber(string phoneNumber, Guid? userId = null, bool updateReq = false)
        {
            if (updateReq)
                return _userDal.Any(q => q.PhoneNumber == phoneNumber && q.Id != userId);
            return _userDal.Any(q => q.PhoneNumber == phoneNumber);
        }
    }
}