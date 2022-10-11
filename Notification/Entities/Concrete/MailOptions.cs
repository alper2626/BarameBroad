namespace Notification.Entities.Concrete
{
    public class MailOptions
    {
        public string SiteReceiver { get; set; }

        public string SiteSender { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public string SmtpClient { get; set; }
    } 
}