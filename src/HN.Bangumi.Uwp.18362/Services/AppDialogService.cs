using System.Threading.Tasks;
using HN.Bangumi.Services;
using Windows.UI.Popups;
using WinRTXamlToolkit.Controls.Extensions;

namespace HN.Bangumi.Uwp.Services
{
    public class AppDialogService : IAppDialogService
    {
        public Task<bool> ShowConfirmAsync(string message)
        {
            return ShowConfirmAsync(message, string.Empty);
        }

        public Task<bool> ShowConfirmAsync(string message, string title)
        {
            return ShowConfirmAsync(message, title, "确定", "取消");
        }

        public async Task<bool> ShowConfirmAsync(string message, string title, string confirmText, string cancelText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var messageDialog = new MessageDialog(message, title);
            messageDialog.Commands.Add(new UICommand(confirmText, command =>
            {
                tcs.SetResult(true);
            }));
            messageDialog.Commands.Add(new UICommand(cancelText, command =>
            {
                tcs.SetResult(false);
            }));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowQueuedAsync();
            return await tcs.Task;
        }
    }
}
