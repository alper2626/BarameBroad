using System.Threading.Tasks;
using BaseEntities.Abstract;

namespace Notification.Helpers
{
    public interface IMailTemplateHelper
    {
        Task<string> GetTemplateHtmlAsStringAsync(
            string viewName, IPostModel model);
    }
}