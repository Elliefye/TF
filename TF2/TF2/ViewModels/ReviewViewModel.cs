using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TF2.Entities;

namespace TF2
{
    class ReviewViewModel
    {
        public ObservableCollection<Rev> _reviewsList;
        public ObservableCollection<Rev> ReviewsList
        {
            get { return _reviewsList; }
            set
            {
                _reviewsList = value;
            }
        }

        public ReviewViewModel()
        {

            ReviewsList = new ObservableCollection<Rev>();

            foreach (Review review in EntityLoader.reviews)
            {
                Lecturer lec = EntityLoader.lecturers.Find(l => l.Id == review.LecturerId);
                String lecturerName = lec.FirstName + " " + lec.LastName;
                Subject sub = EntityLoader.subjects.Find(s => s.Id == review.SubjectId);
                String subjectName = sub.SubjectName;

                _reviewsList.Add(new Rev(lecturerName, subjectName, review.LecturerScore, review.SubjectScore, review.Comment));
            }
        }
    }
}
