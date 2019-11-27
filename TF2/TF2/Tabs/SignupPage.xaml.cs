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
        public SignupPage()
        {
            InitializeComponent();

            UernameTextSignup.ReturnCommand = new Command(() => EmailTextSignup.Focus());
            EmailTextSignup.ReturnCommand = new Command(() => PasswordText1Signup.Focus());
            PasswordText1Signup.ReturnCommand = new Command(() => PasswordText2Signup.Focus());
        }

        async void Signup_Btn_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UernameTextSignup.Text) && !string.IsNullOrWhiteSpace(PasswordText1Signup.Text)
                && !string.IsNullOrWhiteSpace(PasswordText2Signup.Text) && !string.IsNullOrWhiteSpace(EmailTextSignup.Text))
            {
                //check if passwords match
                if (PasswordText1Signup.Text != PasswordText2Signup.Text)
                {
                    await DisplayAlert("Incorrect password", "Passwords do not match. Please try again.", "OK");
                    await DisplayAlert("", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OK");

                    return;
                }

                //check if username valid (4-15 alphanumericals, must start with a letter)
                Regex username = new Regex("^[a-zA-Z][a-zA-Z0-9]{3,14}$");

                if (!username.IsMatch(UernameTextSignup.Text))
                {
                    await DisplayAlert("Incorrect username", "Username too short or contains special characters. Please try again.", "OK");
                    return;
                }

                //check if password long enough
                if (PasswordText1Signup.Text.Length < 6)
                {
                    await DisplayAlert("Incorrect password", "Password must be at least 6 characters long. Please try again.", "OK");
                    return;
                }

                //simple check if email is valid
                try
                {
                    MailAddress email = new MailAddress(EmailTextSignup.Text);
                }
                catch
                {
                    await DisplayAlert("Incorrect email", "Email address invalid. Please try again.", "OK");
                    return;
                }

                User existingU = EntityLoader.LogIn(UernameTextSignup.Text, PasswordText1Signup.Text);
                User existingE = EntityLoader.LogIn(EmailTextSignup.Text, PasswordText1Signup.Text);


                if (existingU != null || existingE != null)
                {
                    await DisplayAlert("Error", "This user already exists. Please try logging in instead.", "OK");
                    return;
                }
                else
                {
                    EntityLoader.SignUp((new User
                    {
                        Username = UernameTextSignup.Text,
                        Password = PasswordText1Signup.Text,
                        Email = EmailTextSignup.Text,
                        Role = "Student" //default role is student
                    }));

                    await DisplayAlert("Success", "You were registered. Please log in now.", "OK");
                    await Navigation.PopAsync();
                }

                UernameTextSignup.Text = "";
                PasswordText1Signup.Text = "";
                PasswordText2Signup.Text = "";
                EmailTextSignup.Text = "";
            }
        }
    }
}