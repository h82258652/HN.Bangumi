using HN.Bangumi.ViewModels;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class SettingView
    {
        public SettingView()
        {
            InitializeComponent();
        }

        public SettingViewModel ViewModel => (SettingViewModel) DataContext;
    }
}
