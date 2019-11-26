using System;
using TF2.Entities;
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
            //MainPage = new NavigationPage(new BottomNavigation());
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            //EntityLoader.ConnectToDatabase("teaching_feedback.db");
            //EntityLoader.LoadLecturersAndSubjects();
            //EntityLoader.LoadReviews();
            // Handle when your app starts -- load all entities
            /*TempEntityLoader.LoadLecturers();
            TempEntityLoader.LoadSubjects();
            TempEntityLoader.LoadReviews();
            TempEntityLoader.lecturersSubjects = TempEntityLoader.groupJoinSubjectsAndLecturers();
            TempEntityLoader.LoadUsers();*/
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
