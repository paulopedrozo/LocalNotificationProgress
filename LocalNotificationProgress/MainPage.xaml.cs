using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalNotificationProgress
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            for (double i = 0; i < 101; i++)
            {
                await Task.Delay(100);
                ProgressBar.Progress= i/100;
                ProgressBar.ProgressColor = Color.Orange;
                Label.Text = i + "/100";
                DependencyService.Get<INotification>().CreateNotification("100","Titulo", Label.Text, Convert.ToInt32(i));
            }

            DependencyService.Get<INotification>().DeleteNotification();
        }
    }
}
