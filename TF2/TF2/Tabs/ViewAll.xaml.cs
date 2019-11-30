using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Entities;
using TF2.Tabs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewAll : ContentPage
    {
        LecSubViewModel lsvm = new LecSubViewModel();

        public ViewAll()
        {
            InitializeComponent();
            BindingContext = lsvm;
        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var content = e.Item as LecSub;
            Subject sub = EntityLoader.subjects.FirstOrDefault(s => s.Id == content.subjectId);
            await Navigation.PushAsync(new SubLectProfile(sub));
            //await Navigation.PushAsync(new NavigationPage(new SubLectProfile(sub)));
            //System.Diagnostics.Debug.WriteLine(content.LecturerName + content.SubjectName);
            //await Navigation.PushAsync(new WriteReview(content)); //pass content if you want to pass the clicked item object to another page
        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Searchbar.Text))
            {
                List<LecSub> currentItems = new List<LecSub>();

                foreach (LecSub item in lsvm.LecSubList)//lsvm.LecSubList.ToList<LecSub>())
                {
                    if (item.LecturerName.ToLower().Contains(Searchbar.Text.ToLower()) ||
                        item.SubjectName.ToLower().Contains(Searchbar.Text.ToLower()))
                    {
                        currentItems.Add(item);
                    }
                }

                lsvm = new LecSubViewModel(currentItems);
                BindingContext = lsvm;
            }            
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(Searchbar.Text))
            {
                lsvm = new LecSubViewModel();
                BindingContext = lsvm;
            }
        }
    }
}