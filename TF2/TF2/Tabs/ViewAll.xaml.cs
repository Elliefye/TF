using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewAll : ContentPage
    {
        public ViewAll()
        {
            InitializeComponent();
            BindingContext = new LecSubViewModel();
        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var content = e.Item as LecSub;
            System.Diagnostics.Debug.WriteLine(content.LecturerName + content.SubjectName);
            await Navigation.PushAsync(new WriteReview(content)); //pass content if you want to pass the clicked item object to another page
        }
    }
}