using System;
using System.Threading.Tasks;
using OneSignal.RestAPIv3.Client;
using OneSignal.RestAPIv3.Client.Resources;
using OneSignal.RestAPIv3.Client.Resources.Notifications;

namespace FinansApp.OneSignal
{
    public class OneSignalHelper
    {

        public static async Task<string> OneSignalPushNotification(CreateNotificationModel request, Guid appId, string restKey)
        {
            var client = new OneSignalClient(restKey);
            var opt = new NotificationCreateOptions()
            {
                AppId = appId,
                IncludedSegments = new string[] { "Subscribed Users" }
            };
            opt.Headings.Add(LanguageCodes.English, request.Title);
            opt.Contents.Add(LanguageCodes.English, request.Content);

            try
            {
                NotificationCreateResult result = await client.Notifications.CreateAsync(opt);
                return result.Id;
            }

            catch (Exception ex)
            {
                throw;
            }


        }
    }
}
