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
        
        public ReviewViewModel(List<SubjectReview> subjectReviews, List<LecturerReview> lecturerReviews)
        {
            ReviewViewList = new ObservableCollection<Rev>();

            foreach (SubjectReview review in subjectReviews)
            {
                string subName = EntityLoader.subjects.Find(s => s.Id == review.SubjectId).SubjectName;
                ReviewViewList.Add(new Rev(subName, review.Rating, review.Comment));
            }

            foreach (LecturerReview review in lecturerReviews)
            {
                Lecturer lec = EntityLoader.lecturers.Find(l => l.Id == review.LecturerId);
                ReviewViewList.Add(new Rev(lec.FirstName + " " + lec.LastName, review.Rating, review.Comment));
            }
        }       
    }
}
