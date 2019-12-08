using System;
using System.Collections.Generic;
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
            SetColorMode();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            SetColorMode();
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
                    string mode = await SecureStorage.GetAsync("mode");
                    
                    if(mode == "0")
                    {
                        ConstVars.DarkMode = false;
                    }
                    else if(mode == "1")
                    {
                        ConstVars.DarkMode = true;
                    }

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

        private void SetColorMode()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                if (ConstVars.DarkMode)
                {
                    mergedDictionaries.Add(new DarkTheme());
                }
                else
                {
                    mergedDictionaries.Add(new LightTheme());
                }
            }
        }
    }
}
