using LocalNotificationProgress.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationHelper))]
namespace LocalNotificationProgress.iOS
{
    class NotificationHelper : INotification
    {
        public void CreateNotification(string ChannelID, string title, string message, int progress)
        {
            new NotificationDelegate().RegisterNotification(title, message);
        }
        public void DeleteNotification()
        {
            //TODO Remover a notificação
        }
    }
}