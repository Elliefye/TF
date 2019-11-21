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

            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts -- load all entities
            TempEntityLoader.LoadLecturers();
            TempEntityLoader.LoadSubjects();
            TempEntityLoader.LoadReviews();
            TempEntityLoader.lecturersSubjects = TempEntityLoader.groupJoinSubjectsAndLecturers();
            TempEntityLoader.LoadUsers();
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
