using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinansApp.OneSignal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FinansApp.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly IConfiguration _configuration;

        public NotificationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(CreateNotificationModel request)
        {
            Guid appId = Guid.Parse(_configuration.GetSection(AppSettingKey.OneSignalAppId).Value);
            string restKey = _configuration.GetSection(AppSettingKey.OneSignalRestKey).Value;
            string result = await OneSignalHelper.OneSignalPushNotification(request, appId, restKey);
            return RedirectToAction("Index", "Notifications");
        }
    }
}