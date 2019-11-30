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

		public AddReview ()
		{
			InitializeComponent ();
		}

        public AddReview (Lecturer lecturer)
        {
            InitializeComponent();

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

            TopTextLabel.Text += subject.SubjectName;
            LectOrSubPicker.Title = "(Optional) pick the lecturer";

            foreach (Lecturer lecturer in subject.Lecturers.Value)
            {
                LectOrSubPicker.Items.Add(lecturer.FirstName + " " + lecturer.LastName);
            }

            s = subject;
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
            await Navigation.PopAsync();
        }
    }
}