using System;
using Template10.Services.Settings;

namespace Tenplex.Services
{
    public class ServerConnectionInfoService
    {
        private ISettingsHelper _settingsHelper;

        public ServerConnectionInfoService(ISettingsHelper settingsHelper)
        {
            _settingsHelper = settingsHelper ?? throw new ArgumentNullException(nameof(settingsHelper));
        }

        public string GetPlexAccessToken()
        {
            var plexAccessToken = string.Empty;
            _settingsHelper.TryReadString("PLEX_ACCESS_TOKEN", out plexAccessToken);
            return plexAccessToken;
        }

        public string GetServerIpAddress()
        {
            var serverIpAddress = string.Empty;
            _settingsHelper.TryReadString("SERVER_IP_ADDRESS", out serverIpAddress);
            return serverIpAddress;
        }

        public string GetServerPortNumber()
        {
            var serverPortNumber = string.Empty;
            _settingsHelper.TryReadString("SERVER_PORT_NUMBER", out serverPortNumber);
            return serverPortNumber;
        }

        public bool HasConnectionInfo()
        {
            var plexAccessToken = GetPlexAccessToken();
            var serverIpAddress = GetServerIpAddress();
            var serverPortNumber = GetServerPortNumber();

            return !string.IsNullOrWhiteSpace(plexAccessToken) &&
                !string.IsNullOrWhiteSpace(serverIpAddress) &&
                !string.IsNullOrWhiteSpace(serverPortNumber);
        }

        public void SetPlexAccessToken(string plexAccessToken)
        {
            if (string.IsNullOrWhiteSpace(plexAccessToken))
                throw new ArgumentNullException(nameof(plexAccessToken));

            _settingsHelper.WriteString("PLEX_ACCESS_TOKEN", plexAccessToken);
        }

        public void SetServerIpAddress(string serverIpAddress)
        {
            if (string.IsNullOrWhiteSpace(serverIpAddress))
                throw new ArgumentNullException(nameof(serverIpAddress));

            _settingsHelper.WriteString("SERVER_IP_ADDRESS", serverIpAddress);
        }

        public void SetServerPortNumber(string serverPortNumber)
        {
            if (string.IsNullOrWhiteSpace(serverPortNumber))
                throw new ArgumentNullException(nameof(serverPortNumber));

            _settingsHelper.WriteString("SERVER_PORT_NUMBER", serverPortNumber);
        }
    }
}
