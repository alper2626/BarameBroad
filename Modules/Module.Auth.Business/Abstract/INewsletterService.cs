using BaseEntities.Response;
using Module.Auth.Entities;

namespace Module.Auth.Business.Abstract
{
    public interface INewsletterService
    {
        DataResponse AllNewsletters();

        DataResponse RegisteredNewsletters();

        DataResponse NotRegisteredNewsletters();

        DataResponse GetByMail(string mailAddress);

        DataResponse UpdateNewsletters(Newsletter newsletter);

        DataResponse CreateNewsletters(Newsletter newsletter);

        DataResponse DeleteNewsletters(string mailAddress);
    }
}