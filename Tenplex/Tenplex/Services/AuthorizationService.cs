using System;
using System.Threading.Tasks;
using Template10.Services.Serialization;
using Template10.Services.Settings;
using Tenplex.Models;
using Windows.Web.Http;

namespace Tenplex.Services
{
    public class AuthorizationService
    {
        private ISerializationService _serializationService;
        private ISettingsHelper _settingsHelper;

        public AuthorizationService(ISerializationService serializationService, ISettingsHelper settingsHelper)
        {
            _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
            _settingsHelper = settingsHelper ?? throw new ArgumentNullException(nameof(settingsHelper));
        }

        public string GetAccessToken()
        {
            return _settingsHelper.ReadString("PLEX_ACCESS_TOKEN");
        }

        public bool IsAuthorized()
        {
            _settingsHelper.TryReadString("PLEX_ACCESS_TOKEN", out var accessToken);
            return !string.IsNullOrWhiteSpace(accessToken);
        }

        public async Task<AuthorizationResponse> SignInAsync(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Plex-Client-Identifier", "tenplex");
                client.DefaultRequestHeaders.Add("X-Plex-Product", "Tenplex");
                client.DefaultRequestHeaders.Add("X-Plex-Version", "1.0.0");

                var json = $@"{{""user[login]"":""{username}"",""user[password]"":""{password}""}}";
                var response = await client.PostAsync(new Uri("https://plex.tv/users/sign_in.json"), new HttpStringContent(json));
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return null;
                else
                    return _serializationService.Deserialize<AuthorizationResponse>(content);
            }
        }

        public void SetAccessToken(string accessToken)
        {
            _settingsHelper.WriteString("PLEX_ACCESS_TOKEN", accessToken);
        }
    }
}
