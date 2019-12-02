using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Entities;
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
            BindingContext = new ReviewViewModel(subjectReviews, lecturerReviews);
        }
    }
}