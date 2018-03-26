using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tenplex.Models.JsonConverters;

namespace Tenplex.Models
{
    /// <summary>
    /// A track in the library.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Track : BindableBase
    {
        #region Disc

        private int _disc = 1;
        /// <summary>
        /// The number of the disc to which this track belongs.
        /// </summary>
        [JsonProperty("parentIndex")]
        public int Disc { get => _disc; set => Set(ref _disc, value); }

        #endregion Disc

        #region Duration

        private TimeSpan _duration = default(TimeSpan);
        /// <summary>
        /// The duration of this track.
        /// </summary>
        [JsonConverter(typeof(MillisecondsToTimeSpan)), JsonProperty("duration")]
        public TimeSpan Duration { get => _duration; set => Set(ref _duration, value); }

        #endregion Duration

        #region Key

        private string _key = default(string);
        /// <summary>
        /// The key for this track.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get => _key; set => Set(ref _key, value); }

        #endregion Key

        #region Media

        private ICollection<Media> _media = new HashSet<Media>();
        [JsonProperty("Media")]
        public ICollection<Media> Media { get => _media; set => Set(ref _media, value); }

        #endregion Media

        #region Title

        private string _title = default(string);
        /// <summary>
        /// The title of this track.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion Title

        #region TrackNumber

        private int _trackNumber = default(int);
        /// <summary>
        /// The order of this track in its parent album.
        /// </summary>
        [JsonProperty("index")]
        public int TrackNumber { get => _trackNumber; set => Set(ref _trackNumber, value); }

        #endregion TrackNumber
    }
}
