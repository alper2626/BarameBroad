using System.Threading.Tasks;
using Notification.Entities.Concrete;

namespace Notification.Abstract
{
    public interface IMailingService
    {
        Task Send(SendMailBaseModel model);
    }
}