using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

            foreach (Lecturer lect in EntityLoader.lecturers.OrderBy(l => l.FirstName))
            {
                LecturerViewList.Add(new LecOrSubView(lect.FirstName + " " + lect.LastName, Math.Round(EntityLoader.GetAvgRating(lect), 2).ToString()));
            }
        }

        public LecturerViewModel(ObservableCollection<LecOrSubView> lecturerViewList)
        {
            LecturerViewList = lecturerViewList;
        }

        public LecturerViewModel(List<LecOrSubView> lecturerViewList)
        {
            LecturerViewList = new ObservableCollection<LecOrSubView>(lecturerViewList);
        }

        public ObservableCollection<LecOrSubView> SortAZ()
        {
            return new ObservableCollection<LecOrSubView>(LecturerViewList.ToList().OrderBy(v => v.Item1));
        }

        public ObservableCollection<LecOrSubView> SortZA()
        {
            return new ObservableCollection<LecOrSubView>(LecturerViewList.ToList().OrderByDescending(v => v.Item1));
        }

        public ObservableCollection<LecOrSubView> Sort05()
        {
            return new ObservableCollection<LecOrSubView>(LecturerViewList.ToList().OrderBy(v => v.Item2));
        }

        public ObservableCollection<LecOrSubView> Sort50()
        {
            return new ObservableCollection<LecOrSubView>(LecturerViewList.ToList().OrderByDescending(v => v.Item2));
        }
    }
}
