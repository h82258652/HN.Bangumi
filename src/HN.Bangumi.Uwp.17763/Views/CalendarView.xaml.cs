using HN.Bangumi.ViewModels;

namespace HN.Bangumi.Uwp.Views
{
    public sealed partial class CalendarView
    {
        public CalendarView()
        {
            InitializeComponent();
        }

        public CalendarViewModel ViewModel => (CalendarViewModel)DataContext;
    }
}
