using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

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
                await DisplayAlert("Error", "Please enter your credentials.", "OK");
                return;
            }

            User match = EntityLoader.LogIn(UsernameTextLogin.Text, PasswordTextLogin.Text);

            if (match == null)
            {
                await DisplayAlert("Error", "Login credentials incorrect. Please try again.", "OK");
            }
            else
            {
                ConstVars.currentUser = match;
                ConstVars.AuthStatus = 1;
                await SecureStorage.SetAsync("uauth_token", ConstVars.currentUser.Id.ToString());
                App.Current.MainPage = new BottomNavigation();
            }
        }

        async void SignUpInsteadBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }
    }
}