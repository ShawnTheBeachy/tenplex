using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Template10.Services.Settings;
using Tenplex.Models;
using Windows.Web.Http;

namespace Tenplex.Services
{
    public class UsersService
    {
        private readonly ServersService _serversService;
        private readonly ISettingsHelper _settingsHelper;
        private List<User> _users = new List<User>();

        public User CurrentUser { get; set; }

        public UsersService(ServersService serversService, ISettingsHelper settingsHelper)
        {
            _serversService = serversService ?? throw new ArgumentNullException(nameof(serversService));
            _settingsHelper = settingsHelper ?? throw new ArgumentNullException(nameof(settingsHelper));
        }

        public string GetDefaultUserId()
        {
            _settingsHelper.TryReadString("DEFAULT_USER_ID", out var id);
            return id;
        }

        public async Task InitializeAsync()
        {
            var defaultUserId = GetDefaultUserId();
            await LoadUsersAsync();

            if (!string.IsNullOrWhiteSpace(defaultUserId))
                await SwitchUserAsync(_users.FirstOrDefault(user => user.Id == defaultUserId));
        }

        public async Task<IEnumerable<User>> LoadUsersAsync()
        {
            if (_users.Count > 0)
                return _users;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Plex-Client-Identifier", "tenplex");
                client.DefaultRequestHeaders.Add("X-Plex-Product", "Tenplex");
                client.DefaultRequestHeaders.Add("X-Plex-Token", _serversService.CurrentServer.AccessToken);
                client.DefaultRequestHeaders.Add("X-Plex-Version", "1.0.0");

                var response = await client.GetAsync(new Uri("https://plex.tv/api/home/users"));
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return null;
                else
                {
                    var reader = new StringReader(content);
                    var deserializer = new XmlSerializer(typeof(List<User>), new XmlRootAttribute("MediaContainer"));
                    _users = (List<User>)deserializer.Deserialize(reader);
                    return _users;
                }
            }
        }

        public async Task<User> SwitchUserAsync(User user)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Plex-Client-Identifier", "tenplex");
                client.DefaultRequestHeaders.Add("X-Plex-Product", "Tenplex");
                client.DefaultRequestHeaders.Add("X-Plex-Token", _serversService.CurrentServer.AccessToken);
                client.DefaultRequestHeaders.Add("X-Plex-Version", "1.0.0");

                var response = await client.PostAsync(new Uri($"https://plex.tv/api/v2/home/users/{user.UniqueId}/switch"), null);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return null;
                else
                {
                    var reader = new StringReader(content);
                    var deserializer = new XmlSerializer(typeof(User), new XmlRootAttribute("user"));
                    CurrentUser = (User)deserializer.Deserialize(reader);
                    return CurrentUser;
                }
            }
        }
        
        public void SetDefaultUserId(string userId)
        {
            _settingsHelper.WriteString("DEFAULT_USER_ID", userId);
        }
    }
}
