using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (UsernameText.Text == "admin" && PasswordText.Text == "admin")
            {
                //await Navigation.PushAsync(new ViewAll());
                App.Current.MainPage = new NavigationPage(new ViewAll());
            }
            else
            {
                await DisplayAlert("Incorrect data", "Login data incorrect. Please try again.", "OK");
            }
        }
    }
}