using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TF2.Entities;

namespace TF2
{
    public class LecSubViewModel
    {
        private ObservableCollection<dynamic> _lecSubList;
        public ObservableCollection<dynamic> LecSubList
        {
            get { return _lecSubList; }
            set
            {
                _lecSubList = value;
            }
        }

        public LecSubViewModel()
        {

            LecSubList = new ObservableCollection<dynamic>();


            foreach (var obj in TempEntityLoader.lecturersSubjects)
            {
                LecSubList.Add(obj);
            }
        }

    }
}
