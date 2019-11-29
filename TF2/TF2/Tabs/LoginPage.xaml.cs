using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            //focus on another field when done typing
            UsernameTextLogin.ReturnCommand = new Command(() => PasswordTextLogin.Focus());
        }

        async void LoginBtnClicked(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(UsernameTextLogin.Text) || String.IsNullOrWhiteSpace(PasswordTextLogin.Text))
            {
                await DisplayAlert("Incorrect data", "Please enter login data.", "OK");
                return;
            }

            User match = EntityLoader.LogIn(UsernameTextLogin.Text, PasswordTextLogin.Text);

            if (match == null)
            {
                await DisplayAlert("Incorrect data", "Login data incorrect. Please try again.", "OK");
            }
            else
            {
                ConstVars.currentUser = match;
                ConstVars.AuthStatus = 1;
                App.Current.MainPage = new NavigationPage(new BottomNavigation());
            }
        }

        async void SignUpInsteadBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }
    }
}