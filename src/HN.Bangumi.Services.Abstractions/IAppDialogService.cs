using System.Threading.Tasks;

namespace HN.Bangumi.Services
{
    public interface IAppDialogService
    {
        Task<bool> ShowConfirmAsync(string message);

        Task<bool> ShowConfirmAsync(string message, string title);

        Task<bool> ShowConfirmAsync(string message, string title, string confirmText, string cancelText);
    }
}
