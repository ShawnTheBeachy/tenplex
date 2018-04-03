using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Tenplex.Models;
using Tenplex.Services;

namespace Tenplex.ViewModels
{
    public sealed class ShowsPageViewModel : ViewModelBase
    {
        private readonly ShowsService _showsService;

        public ObservableCollection<Show> Shows { get; set; } = new ObservableCollection<Show>();
        public string SectionKey = default(string);

        public ShowsPageViewModel(ShowsService showsService)
        {
            _showsService = showsService ?? throw new ArgumentNullException(nameof(showsService));
            Shows = _showsService.Shows;
        }

        public async override Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            var sectionKey = SectionKey;

            if (parameters.ContainsKey("sectionKey"))
                sectionKey = parameters.GetValue<string>("sectionKey");

            if (Shows.Count == 0 || sectionKey != SectionKey)
            {
                _showsService.Shows.Clear();
                await _showsService.LoadShowsAsync(sectionKey);
            }

            SectionKey = sectionKey;
        }
    }
}
