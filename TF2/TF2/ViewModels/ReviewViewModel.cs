using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TF2.Entities;

namespace TF2
{
    class ReviewViewModel
    {
        public ObservableCollection<Rev> ReviewViewList { get; set; }

        public ReviewViewModel(Lecturer lecturer)
        {
            ReviewViewList = new ObservableCollection<Rev>();

            foreach (LecturerReview review in EntityLoader.GetLecturerReviews(lecturer))
            {
                if (review.Anonymous == 1)
                {
                    ReviewViewList.Add(new Rev("Anonymous", review.Rating, review.Comment));
                }
                else
                {
                    ReviewViewList.Add(new Rev(EntityLoader.GetReviewerUsername(review), review.Rating, review.Comment));
                }
                
            }
        }

        public ReviewViewModel(Subject subject)
        {
            ReviewViewList = new ObservableCollection<Rev>();

            foreach (SubjectReview review in EntityLoader.GetSubjectReviews(subject))
            {
                if (review.Anonymous == 1)
                {
                    ReviewViewList.Add(new Rev("Anonymous", review.Rating, review.Comment));
                }
                else
                {
                    ReviewViewList.Add(new Rev(EntityLoader.GetReviewerUsername(review), review.Rating, review.Comment));
                }

            }
        }
        /*
        public ReviewViewModel(List<Review> reviews)
        {
            ReviewsList = new ObservableCollection<Rev>();

            foreach (Review review in reviews)
            {
                Lecturer lec = EntityLoader.lecturers.Find(l => l.Id == review.LecturerId);
                String lecturerName = lec.FirstName + " " + lec.LastName;
                Subject sub = EntityLoader.subjects.Find(s => s.Id == review.SubjectId);
                String subjectName = sub.SubjectName;

                ReviewsList.Add(new Rev(lecturerName, subjectName, review.LecturerScore, review.SubjectScore, review.Comment));
            }
        }
        */
    }
}
