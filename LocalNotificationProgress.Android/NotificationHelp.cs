using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using LocalNotificationProgress.Droid;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationHelper))]
namespace LocalNotificationProgress.Droid
{
    class NotificationHelper : INotification
    {
        private Context mContext;
        private NotificationManager notificationManager;
        private NotificationCompat.Builder mBuilder;

        public NotificationHelper()
        {
            mContext = global::Android.App.Application.Context;
        }
        public void CreateNotification(string ChannelID, string title, string message, int progress)
        {
            try
            {
                var intent = new Intent(mContext, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                intent.PutExtra(title, message);
                var pendingIntent = PendingIntent.GetActivity(mContext, 0, intent, PendingIntentFlags.OneShot);

                //var sound = global::Android.Net.Uri.Parse(ContentResolver.SchemeAndroidResource + "://" + mContext.PackageName + "/" + Resource.Raw.notification);
                // Creating an Audio Attribute
                var alarmAttributes = new AudioAttributes.Builder()
                    .SetContentType(AudioContentType.Sonification)
                    .SetUsage(AudioUsageKind.Notification).Build();

                mBuilder = new NotificationCompat.Builder(mContext, ChannelID);
                mBuilder.SetSmallIcon(Resource.Drawable.NotifyIcon);
                mBuilder.SetContentTitle(title)
//                        .SetSound(sound)
                        .SetAutoCancel(true)
                        .SetProgress(100, progress, false)
                        .SetContentTitle(title)
                        .SetContentText(message)
                        .SetPriority((int)NotificationPriority.Low)
//                        .SetVibrate(new long[0])
//                        .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
                        .SetVisibility((int)NotificationVisibility.Public)
                        .SetSmallIcon(Resource.Drawable.NotifyIcon)
                        .SetContentIntent(pendingIntent);

                notificationManager = mContext.GetSystemService(Context.NotificationService) as NotificationManager;

                if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
                {
                    NotificationImportance importance = NotificationImportance.Low;

                    NotificationChannel notificationChannel = new NotificationChannel(ChannelID, title, importance);
                    notificationChannel.EnableLights(true);
//                    notificationChannel.EnableVibration(true);
//                    notificationChannel.SetSound(sound, alarmAttributes);
                    notificationChannel.SetShowBadge(true);
                    notificationChannel.Importance = importance;
//                    notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

                    if (notificationManager != null)
                    {
                        mBuilder.SetChannelId(ChannelID);
                        notificationManager.CreateNotificationChannel(notificationChannel);
                    }
                }

                notificationManager.Notify(0, mBuilder.Build());
            }
            catch (Exception ex)
            {
                //
            }
        }
        public void DeleteNotification()
        {
            notificationManager.Cancel(0);
        }
    }
}