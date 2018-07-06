using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Template10.Services.Web;
using Template10.Utilities;
using Tenplex.Models;

namespace Tenplex.Services
{
    public sealed class AlbumsService
    {
        private readonly ConnectionsService _connectionsService;
        private readonly IWebApiService _webApiService;

        public ObservableCollection<Album> Albums { get; } = new ObservableCollection<Album>();

        public AlbumsService(ConnectionsService connectionsService, IWebApiService webApiService)
        {
            _connectionsService = connectionsService ?? throw new ArgumentNullException(nameof(connectionsService));
            _webApiService = webApiService ?? throw new ArgumentNullException(nameof(webApiService));
        }

        public async Task<Album> GetAlbumAsync(string ratingKey)
        {
            var url = $"{_connectionsService.CurrentConnection.Uri}/library/metadata/{ratingKey}";
            var result = await _webApiService.GetAsync(new Uri(url));
            var jObj = JObject.Parse(result);
            var metadata = jObj.SelectToken("MediaContainer.Metadata")[0];
            var album = metadata.ToObject<Album>();
            return album;
        }

        public async Task<AlbumProperties> GetAlbumPropertiesAsync(Album album)
        {
            var url = $"{_connectionsService.CurrentConnection.Uri}/library/metadata/{album.RatingKey}";
            var result = await _webApiService.GetAsync(new Uri(url));
            var jObj = JObject.Parse(result);
            var directory = jObj.SelectToken("MediaContainer.Metadata")[0];
            var properties = directory.ToObject<AlbumProperties>();
            return properties;
        }

        public async Task<IEnumerable<Poster>> GetPostersAsync(Album album)
        {
            var url = $"{_connectionsService.CurrentConnection.Uri}/library/metadata/{album.RatingKey}/posters";
            var result = await _webApiService.GetAsync(new Uri(url));
            var jObj = JObject.Parse(result);
            var directory = jObj.SelectToken("MediaContainer.Metadata");
            var posters = directory.ToObject<IEnumerable<Poster>>();
            return posters;
        }

        public async Task LoadAlbumsAsync(string sectionKey)
        {
            // this._webApiService.AddHeader("Accept", "application/json");

            var url = $"{_connectionsService.CurrentConnection.Uri}/library/sections/{sectionKey}/albums";
            var result = await _webApiService.GetAsync(new Uri(url));
            var jObj = JObject.Parse(result);

            try
            {
                var directory = jObj.SelectToken("MediaContainer.Metadata");
                var albums = directory.ToObject<IEnumerable<Album>>();
                Albums.AddRange(albums, true);
            }

            catch
            {
                // This can happen if there are no albums; the Metadata token won't exist.
                Albums.Clear();
            }
        }

        public async Task SetPosterAsync(Album album, Poster poster)
        {
            var url = $"{_connectionsService.CurrentConnection.Uri}/library/metadata/{album.RatingKey}/poster?url={WebUtility.UrlEncode(poster.RatingKey)}";
            await _webApiService.PutAsync<object>(new Uri(url), "");
        }

        public async Task UpdateAlbumPropertiesAsync(Album album, AlbumProperties properties)
        {
            var url = $"{_connectionsService.CurrentConnection.Uri}/library/sections/{album.LibrarySectionId}/all?type=9&id={properties.RatingKey}";
            // url += WebUtility.UrlEncode(string.Join("", properties.GetTrackedChanges().Select(p => $"&{p.Key}={p.Value}")));
            await _webApiService.PutAsync<object>(new Uri(url), properties.GetTrackedChanges());
        }
    }
}
