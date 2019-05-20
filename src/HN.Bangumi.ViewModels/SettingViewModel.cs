using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HN.Bangumi.Services;
using HN.Cache;

namespace HN.Bangumi.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        private readonly IAppToastService _appToastService;
        private readonly IDiskCache _diskCache;
        private long? _cacheSize;
        private RelayCommand _clearCacheCommand;
        private bool _isCleaningCache;

        public SettingViewModel(IDiskCache diskCache, IAppToastService appToastService)
        {
            _diskCache = diskCache;
            _appToastService = appToastService;

            Initialize();
        }

        public long? CacheSize
        {
            get => _cacheSize;
            private set => Set(ref _cacheSize, value);
        }

        public RelayCommand ClearCacheCommand
        {
            get
            {
                _clearCacheCommand = _clearCacheCommand ?? new RelayCommand(async () =>
                {
                    if (IsCleaningCache)
                    {
                        return;
                    }

                    try
                    {
                        IsCleaningCache = true;

                        await _diskCache.DeleteAllAsync();

                        CacheSize = await _diskCache.CalculateAllSizeAsync();

                        _appToastService.ShowMessage("清理成功");
                    }
                    finally
                    {
                        IsCleaningCache = false;
                    }
                }, () => CacheSize.HasValue);
                return _clearCacheCommand;
            }
        }

        public bool IsCleaningCache
        {
            get => _isCleaningCache;
            private set => Set(ref _isCleaningCache, value);
        }

        private async void Initialize()
        {
            CacheSize = await _diskCache.CalculateAllSizeAsync();
        }
    }
}
