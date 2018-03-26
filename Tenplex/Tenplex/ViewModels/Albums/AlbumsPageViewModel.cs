using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Tenplex.Models;
using Tenplex.Services;

namespace Tenplex.ViewModels
{
    public class AlbumsPageViewModel : ViewModelBase
    {
        private readonly AlbumsService _albumsService;
        private string _sectionKey = default(string);

        public ObservableCollection<Album> Albums { get; set; } = new ObservableCollection<Album>();

        public AlbumsPageViewModel(AlbumsService albumsService)
        {
            _albumsService = albumsService ?? throw new ArgumentNullException(nameof(albumsService));
            Albums = _albumsService.Albums;
        }

        public async override Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            var sectionKey = _sectionKey;

            if (parameters.ContainsKey("sectionKey"))
                sectionKey = parameters.GetValue<string>("sectionKey");

            if (Albums.Count == 0 || sectionKey != _sectionKey)
            {
                _albumsService.Albums.Clear();
                await _albumsService.LoadAlbumsAsync(sectionKey);
            }

            _sectionKey = sectionKey;
        }
    }
}
