using Clone_Main_Project_0710.Constant;
using Clone_Main_Project_0710.DataCookies;
using Clone_Main_Project_0710.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clone_Main_Project_0710.Components
{
    public class Notifications : ViewComponent
    {
        private NotifitcationRepository _notifiContext;
        public Notifications(SocialContext context)
        {
            _notifiContext = new NotifitcationRepository(context);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Guid userId = UsersCookies.GetUserCookie().UserId; 
            List<Notification> notifications = await _notifiContext.GetNotificationsByUserId(userId);
            int countUnReadNotifi = notifications.Where(n => n.Read == false).ToList().Count();

            NotificationView view = new NotificationView()
            {
                NotificationList = notifications,
                TotalNotifi = notifications.Count,
                CountUnReadNotification = countUnReadNotifi
            };

            return View(view);
        }
    }
}