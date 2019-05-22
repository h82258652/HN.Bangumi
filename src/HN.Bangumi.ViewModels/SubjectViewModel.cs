using System.Linq;
using System.Net.Http;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        private RelayCommand<Ep> _dropEpCommand;
        private int _id;
        private bool _isLoading;
        private RelayCommand<Ep> _queueEpCommand;
        private RelayCommand<Ep> _removeEpCommand;
        private Subject _subject;

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            private set => Set(ref _isBusy, value);
        }

        private RelayCommand<Ep> _watchedEpCommand;

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
                RaisePropertyChanged(nameof(IsSignIn));
                Load(_id);
            });
            messenger.Register<SignedOutMessage>(this, message =>
            {
                RaisePropertyChanged(nameof(IsSignIn));
                Load(_id);
            });
        }

        public RelayCommand<Ep> DropEpCommand
        {
            get
            {
                _dropEpCommand = _dropEpCommand ?? new RelayCommand<Ep>(async ep =>
                {
                    if (IsBusy)
                    {
                        return;
                    }

                    try
                    {
                        IsBusy = true;

                        var result = await _subjectService.UpdateEpStatusAsync(ep.Id, EpStatus.Drop);
                        if (result.ErrorCode == 0 || result.ErrorCode == 200)
                        {
                            ep.Status = "Drop";

                            Load(_id);
                        }
                        else
                        {
                            _appToastService.ShowError(result.ErrorMessage);
                        }
                    }
                    catch (HttpRequestException)
                    {
                        _appToastService.ShowError("标记条目状态失败，请检查网络");
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
                return _dropEpCommand;
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set => Set(ref _isLoading, value);
        }

        public bool IsSignIn => _client.IsSignIn;

        public Ep[] NormalEps => Subject?.Eps?.Where(temp => temp.Type != 1).ToArray();

        public RelayCommand<Ep> QueueEpCommand
        {
            get
            {
                _queueEpCommand = _queueEpCommand ?? new RelayCommand<Ep>(async ep =>
                {
                    if (IsBusy)
                    {
                        return;
                    }

                    try
                    {
                        IsBusy = true;

                        var result = await _subjectService.UpdateEpStatusAsync(ep.Id, EpStatus.Queue);
                        if (result.ErrorCode == 0 || result.ErrorCode == 200)
                        {
                            ep.Status = "Queue";

                            Load(_id);
                        }
                        else
                        {
                            _appToastService.ShowError(result.ErrorMessage);
                        }
                    }
                    catch (HttpRequestException)
                    {
                        _appToastService.ShowError("标记条目状态失败，请检查网络");
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
                return _queueEpCommand;
            }
        }

        public RelayCommand<Ep> RemoveEpCommand
        {
            get
            {
                _removeEpCommand = _removeEpCommand ?? new RelayCommand<Ep>(async ep =>
                {
                    if (IsBusy)
                    {
                        return;
                    }

                    try
                    {
                        IsBusy = true;

                        var result = await _subjectService.UpdateEpStatusAsync(ep.Id, EpStatus.Remove);
                        if (result.ErrorCode == 0 || result.ErrorCode == 200)
                        {
                            Load(_id);
                        }
                        else
                        {
                            _appToastService.ShowError(result.ErrorMessage);
                        }
                    }
                    catch (HttpRequestException)
                    {
                        _appToastService.ShowError("标记条目状态失败，请检查网络");
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
                return _removeEpCommand;
            }
        }

        public Ep[] SpecialEps => Subject?.Eps?.Where(temp => temp.Type == 1).ToArray();

        public Subject Subject
        {
            get => _subject;
            private set
            {
                Set(ref _subject, value);
                RaisePropertyChanged(nameof(NormalEps));
                RaisePropertyChanged(nameof(SpecialEps));
            }
        }

        public RelayCommand<Ep> WatchedEpCommand
        {
            get
            {
                _watchedEpCommand = _watchedEpCommand ?? new RelayCommand<Ep>(async ep =>
                {
                    if (IsBusy)
                    {
                        return;
                    }

                    try
                    {
                        IsBusy = true;

                        var result = await _subjectService.UpdateEpStatusAsync(ep.Id, EpStatus.Watched);
                        if (result.ErrorCode == 0 || result.ErrorCode == 200)
                        {
                            ep.Status = "Watched";

                            Load(_id);
                        }
                        else
                        {
                            _appToastService.ShowError(result.ErrorMessage);
                        }
                    }
                    catch (HttpRequestException)
                    {
                        _appToastService.ShowError("标记条目状态失败，请检查网络");
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
                return _watchedEpCommand;
            }
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

                _id = id;

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

                if (_id == id)
                {
                    Subject = subject;
                }
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
