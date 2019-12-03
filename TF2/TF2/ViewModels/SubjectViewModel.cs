using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TF2.Entities;

namespace TF2.ViewModels
{
    public class SubjectViewModel
    {
        public ObservableCollection<LecOrSubView> SubjectViewList { get; set; }

        public SubjectViewModel()
        {
            SubjectViewList = new ObservableCollection<LecOrSubView>();

            foreach (Subject sub in EntityLoader.subjects.OrderBy(s => s.SubjectName))
            {
                SubjectViewList.Add(new LecOrSubView(sub.SubjectName, Math.Round(EntityLoader.GetAvgRating(sub), 2).ToString()));
            }
        }

        public SubjectViewModel(List<LecOrSubView> subjectViewList)
        {
            SubjectViewList = new ObservableCollection<LecOrSubView>(subjectViewList);
        }

        public SubjectViewModel(ObservableCollection<LecOrSubView> subjectViewList)
        {
            SubjectViewList = subjectViewList;
        }

        public ObservableCollection<LecOrSubView> SortAZ()
        {
            return new ObservableCollection<LecOrSubView>(SubjectViewList.ToList().OrderBy(v => v.Item1));
        }

        public ObservableCollection<LecOrSubView> SortZA()
        {
            return new ObservableCollection<LecOrSubView>(SubjectViewList.ToList().OrderByDescending(v => v.Item1));
        }

        public ObservableCollection<LecOrSubView> Sort05()
        {
            return new ObservableCollection<LecOrSubView>(SubjectViewList.ToList().OrderBy(v => v.Item2));
        }

        public ObservableCollection<LecOrSubView> Sort50()
        {
            return new ObservableCollection<LecOrSubView>(SubjectViewList.ToList().OrderByDescending(v => v.Item2));
        }
    }
}
