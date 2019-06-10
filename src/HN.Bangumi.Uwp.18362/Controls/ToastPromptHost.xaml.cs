namespace HN.Bangumi.Uwp.Controls
{
    public sealed partial class ToastPromptHost
    {
        public ToastPromptHost()
        {
            InitializeComponent();
        }

        public async void Show(ToastPrompt toastPrompt)
        {
            RootGrid.Children.Add(toastPrompt);
            await toastPrompt.ShowAsync();
            RootGrid.Children.Remove(toastPrompt);
        }
    }
}
