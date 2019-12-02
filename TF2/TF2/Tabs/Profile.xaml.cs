using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Entities;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            EmailLabel.Text = ConstVars.currentUser.Email;
            UsernameLabel.Text = ConstVars.currentUser.Username;

            List<LecturerReview> lectList = EntityLoader.GetUserReviewsL();
            List<SubjectReview> subList = EntityLoader.GetUserReviewsS();

            if (lectList.Count == 0 && subList.Count == 0)
            {
                MyReviewsBtn.IsEnabled = false;
            }
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            ConstVars.currentUser = null;
            ConstVars.AuthStatus = 0;
            SecureStorage.Remove("uauth_token");
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        async void ChangeAccount_Clicked(object sender, EventArgs e)
        {
            var SignupPage = new SignupPage(ConstVars.currentUser);
            SignupPage.UpdatedData += UpdateLabels;
            await Navigation.PushAsync(SignupPage);
        }

        private void UpdateLabels(object sender, EventArgs e)
        {
            EmailLabel.Text = ConstVars.currentUser.Email;
            UsernameLabel.Text = ConstVars.currentUser.Username;
        }

        async void MyReviews_Clicked(object sender, EventArgs e)
        {          
            await Navigation.PushAsync(new Reviews(EntityLoader.GetUserReviewsS(), EntityLoader.GetUserReviewsL()));
        }
    }
}