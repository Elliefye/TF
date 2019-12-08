using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TF2.Entities;
using TF2.Tabs;
using TF2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TF2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reviews : ContentPage
    {
        public Reviews(List<SubjectReview> subjectReviews, List<LecturerReview> lecturerReviews)
        {
            InitializeComponent();
            BindingContext = new UserReviewListView(subjectReviews, lecturerReviews);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new UserReviewListView(EntityLoader.GetUserReviewsS(), EntityLoader.GetUserReviewsL());
        }
    }
}