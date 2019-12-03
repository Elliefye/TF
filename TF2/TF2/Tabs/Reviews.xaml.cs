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

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var content = e.Item as Rev;
            Subject sub = EntityLoader.subjects.Find(s => s.SubjectName == content.Reviewerusername);

            if(sub != null)
            {
                SubjectReview subrev = EntityLoader.GetUserReviewsS().Find(sr => sr.SubjectId == sub.Id);
                await Navigation.PushAsync(new Tabs.AddReview(sub, subrev));
            }
            else
            {
                Lecturer lec = EntityLoader.lecturers.Find(l => l.FirstName + " " + l.LastName == content.Reviewerusername);
                LecturerReview lecrev = EntityLoader.GetUserReviewsL().Find(lr => lr.LecturerId == lec?.Id);
                await Navigation.PushAsync(new Tabs.AddReview(lec, lecrev));
            }
        }
    }
}