using HN.Bangumi.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class CalendarView
    {
        public CalendarView()
        {
            InitializeComponent();
        }

        public CalendarViewModel ViewModel => (CalendarViewModel)DataContext;

        private void SubjectGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemsView = (ListViewBase)sender;
            var animation = itemsView.PrepareConnectedAnimation("SubjectForwardAnimation", e.ClickedItem, "SubjectImage");
            animation.Configuration = new BasicConnectedAnimationConfiguration();
        }
    }
}
