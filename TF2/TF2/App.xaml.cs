using System;
using TF2.Entities;
using TF2.Tabs;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TF2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            EntityLoader.ConnectToDatabase("teaching_feedback.db");
            EntityLoader.LoadLecturersAndSubjects();
            EntityLoader.LoadReviews();
            CheckForAuth();         
        }

        protected override void OnStart()
        {
            //EntityLoader.ConnectToDatabase("teaching_feedback.db");
            //EntityLoader.LoadLecturersAndSubjects();
            //EntityLoader.LoadReviews();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private async void CheckForAuth()
        {
            try
            {
                string token = await SecureStorage.GetAsync("uauth_token");

                if (token != null)
                {
                    ConstVars.AuthStatus = 1;
                    ConstVars.currentUser = EntityLoader.GetUserFromId(Int32.Parse(token));
                    MainPage = new NavigationPage(new BottomNavigation());
                }
                else
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
            }
            catch
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
