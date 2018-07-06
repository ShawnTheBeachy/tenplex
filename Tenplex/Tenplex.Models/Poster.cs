using Newtonsoft.Json;

namespace Tenplex.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Poster : BindableBase
    {
        #region IsSelected

        private bool _isSelected;
        [JsonProperty("selected")]
        public bool IsSelected { get => _isSelected; set => Set(ref _isSelected, value); }

        #endregion IsSelected

        #region Key

        private string _key;
        [JsonProperty("key")]
        public string Key { get => _key; set => Set(ref _key, value); }

        #endregion Key

        #region Provider

        private string _provider;
        [JsonProperty("provider")]
        public string Provider { get => _provider; set => Set(ref _provider, value); }

        #endregion Provider

        #region RatingKey

        private string _ratingKey;
        [JsonProperty("ratingKey")]
        public string RatingKey { get => _ratingKey; set => Set(ref _ratingKey, value); }

        #endregion RatingKey

        #region Thumb

        private string _thumb;
        [JsonProperty("thumb")]
        public string Thumb { get => _thumb; set => Set(ref _thumb, value); }

        #endregion Thumb
    }
}
