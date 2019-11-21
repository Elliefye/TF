using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TF2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Login_Btn_Clicked(object sender, EventArgs e)
        {
            if(UsernameText.Text == "Admin" && PasswordText.Text == "admin")
            {
                await Navigation.PushAsync(new ViewAll());
            }
            else
            {
                await DisplayAlert("Incorrect data", "Login data incorrect. Please try again.", "OK");
            }
        }
    }
}
