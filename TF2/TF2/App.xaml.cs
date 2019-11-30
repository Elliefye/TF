using System;
using TF2.Entities;
using TF2.Tabs;
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
           MainPage = new NavigationPage(new LoginPage());

            /*ConstVars.currentUser = new User()
            {
                Id = 2,
                Username = "Ellie",
                Email = "ellie@mail.com"                
            };
            ConstVars.AuthStatus = 1;
            MainPage = new NavigationPage(new BottomNavigation()); */          
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
    }
}
