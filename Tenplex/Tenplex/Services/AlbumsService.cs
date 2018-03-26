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
    public sealed class AlbumsService
    {
        private readonly ServerConnectionInfoService _serverConnectionInfoService;
        private readonly IWebApiService _webApiService;

        public ObservableCollection<Album> Albums { get; } = new ObservableCollection<Album>();

        public AlbumsService(ServerConnectionInfoService serverConnectionInfoService, IWebApiService webApiService)
        {
            _serverConnectionInfoService = serverConnectionInfoService ?? throw new ArgumentNullException(nameof(serverConnectionInfoService));
            _webApiService = webApiService ?? throw new ArgumentNullException(nameof(webApiService));
        }

        public async Task LoadAlbumsAsync(string sectionKey)
        {
            // this._webApiService.AddHeader("Accept", "application/json");

            var url = $"http://{_serverConnectionInfoService.GetServerIpAddress()}:{_serverConnectionInfoService.GetServerPortNumber()}/library/sections/{sectionKey}/albums";
            var result = await _webApiService.GetAsync(new Uri(url));

            var jObj = JObject.Parse(result);
            var directory = jObj.SelectToken("MediaContainer.Metadata");
            var albums = directory.ToObject<IEnumerable<Album>>();
            Albums.AddRange(albums, true);
        }
    }
}
