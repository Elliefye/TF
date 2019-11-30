using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TF2.ViewModels;
using TF2.Entities;

namespace TF2.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllSubjects : ContentPage
    {
        SubjectViewModel subjectViewModel = new SubjectViewModel();

        public AllSubjects()
        {
            InitializeComponent();
            SubjectList.BindingContext = subjectViewModel;
        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as LecOrSubView;
            Subject sub = EntityLoader.subjects.FirstOrDefault(s => s.SubjectName == content.Item1);
            await Navigation.PushAsync(new SubLectProfile(sub));
        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Searchbar.Text))
            {
                List<LecOrSubView> currentItems = new List<LecOrSubView>();

                foreach (LecOrSubView item in subjectViewModel.SubjectViewList)
                {
                    if (item.Item1.ToLower().Contains(Searchbar.Text.ToLower()))
                    {
                        currentItems.Add(item);
                    }
                }

                subjectViewModel = new SubjectViewModel(currentItems);
                SubjectList.BindingContext = subjectViewModel;
            }
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Searchbar.Text))
            {
                subjectViewModel = new SubjectViewModel();
                SubjectList.BindingContext = subjectViewModel;
            }
        }
    }
}