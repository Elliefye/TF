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
        private int currentSort = 1;

        public AllSubjects()
        {
            InitializeComponent();
            SubjectList.BindingContext = subjectViewModel;

            var sortTapGestureRecognizer = new TapGestureRecognizer();
            sortTapGestureRecognizer.Tapped += (s, e) => {
                if (currentSort == 1)
                {
                    currentSort = 2; //z-a
                    SortIcon.Source = "azup.png";
                    subjectViewModel = new SubjectViewModel(subjectViewModel.SortZA());
                    SubjectList.BindingContext = subjectViewModel;
                }
                else if (currentSort == 2)
                {
                    currentSort = 3; //5-0
                    SortIcon.Source = "stardown.png";
                    subjectViewModel = new SubjectViewModel(subjectViewModel.Sort50());
                    SubjectList.BindingContext = subjectViewModel;
                }
                else if (currentSort == 3)
                {
                    currentSort = 4; //0-5
                    SortIcon.Source = "starup.png";
                    subjectViewModel = new SubjectViewModel(subjectViewModel.Sort05());
                    SubjectList.BindingContext = subjectViewModel;
                }
                else
                {
                    currentSort = 1; //a-z
                    SortIcon.Source = "azdown.png";
                    subjectViewModel = new SubjectViewModel(subjectViewModel.SortAZ());
                    SubjectList.BindingContext = subjectViewModel;
                }
            };
            SortIcon.GestureRecognizers.Add(sortTapGestureRecognizer);
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            subjectViewModel = new SubjectViewModel(subjectViewModel.UpdateRatings());
            SubjectList.BindingContext = subjectViewModel;
        }
    }
}