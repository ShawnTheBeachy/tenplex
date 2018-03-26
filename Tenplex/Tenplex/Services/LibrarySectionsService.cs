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
        private readonly ServerConnectionInfoService _serverConnectionInfoService;
        private readonly IWebApiService _webApiService;

        public ObservableCollection<LibrarySection> LibrarySections { get; } = new ObservableCollection<LibrarySection>();

        public LibrarySectionsService(ServerConnectionInfoService serverConnectionInfoService, IWebApiService webApiService)
        {
            _serverConnectionInfoService = serverConnectionInfoService ?? throw new ArgumentNullException(nameof(serverConnectionInfoService));
            _webApiService = webApiService ?? throw new ArgumentNullException(nameof(webApiService));
        }

        public async Task InitializeAsync()
        {
            // this._webApiService.AddHeader("Accept", "application/json");

            var url = $"http://{_serverConnectionInfoService.GetServerIpAddress()}:{_serverConnectionInfoService.GetServerPortNumber()}/library/sections";
            var result = await _webApiService.GetAsync(new Uri(url));

            var jObj = JObject.Parse(result);
            var directory = jObj.SelectToken("MediaContainer.Directory");
            var sections = directory.ToObject<IEnumerable<LibrarySection>>();
            LibrarySections.AddRange(sections, true);
        }
    }
}
