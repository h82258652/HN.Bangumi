using System.Net.Http;
using GalaSoft.MvvmLight;
using HN.Bangumi.API.Models;
using HN.Bangumi.Services;

namespace HN.Bangumi.ViewModels
{
    public class SubjectViewModel : ViewModelBase
    {
        private readonly IAppToastService _appToastService;
        private readonly ISubjectService _subjectService;
        private bool _isLoading;
        private Subject _subject;

        public SubjectViewModel(
            ISubjectService subjectService,
            IAppToastService appToastService)
        {
            _subjectService = subjectService;
            _appToastService = appToastService;
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set => Set(ref _isLoading, value);
        }

        public Subject Subject
        {
            get => _subject;
            private set => Set(ref _subject, value);
        }

        public async void Load(int id)
        {
            try
            {
                IsLoading = true;

                Subject = await _subjectService.GetAsync(id);
            }
            catch (HttpRequestException)
            {
                _appToastService.ShowError("加载条目信息失败，请稍后重试");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
