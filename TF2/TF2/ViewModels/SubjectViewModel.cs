using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            foreach (Subject sub in EntityLoader.subjects)
            {
                System.Diagnostics.Debug.WriteLine(sub.SubjectName);
                SubjectViewList.Add(new LecOrSubView(sub.SubjectName, Math.Round(EntityLoader.GetAvgRating(sub), 2).ToString()));
            }
        }

        public SubjectViewModel(List<LecOrSubView> subjectViewList)
        {
            SubjectViewList = new ObservableCollection<LecOrSubView>(subjectViewList);
        }
    }
}
