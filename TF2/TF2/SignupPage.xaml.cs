using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TF2.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignupPage : ContentPage
	{
		public SignupPage ()
		{
			InitializeComponent ();
		}

        async void Signup_Btn_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(usernameText.Text) && !string.IsNullOrWhiteSpace(passwordText1.Text)
                && !string.IsNullOrWhiteSpace(passwordText2.Text) && !string.IsNullOrWhiteSpace(emailText.Text))
            {
                //check if passwords match
                if(passwordText1.Text != passwordText2.Text)
                {
                    await DisplayAlert("Incorrect password", "Passwords do not match. Please try again.", "OK");
                    await DisplayAlert("", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OK");

                    return;
                }

                //check if username valid (4-15 alphanumericals, must start with a letter)
                Regex username = new Regex("^[a-zA-Z][a-zA-Z0-9]{3,14}$");

                if (!username.IsMatch(usernameText.Text))
                {
                    await DisplayAlert("Incorrect username", "Username too short or contains special characters. Please try again.", "OK");
                    return;
                }

                //check if password long enough
                if (passwordText1.Text.Length < 6)
                {
                    await DisplayAlert("Incorrect password", "Password must be at least 6 characters long. Please try again.", "OK");
                    return;
                }

                //simple check if email is valid
                try
                {
                    MailAddress email = new MailAddress(emailText.Text);
                }
                catch
                {
                    await DisplayAlert("Incorrect email", "Email address invalid. Please try again.", "OK");
                    return;
                }

                User existingU = EntityLoader.LogIn(usernameText.Text, passwordText1.Text);
                User existingE = EntityLoader.LogIn(emailText.Text, passwordText1.Text);


                if (existingU != null || existingE != null)
                {
                    await DisplayAlert("Error", "This user already exists. Please try logging in instead.", "OK");
                    return;
                }
                else
                {
                    EntityLoader.SignUp((new User
                    {
                        Username = usernameText.Text,
                        Password = passwordText1.Text,
                        Email = emailText.Text,
                        Role = "Student" //default role is student
                    }));

                    await DisplayAlert("Success", "You were registered. Please log in now.", "OK");
                }

                usernameText.Text = "";
                passwordText1.Text = "";
                passwordText2.Text = "";
                emailText.Text = "";
            }
        }
    }
}