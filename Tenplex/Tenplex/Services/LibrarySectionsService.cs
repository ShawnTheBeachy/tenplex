using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Services.Web;
using Template10.Utilities;
using Tenplex.Models;

namespace Tenplex.Services
{
    public sealed class LibrarySectionsService
    {
        private readonly ConnectionsService _connectionsService;
        private readonly IWebApiService _webApiService;

        public ObservableCollection<LibrarySection> LibrarySections { get; } = new ObservableCollection<LibrarySection>();

        public LibrarySectionsService(ConnectionsService connectionsService, IWebApiService webApiService)
        {
            _connectionsService = connectionsService ?? throw new ArgumentNullException(nameof(connectionsService));
            _webApiService = webApiService ?? throw new ArgumentNullException(nameof(webApiService));
        }

        public async Task InitializeAsync()
        {
            var url = $"{_connectionsService.CurrentConnection.Uri}/library/sections";
            var result = await _webApiService.GetAsync(new Uri(url));

            var jObj = JObject.Parse(result);
            var directory = jObj.SelectToken("MediaContainer.Directory");
            var sections = directory.ToObject<IEnumerable<LibrarySection>>();
            LibrarySections.AddRange(sections, true);
        }
    }
}
