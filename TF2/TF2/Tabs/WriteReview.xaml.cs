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
    public partial class WriteReview : ContentPage
    {
        public LecSub lecSub;
        public WriteReview(LecSub lecSub)
        {
            InitializeComponent();
            this.lecSub = lecSub;
            PopulateReviewForm();
        }

        private void PopulateReviewForm()
        {
            LecturerName.Text = lecSub.LecturerName;
            SubjectName.Text = lecSub.SubjectName;

            LecturerScore.Items.Add("1");
            LecturerScore.Items.Add("2");
            LecturerScore.Items.Add("3");
            LecturerScore.Items.Add("4");
            LecturerScore.Items.Add("5");
            SubjectScore.Items.Add("1");
            SubjectScore.Items.Add("2");
            SubjectScore.Items.Add("3");
            SubjectScore.Items.Add("4");
            SubjectScore.Items.Add("5");
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {
            Review review = new Review();
            review.LecturerId = lecSub.lecturerId;
            review.SubjectId = lecSub.subjectId;
            review.LecturerScore = Convert.ToInt32(LecturerScore.SelectedItem);
            review.SubjectScore = Convert.ToInt32(SubjectScore.SelectedItem);
            review.Comment = CommentEntry.Text;
            review.UserId = ConstVars.currentUser.Id;

            if(AnonCheckBox.IsChecked)
            {
                review.Anonymous = 1;
            }
            else
            {
                review.Anonymous = 0;
            }

            EntityLoader.AddReview(review);

            Application.Current.MainPage = new NavigationPage(new BottomNavigation());
        }

    }
}