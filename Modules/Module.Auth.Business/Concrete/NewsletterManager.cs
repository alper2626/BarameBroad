using System.Net;
using BaseEntities.Response;
using Microsoft.EntityFrameworkCore;
using Module.Auth.Business.Abstract;
using Module.Auth.DataAccess.Abstract;
using Module.Auth.Entities;

namespace Module.Auth.Business.Concrete
{
    public class NewsletterManager : INewsletterService
    {
        private INewsletterDal _newsletterDal;

        public NewsletterManager(INewsletterDal newsletterDal)
        {
            _newsletterDal = newsletterDal;
        }

        public DataResponse AllNewsletters()
        {
            return new DataResponse
            {
                Message = "Tüm Mail Adresleri",
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Data = _newsletterDal.GetList(),
            };
        }

        public DataResponse RegisteredNewsletters()
        {
            return new DataResponse
            {
                Message = "Musteri Adresleri",
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Data = _newsletterDal.GetList(s => s.Registered),
            };
        }

        public DataResponse NotRegisteredNewsletters()
        {
            return new DataResponse
            {
                Message = "Takipçi Adresleri",
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Data = _newsletterDal.GetList(s => !s.Registered),
            };
        }

        public DataResponse GetByMail(string mailAddress)
        {
            var mail = _newsletterDal.Get(s => s.MailAddress == mailAddress);
            if (mail == null)
                return new DataResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Kayıt Bulunamadı",
                };
            return new DataResponse
            {
                Data = mail,
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Abonelik Bilgisi",
            };

        }

        public DataResponse UpdateNewsletters(Newsletter newsletter)
        {
            if (_newsletterDal.SetState(newsletter, EntityState.Modified))
                return new DataResponse
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Mail Güncellendi",
                };

            return new DataResponse
            {
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Mail Güncellenirken Hata Oluştu",
            };
        }

        public DataResponse CreateNewsletters(Newsletter newsletter)
        {
            if (_newsletterDal.Any(s => s.MailAddress == newsletter.MailAddress))
                return new DataResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Mail Daha Önce Eklenmiş",
                };

            if (_newsletterDal.SetState(newsletter, EntityState.Added))
                return new DataResponse
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Mail Eklendi",
                };

            return new DataResponse
            {
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Mail Eklenirken Hata Oluştu",
            };
        }

        public DataResponse DeleteNewsletters(string mailAddress)
        {
            var mail = _newsletterDal.Get(s => s.MailAddress == mailAddress);
            if (mail == null)
                return new DataResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Kayıt Bulunamadı",
                };

            if (_newsletterDal.SetState(mail, EntityState.Deleted))
                return new DataResponse
                {
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Mail Silindi",
                };

            return new DataResponse
            {
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Mail Silinirken Hata Oluştu",
            };
        }
    }
}