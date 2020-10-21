namespace LocalNotificationProgress
{
    public interface INotification
    {
        void CreateNotification(string ChannelID, string title, string message, int progress);
        void DeleteNotification();
    }
}
