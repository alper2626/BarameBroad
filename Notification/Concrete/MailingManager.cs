using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.Extensions.Configuration;
using Notification.Abstract;
using Notification.Entities.Concrete;
using Notification.Helpers;
namespace Notification.Concrete
{
    public class MailingManager : IMailingService
    {

        private readonly MailOptions _options;
        private IMailTemplateHelper _mailTemplateHelper;
        public MailingManager(IMailTemplateHelper mailTemplateHelper)
        {
            _mailTemplateHelper = mailTemplateHelper;
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true);

            var configuration = config.Build();
            _options = configuration.GetSection("MailOptions").Get<MailOptions>();
        }

        public async Task Send(SendMailBaseModel model)
        {
            if (model.IsContactMail)
                model.To = new HashSet<string> { _options.SiteReceiver };


            if (model.To.IsNullOrEmpty())
                return;

            System.Net.NetworkCredential cred = new System.Net.NetworkCredential(_options.SiteSender, _options.Password);

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage
            {
                From = new System.Net.Mail.MailAddress(_options.SiteSender, _options.DisplayName)
            };
            foreach (var mailAddress in model.To)
                mail.To.Add(mailAddress);

            mail.Subject = model.Subject;

            mail.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(_options.SmtpClient, 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = false,
                Credentials = cred
            };
            
            string path = Path.Combine("MailTemplates", model.TemplateName);
            mail.Body = await _mailTemplateHelper
                .GetTemplateHtmlAsStringAsync(
                    path, model.MailModel);

            smtp.Send(mail);
        }
    }
}