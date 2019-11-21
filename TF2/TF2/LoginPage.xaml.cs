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

        async void Login_Btn_Clicked(object sender, EventArgs e)
        {
            User match = TempEntityLoader.users.FirstOrDefault(user => user.Email == UsernameText.Text
            && user.Password == PasswordText.Text);

            if (match != null)
            {
                ConstVars.AuthStatus = 1;
                App.Current.MainPage = new NavigationPage(new ViewAll());
            }
            else
            {
                await DisplayAlert("Incorrect data", "Login data incorrect. Please try again.", "OK");

            }
        }
    }
}