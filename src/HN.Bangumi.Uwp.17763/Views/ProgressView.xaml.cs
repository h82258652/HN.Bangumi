using HN.Bangumi.ViewModels;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class ProgressView
    {
        public ProgressView()
        {
            InitializeComponent();
        }

        public ProgressViewModel ViewModel => (ProgressViewModel)DataContext;
    }
}
