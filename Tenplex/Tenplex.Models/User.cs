using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Tenplex.Models
{
    [JsonObject(MemberSerialization.OptIn), XmlType("User")]
    public class User : BindableBase
    {
        #region AuthToken

        private string _authToken;
        [JsonProperty("authToken"), XmlAttribute("authToken")]
        public string AuthToken { get => _authToken; set => Set(ref _authToken, value); }

        #endregion AuthToken

        #region Id

        private string _id;
        [XmlAttribute("id")]
        public string Id { get => _id; set => Set(ref _id, value); }

        #endregion Id

        #region Thumb

        private string _thumb;
        [XmlAttribute("thumb")]
        public string Thumb { get => _thumb; set => Set(ref _thumb, value); }

        #endregion Thumb

        #region Title

        private string _title;
        [XmlAttribute("title")]
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion Title

        #region UniqueId

        private string _uniqueId;
        [XmlAttribute("uuid")]
        public string UniqueId { get => _uniqueId; set => Set(ref _uniqueId, value); }

        #endregion UniqueId
    }
}
