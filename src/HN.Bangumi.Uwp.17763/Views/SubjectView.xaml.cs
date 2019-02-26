using HN.Bangumi.ViewModels;
using Windows.UI.Xaml.Navigation;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class SubjectView
    {
        public SubjectView()
        {
            InitializeComponent();
        }

        public SubjectViewModel ViewModel => (SubjectViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var id = (int)e.Parameter;

            ViewModel.Load(id);
        }
    }
}
