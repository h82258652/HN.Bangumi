using System.Linq;
using System.Net.Http;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using HN.Bangumi.API;
using HN.Bangumi.API.Models;
using HN.Bangumi.Messages;
using HN.Bangumi.Services;

namespace HN.Bangumi.ViewModels
{
    public class SubjectViewModel : ViewModelBase
    {
        private readonly IAppToastService _appToastService;
        private readonly IBangumiClient _client;
        private readonly ISubjectService _subjectService;
        private bool _isLoading;
        private Subject _subject;

        public SubjectViewModel(
            IBangumiClient client,
            ISubjectService subjectService,
            IAppToastService appToastService,
            IMessenger messenger)
        {
            _client = client;
            _subjectService = subjectService;
            _appToastService = appToastService;

            messenger.Register<SignedInMessage>(this, message =>
            {
                // TODO reload
            });
            messenger.Register<SignedOutMessage>(this, message =>
            {
                // TODO reload
            });
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
            if (IsLoading)
            {
                return;
            }

            try
            {
                IsLoading = true;

                var subject = await _subjectService.GetAsync(id);

                if (_client.IsSignIn)
                {
                    var progress = await _subjectService.GetProgressAsync(id);
                    if (progress?.Eps != null)
                    {
                        foreach (var epProgress in progress.Eps)
                        {
                            if (epProgress.Status.Id == 1)
                            {
                                var ep = subject.Eps.FirstOrDefault(temp => temp.Id == epProgress.Id);
                                if (ep != null)
                                {
                                    ep.Status = "Queue";
                                }
                            }
                            else if (epProgress.Status.Id == 2)
                            {
                                var ep = subject.Eps.FirstOrDefault(temp => temp.Id == epProgress.Id);
                                if (ep != null)
                                {
                                    ep.Status = "Watched";
                                }
                            }
                            else if (epProgress.Status.Id == 3)
                            {
                                var ep = subject.Eps.FirstOrDefault(temp => temp.Id == epProgress.Id);
                                if (ep != null)
                                {
                                    ep.Status = "Drop";
                                }
                            }
                        }
                    }
                }

                Subject = subject;
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
