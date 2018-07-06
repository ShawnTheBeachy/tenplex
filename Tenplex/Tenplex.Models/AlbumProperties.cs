using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Tenplex.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class AlbumProperties : BindableBase
    {
        private Dictionary<string, string> _changedValues = new Dictionary<string, string>();
        private bool _isTracking = false;

        #region Album

        private string _album;
        [JsonProperty("title")]
        public string Album
        {
            get => _album;
            set
            {
                if (Set(ref _album, value))
                    TrackChange("title", value);
            }
        }

        #endregion Album

        #region Artist

        private string _artist;
        [JsonProperty("parentTitle")]
        public string Artist
        {
            get => _artist;
            set
            {
                if (Set(ref _artist, value))
                    TrackChange("parentTitle", value);
            }
        }

        #endregion Artist

        #region OriginallyAvailable

        private DateTime _originallyAvailable;
        [JsonProperty("originallyAvailableAt")]
        public DateTime OriginallyAvailable
        {
            get => _originallyAvailable;
            set
            {
                if (Set(ref _originallyAvailable, value))
                    TrackChange("originallyAvailableAt", value.ToString("yyyy-MM-dd"));
            }
        }

        #endregion OriginallyAvailable

        #region Rating

        private int _rating;
        [JsonProperty("userRating")]
        public int Rating
        {
            get => _rating;
            set
            {
                if (Set(ref _rating, value))
                    TrackChange("userRating", value.ToString());
            }
        }

        #endregion Rating

        #region RatingKey

        private string _ratingKey;
        [JsonProperty("ratingKey")]
        public string RatingKey { get => _ratingKey; set => Set(ref _ratingKey, value); }

        #endregion RatingKey

        #region RecordLabel

        private string _recordLabel;
        [JsonProperty("studio")]
        public string RecordLabel
        {
            get => _recordLabel;
            set
            {
                if (Set(ref _recordLabel, value))
                    TrackChange("studio", value);
            }
        }

        #endregion RecordLabel

        #region Review

        private string _review;
        [JsonProperty("summary")]
        public string Review
        {
            get => _review;
            set
            {
                if (Set(ref _review, value))
                    TrackChange("summary", value);
            }
        }

        #endregion Review

        #region SortAlbum

        private string _sortAlbum;
        [JsonProperty("titleSort")]
        public string SortAlbum
        {
            get => _sortAlbum;
            set
            {
                if (Set(ref _sortAlbum, value))
                    TrackChange("titleSort", value);
            }
        }

        #endregion SortAlbum

        #region Thumb

        private string _thumb;
        [JsonProperty("thumb")]
        public string Thumb
        {
            get => _thumb;
            set
            {
                if (Set(ref _thumb, value))
                    TrackChange("thumb", value);
            }
        }

        #endregion Thumb

        public Dictionary<string, string> GetTrackedChanges()
        {
            return _changedValues;
        }

        public void StartTracking()
        {
            _isTracking = true;
        }

        public void StopTracking()
        {
            _isTracking = false;
        }

        private void TrackChange(string key, string value)
        {
            if (!_isTracking)
                return;

            _changedValues[$"{key}.value"] = value;
        }
    }
}
