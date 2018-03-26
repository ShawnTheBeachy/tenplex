using Newtonsoft.Json;

namespace Tenplex.Models
{
    /// <summary>
    /// An option by which media can be sorted.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Sort : BindableBase
    {
        #region AscendingKey

        private string _ascendingKey = default(string);
        /// <summary>
        /// The key to be used for ascending sorts.
        /// </summary>
        [JsonProperty("key")]
        public string AscendingKey { get => _ascendingKey; set => Set(ref _ascendingKey, value); }

        #endregion AscendingKey

        #region DefaultDirection

        private string _defaultDirection = default(string);
        /// <summary>
        /// The default direction of this sort. One of "asc" or "desc".
        /// </summary>
        [JsonProperty("defaultDirection")]
        public string DefaultDirection { get => _defaultDirection; set => Set(ref _defaultDirection, value); }

        #endregion DefaultDirection

        #region DescendingKey

        private string _descendingKey = default(string);
        /// <summary>
        /// The key to be used for descending sorts.
        /// </summary>
        [JsonProperty("descKey")]
        public string DescendingKey { get => _descendingKey; set => Set(ref _descendingKey, value); }

        #endregion DescendingKey

        #region Title

        private string _title = default(string);
        /// <summary>
        /// The title of this sort.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion Title
    }
}
