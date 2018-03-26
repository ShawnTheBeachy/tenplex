using Newtonsoft.Json;

namespace Tenplex.Models
{
    /// <summary>
    /// An available view for a library (e.g. "By Album", "By Year").
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class View : BindableBase
    {
        #region Key

        private string _key = default(string);
        /// <summary>
        /// The key to be used in API requests.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get => _key; set => Set(ref _key, value); }

        #endregion Key

        #region Title

        private string _title = default(string);
        /// <summary>
        /// The title of the view.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion Title
    }
}
