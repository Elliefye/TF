using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TF2.Entities;

namespace TF2.ViewModels
{
    public class UserReviewListView
    {
        public ObservableCollection<UserReview> ReviewViewList { get; set; }

        public UserReviewListView(List<SubjectReview> subjectReviews, List<LecturerReview> lecturerReviews)
        {
            ReviewViewList = new ObservableCollection<UserReview>();

            foreach (SubjectReview sr in subjectReviews)
            {
                ReviewViewList.Add(new UserReview(sr));
            }

            foreach (LecturerReview lr in lecturerReviews)
            {
                ReviewViewList.Add(new UserReview(lr));
            }
        }
    }
}
