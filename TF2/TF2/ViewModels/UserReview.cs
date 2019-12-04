using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TF2.Entities;
using TF2.Tabs;
using Xamarin.Forms;

namespace TF2.ViewModels
{
    public class UserReview
    {
        public string LecSubName { get; set; }
        public string LecSubScore { get; set; }
        public string Comment { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public UserReview(LecturerReview review)
        {
            Lecturer lecturer = EntityLoader.lecturers.Find(l => l.Id == review.LecturerId);
            LecSubName = lecturer.FirstName + " " + lecturer.LastName;
            LecSubScore = review.Rating.ToString();
            Comment = review.Comment;
            EditCommand = new Command(OnEdit);
            DeleteCommand = new Command(OnDelete);
        }

        public UserReview(SubjectReview review)
        {
            Subject subject = EntityLoader.subjects.Find(s => s.Id == review.SubjectId);
            LecSubName = subject.SubjectName;
            LecSubScore = review.Rating.ToString();
            Comment = review.Comment;
            EditCommand = new Command(OnEdit);
            DeleteCommand = new Command(OnDelete);
        }

        public async void OnEdit(object s)
        {
            UserReview current = (UserReview)s;
            Lecturer lect = EntityLoader.lecturers.Find(l => l.FirstName + " " + l.LastName == current.LecSubName);

            if (lect != null)
            {
                LecturerReview review = EntityLoader.GetUserReviewsL().Find(lr => lr.LecturerId == lect.Id);
                await App.Current.MainPage.Navigation.PushAsync(new AddReview(lect, review));
            }
            else
            {
                Subject sub = EntityLoader.subjects.Find(su => su.SubjectName == current.LecSubName);
                SubjectReview review = EntityLoader.GetUserReviewsS().Find(sr => sr.SubjectId == sub?.Id);
                await App.Current.MainPage.Navigation.PushAsync(new AddReview(sub, review));
            }
        }

        public async void OnDelete(object s)
        {
            UserReview current = (UserReview)s;
            bool answer = await App.Current.MainPage.DisplayAlert("", "Are you sure you want to delete your review for " + current.LecSubName + "?", "Yes", "No");

            if (answer)
            {
                Lecturer lect = EntityLoader.lecturers.Find(l => l.FirstName + " " + l.LastName == current.LecSubName);

                if (lect != null)
                {
                    LecturerReview review = EntityLoader.GetUserReviewsL().Find(lr => lr.LecturerId == lect.Id);
                    try
                    {
                        EntityLoader.DeleteReview(review);
                        await App.Current.MainPage.Navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Oh no!", ex.Message, "OK");
                    }
                }
                else
                {
                    Subject sub = EntityLoader.subjects.Find(su => su.SubjectName == current.LecSubName);
                    SubjectReview review = EntityLoader.GetUserReviewsS().Find(sr => sr.SubjectId == sub?.Id);
                    try
                    {
                        EntityLoader.DeleteReview(review);
                        await App.Current.MainPage.Navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Oh no!", ex.Message, "OK");
                    }
                }
            }
        }
    }
}
