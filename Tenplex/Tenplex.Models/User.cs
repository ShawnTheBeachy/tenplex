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

        #region HasPassword

        private bool _hasPassword;
        [XmlAttribute("hasPassword")]
        public bool HasPassword { get => _hasPassword; set => Set(ref _hasPassword, value); }

        #endregion HasPassword

        #region Id

        private string _id;
        [XmlAttribute("id")]
        public string Id { get => _id; set => Set(ref _id, value); }

        #endregion Id

        #region IsAdmin

        private bool _isAdmin;
        [XmlAttribute("admin")]
        public bool IsAdmin { get => _isAdmin; set => Set(ref _isAdmin, value); }

        #endregion IsAdmin

        #region IsProtected

        private bool _isProtected;
        [XmlAttribute("protected")]
        public bool IsProtected { get => _isProtected; set => Set(ref _isProtected, value); }

        #endregion IsProtected

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
