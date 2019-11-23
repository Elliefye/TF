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
		public LoginPage ()
		{
			InitializeComponent ();
		}

        async void LoginBtnClicked(object sender, EventArgs e)
        {
            //encrypt data first
            Encryption enc = new Encryption();
            EntityLoader.LogIn(enc.Encrypt(UsernameText.Text), enc.Encrypt(PasswordText.Text));

            if (ConstVars.currentUser == null)
            {
                await DisplayAlert("Incorrect data", "Login data incorrect. Please try again.", "OK");
            }
            else
            {
                App.Current.MainPage = new NavigationPage(new ViewAll());
            }
            /*User match = TempEntityLoader.users.FirstOrDefault(user => user.Email == UsernameText.Text
            && user.Password == PasswordText.Text);

            if (match != null)
            {
                ConstVars.AuthStatus = 1;
                App.Current.MainPage = new NavigationPage(new ViewAll());
            }
            else
            {
                await DisplayAlert("Incorrect data", "Login data incorrect. Please try again.", "OK");

            }*/
        }

        async void SignUpInsteadBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }
    }
}