using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TF2.Entities;

namespace TF2
{
    public class LecSubViewModel
    {
        public ObservableCollection<LecSub> _lecSubList;
        public ObservableCollection<LecSub> LecSubList
        {
            get { return _lecSubList; }
            set
            {
                _lecSubList = value;
            }
        }

        public LecSubViewModel()
        {
            LecSubList = new ObservableCollection<LecSub>();

            /*foreach (LecturersAndSubjects ls in EntityLoader.GetLecturersAndSubjects())
            {
                Lecturer lec = EntityLoader.lecturers.Find(l => l.Id == ls.LecturerId);
                int lecturerId = lec.Id;
                String lecturerName = lec.FirstName + " " + lec.LastName;
                Subject sub = EntityLoader.subjects.Find(s => s.Id == ls.SubjectId);
                int subjectId = sub.Id;
                String subjectName = sub.SubjectName;

                _lecSubList.Add(new LecSub(lecturerId, subjectId, lecturerName, subjectName));
            }*/
        }

        public LecSubViewModel(List<LecSub> lecSubList)
        {
            LecSubList = new ObservableCollection<LecSub>(lecSubList);
        }


        public List<LecSub> GetAllItems()
        {
            return new List<LecSub>(LecSubList);
        }
    }
}
