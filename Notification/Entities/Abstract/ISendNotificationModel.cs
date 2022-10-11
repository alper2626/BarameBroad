using System.Collections.Generic;

namespace Notification.Entities.Abstract
{
    public interface ISendNotificationModel
    {
        public HashSet<string> To { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public bool IsContactMail { get; set; }
    }
}