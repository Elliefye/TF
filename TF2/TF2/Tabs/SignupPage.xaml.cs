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
        public event EventHandler UpdatedData;

        private void OnUpdatedData()
        {
            UpdatedData?.Invoke(this, EventArgs.Empty);
        }

        public SignupPage()
        {
            InitializeComponent();

            UernameTextSignup.ReturnCommand = new Command(() => EmailTextSignup.Focus());
            EmailTextSignup.ReturnCommand = new Command(() => PasswordText1Signup.Focus());
            PasswordText1Signup.ReturnCommand = new Command(() => PasswordText2Signup.Focus());
        }

        public SignupPage(User existing)
        {
            InitializeComponent();

            UernameTextSignup.ReturnCommand = new Command(() => EmailTextSignup.Focus());
            EmailTextSignup.ReturnCommand = new Command(() => PasswordText1Signup.Focus());
            PasswordText1Signup.ReturnCommand = new Command(() => PasswordText2Signup.Focus());
            Title = "Update profile data";

            UernameTextSignup.Text = existing.Username;
            EmailTextSignup.Text = existing.Email;
            SignupBtn.Text = "Update data";

            SignupBtn.Clicked -= Signup_Btn_Clicked;
            SignupBtn.Clicked += async (s, e) =>
            {
                switch (EntryTests())
                {
                    case 1:
                        await DisplayAlert("Empty data", "Please fill all fields.", "OK");
                        return;
                    case 2:
                        await DisplayAlert("Incorrect password", "Passwords do not match. Please try again.", "OK");
                        return;
                    case 3:
                        await DisplayAlert("Incorrect username", "Username too short or contains special characters. Please try again.", "OK");
                        return;
                    case 4:
                        await DisplayAlert("Incorrect password", "Password must be at least 6 characters long. Please try again.", "OK");
                        return;
                    case 5:
                        await DisplayAlert("Incorrect email", "Email address invalid. Please try again.", "OK");
                        return;
                    case 0: break;
                    default:
                        await DisplayAlert("Something went wrong", "An unknown error occured.", "OK");
                        return;
                }

                if (EntityLoader.LookForExistingUser(EmailTextSignup.Text, UernameTextSignup.Text))
                {
                    await DisplayAlert("Error", "A user with this email or username already exists.", "OK");
                    return;
                }
                else
                {
                    Encryption enc = new Encryption();
                    string prevRole = ConstVars.currentUser.Role;
                    int prevId = ConstVars.currentUser.Id;

                    ConstVars.currentUser = new User
                    {
                        Id = prevId,
                        Username = UernameTextSignup.Text,
                        Password = enc.Encrypt(PasswordText1Signup.Text),
                        Email = EmailTextSignup.Text,
                        Role = prevRole
                    };
                    EntityLoader.UpdateUserData();

                    await DisplayAlert("Success", "Data change sucessful.", "OK");
                    OnUpdatedData();
                    await Navigation.PopAsync();                   
                }
            };
        }

        async void Signup_Btn_Clicked(object sender, EventArgs e)
        {
            if(ConstVars.AuthStatus != 0)
            {
                return;
            }

            switch (EntryTests())
            {
                case 1:
                    await DisplayAlert("Empty data", "Please fill all fields.", "OK");
                    return;
                case 2:
                    await DisplayAlert("Incorrect password", "Passwords do not match. Please try again.", "OK");
                    return;
                case 3:
                    await DisplayAlert("Incorrect username", "Username too short or contains special characters. Please try again.", "OK");
                    return;
                case 4:
                    await DisplayAlert("Incorrect password", "Password must be at least 6 characters long. Please try again.", "OK");
                    return;
                case 5:
                    await DisplayAlert("Incorrect email", "Email address invalid. Please try again.", "OK");
                    return;
                case 0: break;
                default:
                    await DisplayAlert("Something went wrong", "An unknown error occured.", "OK");
                    return;
            }

            if (EntityLoader.LookForExistingUser(UernameTextSignup.Text) || EntityLoader.LookForExistingUser(EmailTextSignup.Text))
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

        private int EntryTests()
        {
            if (!string.IsNullOrWhiteSpace(UernameTextSignup.Text) && !string.IsNullOrWhiteSpace(PasswordText1Signup.Text)
                && !string.IsNullOrWhiteSpace(PasswordText2Signup.Text) && !string.IsNullOrWhiteSpace(EmailTextSignup.Text))
            {
                //check if passwords match
                if (PasswordText1Signup.Text != PasswordText2Signup.Text)
                {
                    return 2;
                }

                //check if username valid (4-15 alphanumericals, must start with a letter)
                Regex username = new Regex("^[a-zA-Z][a-zA-Z0-9]{3,14}$");

                if (!username.IsMatch(UernameTextSignup.Text))
                {
                    return 3;
                }

                //check if password long enough
                if (PasswordText1Signup.Text.Length < 6)
                {
                    return 4;
                }

                //simple check if email is valid
                try
                {
                    MailAddress email = new MailAddress(EmailTextSignup.Text);
                }
                catch
                {
                    return 5;
                }

                return 0;
            }

            return 1;
        } 
    }
}