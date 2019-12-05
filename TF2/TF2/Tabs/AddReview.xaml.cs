using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF2.Tabs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddReview : ContentPage
	{
        private bool subrev = true;
        private Lecturer l;
        private Subject s;

        public event EventHandler AddedReview;

        private void OnAddReview()
        {
            AddedReview?.Invoke(this, EventArgs.Empty);
        }

        public AddReview ()
		{
			InitializeComponent ();
		}

        public AddReview (Lecturer lecturer)
        {
            InitializeComponent();

            var AnonLabelTapGestureRecognizer = new TapGestureRecognizer();
            AnonLabelTapGestureRecognizer.Tapped += (s, e) =>
            {
                AnonCheckBox.IsChecked = !AnonCheckBox.IsChecked;
            };
            AnonLabel.GestureRecognizers.Add(AnonLabelTapGestureRecognizer);

            TopTextLabel.Text += lecturer.FirstName + " " + lecturer.LastName;
            LectOrSubPicker.Title = "(Optional) pick the subject";

            foreach(Subject subject in lecturer.Subjects.Value)
            {
                LectOrSubPicker.Items.Add(subject.SubjectName);
            }

            subrev = false;
            l = lecturer;
        }

        public AddReview(Subject subject)
        {
            InitializeComponent();

            var AnonLabelTapGestureRecognizer = new TapGestureRecognizer();
            AnonLabelTapGestureRecognizer.Tapped += (s, e) =>
            {
                AnonCheckBox.IsChecked = !AnonCheckBox.IsChecked;
            };
            AnonLabel.GestureRecognizers.Add(AnonLabelTapGestureRecognizer);

            TopTextLabel.Text += subject.SubjectName;
            LectOrSubPicker.Title = "(Optional) pick the lecturer";

            foreach (Lecturer lecturer in subject.Lecturers.Value)
            {
                LectOrSubPicker.Items.Add(lecturer.FirstName + " " + lecturer.LastName);
            }

            s = subject;
        }

        public AddReview(Subject subject, SubjectReview review)
        {
            InitializeComponent();

            var AnonLabelTapGestureRecognizer = new TapGestureRecognizer();
            AnonLabelTapGestureRecognizer.Tapped += (s, e) =>
            {
                AnonCheckBox.IsChecked = !AnonCheckBox.IsChecked;
            };
            AnonLabel.GestureRecognizers.Add(AnonLabelTapGestureRecognizer);

            TopTextLabel.Text = "Editing your review for: " + subject.SubjectName;

            if(review.LecturerId != 0)
            {
                int index = 0;

                foreach (Lecturer lecturer in subject.Lecturers.Value)
                {
                    LectOrSubPicker.Items.Add(lecturer.FirstName + " " + lecturer.LastName);

                    if(lecturer.Id == review.LecturerId)
                    {
                        LectOrSubPicker.SelectedIndex = index;
                    }

                    index++;
                }
            }
            else
            {
                LectOrSubPicker.Title = "(Optional) pick the lecturer";

                foreach (Lecturer lecturer in subject.Lecturers.Value)
                {
                    LectOrSubPicker.Items.Add(lecturer.FirstName + " " + lecturer.LastName);
                }
            }

            ScorePicker.SelectedIndex = review.Rating - 1;
            CommentEntry.Text = review.Comment;

            if(review.Anonymous == 1)
            {
                AnonCheckBox.IsChecked = true;
            }
            
            s = subject;
            Submit.Clicked -= Submit_Clicked;
            Submit.Clicked += SubmitEdit_Clicked;
        }

        public AddReview(Lecturer lecturer, LecturerReview review)
        {
            InitializeComponent();

            var AnonLabelTapGestureRecognizer = new TapGestureRecognizer();
            AnonLabelTapGestureRecognizer.Tapped += (s, e) =>
            {
                AnonCheckBox.IsChecked = !AnonCheckBox.IsChecked;
            };
            AnonLabel.GestureRecognizers.Add(AnonLabelTapGestureRecognizer);

            TopTextLabel.Text = "Editing your review for: " + lecturer.FirstName + " " + lecturer.LastName;

            if (review.SubjectId != 0)
            {
                int index = 0;

                foreach (Subject subject in lecturer.Subjects.Value)
                {
                    LectOrSubPicker.Items.Add(subject.SubjectName);

                    if (subject.Id == review.SubjectId)
                    {
                        LectOrSubPicker.SelectedIndex = index;
                    }

                    index++;
                }
            }
            else
            {
                LectOrSubPicker.Title = "(Optional) pick the subject";

                foreach (Subject subject in lecturer.Subjects.Value)
                {
                    LectOrSubPicker.Items.Add(subject.SubjectName);
                }
            }

            ScorePicker.SelectedIndex = review.Rating - 1;
            CommentEntry.Text = review.Comment;

            if (review.Anonymous == 1)
            {
                AnonCheckBox.IsChecked = true;
            }

            l = lecturer;
            subrev = false;
            Submit.Clicked -= Submit_Clicked;
            Submit.Clicked += SubmitEdit_Clicked;
        }

        async void Submit_Clicked(object sender, EventArgs e)
        {
            if(subrev)
            {
                SubjectReview newReview = new SubjectReview();
                newReview.SubjectId = s.Id;

                if (LectOrSubPicker.SelectedIndex != -1)
                {
                    newReview.LecturerId = EntityLoader.lecturers.Find(l => {
                        string name = l.FirstName + " " + l.LastName;
                        return name == LectOrSubPicker.Items[LectOrSubPicker.SelectedIndex];
                        }).Id;
                }

                newReview.Rating = ScorePicker.SelectedIndex + 1;
                newReview.UserId = ConstVars.currentUser.Id;
                newReview.Comment = CommentEntry.Text;

                if (AnonCheckBox.IsChecked)
                {
                    newReview.Anonymous = 1;
                }
                else
                {
                    newReview.Anonymous = 0;
                }

                EntityLoader.AddReview(newReview);
            }
            else
            {
                LecturerReview newReview = new LecturerReview();
                newReview.LecturerId = l.Id;

                if (LectOrSubPicker.SelectedIndex != -1)
                {
                    newReview.LecturerId = EntityLoader.subjects.Find(s => 
                        s.SubjectName == LectOrSubPicker.Items[LectOrSubPicker.SelectedIndex]).Id;
                }

                newReview.Rating = ScorePicker.SelectedIndex + 1;
                newReview.UserId = ConstVars.currentUser.Id;
                newReview.Comment = CommentEntry.Text;

                if (AnonCheckBox.IsChecked)
                {
                    newReview.Anonymous = 1;
                }
                else
                {
                    newReview.Anonymous = 0;
                }

                EntityLoader.AddReview(newReview);
            }

            await DisplayAlert("Success", "Your review was added.", "Cool!");
            OnAddReview();
            await Navigation.PopAsync();
        }

        async void SubmitEdit_Clicked(object sender, EventArgs e)
        {
            if(subrev)
            {
                SubjectReview currentReview = EntityLoader.GetUserReviewsS().Find(sr => sr.SubjectId == s.Id);

                if (LectOrSubPicker.SelectedIndex != -1)
                {
                    currentReview.LecturerId = EntityLoader.lecturers.Find(l => {
                        string name = l.FirstName + " " + l.LastName;
                        return name == LectOrSubPicker.Items[LectOrSubPicker.SelectedIndex];
                    }).Id;
                }

                currentReview.Rating = ScorePicker.SelectedIndex + 1;
                currentReview.Comment = CommentEntry.Text;

                if (AnonCheckBox.IsChecked)
                {
                    currentReview.Anonymous = 1;
                }
                else
                {
                    currentReview.Anonymous = 0;
                }

                EntityLoader.EditReview(currentReview);
            }
            else
            {
                LecturerReview currentReview = EntityLoader.GetUserReviewsL().Find(lr => lr.LecturerId == l.Id);

                if (LectOrSubPicker.SelectedIndex != -1)
                {
                    currentReview.LecturerId = EntityLoader.subjects.Find(s =>
                        s.SubjectName == LectOrSubPicker.Items[LectOrSubPicker.SelectedIndex]).Id;
                }

                currentReview.Rating = ScorePicker.SelectedIndex + 1;
                currentReview.Comment = CommentEntry.Text;

                if (AnonCheckBox.IsChecked)
                {
                    currentReview.Anonymous = 1;
                }
                else
                {
                    currentReview.Anonymous = 0;
                }

                EntityLoader.EditReview(currentReview);
            }

            await DisplayAlert("Success", "Your review was edited successfully.", "Cool!");
            OnAddReview();
            await Navigation.PopAsync();
        }
    }
}