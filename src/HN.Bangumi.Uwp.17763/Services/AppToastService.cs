using System;
using FontAwesome5;
using FontAwesome5.UWP;
using HN.Bangumi.Services;
using HN.Bangumi.Uwp.Controls;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using WinRTXamlToolkit.Controls.Extensions;

namespace HN.Bangumi.Uwp.Services
{
    public class AppToastService : IAppToastService
    {
        private static ToastPromptHost ToastPromptHost => Window.Current.Content.GetFirstDescendantOfType<ToastPromptHost>();

        private static ToastPrompt CreateToastPrompt(string message)
        {
            return new ToastPrompt
            {
                Foreground = new SolidColorBrush(Colors.White),
                Message = message,
                Duration = TimeSpan.FromSeconds(2)
            };
        }

        public void ShowError(string message)
        {
            var toastPrompt = CreateToastPrompt(message);
            toastPrompt.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0x17, 0x20));
            toastPrompt.Icon = new FontAwesome
            {
                Icon = EFontAwesomeIcon.Solid_WindowClose
            };
            ToastPromptHost.Show(toastPrompt);
        }

        public void ShowInformation(string message)
        {
            var toastPrompt = CreateToastPrompt(message);
            toastPrompt.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x0, 0x9C, 0xF3));
            toastPrompt.Icon = new FontAwesome
            {
                Icon = EFontAwesomeIcon.Solid_InfoCircle
            };
            ToastPromptHost.Show(toastPrompt);
        }

        public void ShowMessage(string message)
        {
            var toastPrompt = CreateToastPrompt(message);
            toastPrompt.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x13, 0xC0, 0x4D));
            toastPrompt.Icon = new FontAwesome
            {
                Icon = EFontAwesomeIcon.Solid_Check
            };
            ToastPromptHost.Show(toastPrompt);
        }

        public void ShowWarning(string message)
        {
            var toastPrompt = CreateToastPrompt(message);
            toastPrompt.Background = new SolidColorBrush(Color.FromArgb(a: 0xFF, r: 0xFF, g: 0xC1, b: 0x0));
            toastPrompt.Icon = new FontAwesome
            {
                Icon = EFontAwesomeIcon.Solid_ExclamationTriangle
            };
            ToastPromptHost.Show(toastPrompt);
        }
    }
}
