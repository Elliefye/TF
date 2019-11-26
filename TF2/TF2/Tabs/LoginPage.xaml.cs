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
        }

        async void LoginBtnClicked(object sender, EventArgs e)
        {
            User match = EntityLoader.LogIn(UsernameText.Text, PasswordText.Text);

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