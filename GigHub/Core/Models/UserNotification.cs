using System;

namespace GigHub.Core.Models
{
    public class UserNotification
    {
        //it is for entity framework
        protected UserNotification()
        {
        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User");
            }

            User = user;
            Notification = notification;
        }

        public string UserId { get; private set; }

        public int NotificationId { get; set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }

        public void Read()
        {
            IsRead = true;
        }
    }
}