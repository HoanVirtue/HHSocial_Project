using Clone_Main_Project_0710.Constant;
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
            Guid userId = Guid.Parse(Request.Cookies[UsersCookiesConstant.CookieUserId]); 
            List<Notification> notifications = await _notifiContext.GetNotificationsByUserId(userId);

            NotificationView view = new NotificationView()
            {
                NotificationList = notifications,
                TotalNotifi = notifications.Count
            };

            return View(view);
        }
    }
}