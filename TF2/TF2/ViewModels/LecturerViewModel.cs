using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TF2.Entities;

namespace TF2.ViewModels
{
    public class LecturerViewModel
    {
        public ObservableCollection<LecOrSubView> LecturerViewList { get; set; }

        public LecturerViewModel()
        {
            LecturerViewList = new ObservableCollection<LecOrSubView>();

            foreach (Lecturer lect in EntityLoader.lecturers)
            {
                LecturerViewList.Add(new LecOrSubView(lect.FirstName + " " + lect.LastName, Math.Round(EntityLoader.GetAvgRating(lect), 2).ToString()));
            }
        }

        public LecturerViewModel(List<LecOrSubView> lecturerViewList)
        {
            LecturerViewList = new ObservableCollection<LecOrSubView>(lecturerViewList);
        }
    }
}
