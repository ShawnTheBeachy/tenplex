using Newtonsoft.Json;

namespace Tenplex.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AuthorizationResponse : BindableBase
    {
        #region User

        private User _user;
        [JsonProperty("user")]
        public User User { get => _user; set => Set(ref _user, value); }

        #endregion User
    }
}
