using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tenplex.Models.JsonConverters;

namespace Tenplex.Models
{
    /// <summary>
    /// A section of the user's library (e.g. "Music", "TV Shows").
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class LibrarySection : BindableBase
    {
        #region Agent

        private string _agent = default(string);
        /// <summary>
        /// The metadata agent which is used for this section.
        /// </summary>
        [JsonProperty("agent")]
        public string Agent { get => _agent; set => Set(ref _agent, value); }

        #endregion Agent

        #region AllowSync

        private bool _allowSync = default(bool);
        /// <summary>
        /// Whether the user is allowed to sync this section to their device.
        /// </summary>
        [JsonProperty("allowSync")]
        public bool AllowSync { get => _allowSync; set => Set(ref _allowSync, value); }

        #endregion AllowSync

        #region Art

        private string _art = default(string);
        /// <summary>
        /// The URL to the artwork for this section.
        /// </summary>
        [JsonProperty("art")]
        public string Art { get => _art; set => Set(ref _art, value); }

        #endregion Art

        #region Composite

        private string _composite = default(string);
        /// <summary>
        /// The URL to the auto-generated composite artwork for this section.
        /// </summary>
        [JsonProperty("composite")]
        public string Composite { get => _composite; set => Set(ref _composite, value); }

        #endregion Composite

        #region CreatedAt

        private DateTime _createdAt = default(DateTime);
        /// <summary>
        /// The date and time when this section was created.
        /// </summary>
        // [JsonProperty("createdAt")]   //TODO: Use custom converter.
        public DateTime CreatedAt { get => _createdAt; set => Set(ref _createdAt, value); }

        #endregion CreatedAt

        #region Filters

        private bool _filters = default(bool);
        /// <summary>
        /// Whether there are filters for this section.
        /// </summary>
        [JsonProperty("filters")]
        public bool Filters { get => _filters; set => Set(ref _filters, value); }

        #endregion Filters

        #region Key

        private string _key = default(string);
        /// <summary>
        /// The key for this section.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get => _key; set => Set(ref _key, value); }

        #endregion Key

        #region Language

        private string _language = default(string);
        /// <summary>
        /// The language of this section.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get => _language; set => Set(ref _language, value); }

        #endregion Language

        #region Locations

        private ICollection<Location> _locations = new HashSet<Location>();
        /// <summary>
        /// The locations which this section scans for media.
        /// </summary>
        [JsonProperty("Location")]
        public ICollection<Location> Locations { get => _locations; set => Set(ref _locations, value); }

        #endregion Locations

        #region Refreshing

        private bool _refreshing = default(bool);
        /// <summary>
        /// Whether this section is currently being refreshed.
        /// </summary>
        [JsonProperty("refreshing")]
        public bool Refreshing { get => _refreshing; set => Set(ref _refreshing, value); }

        #endregion Refreshing

        #region ScannedAt

        private DateTime _scannedAt = default(DateTime);
        /// <summary>
        /// The date and time when this section was last scanned.
        /// </summary>
        // [JsonProperty("scannedAt")]   //TODO: Use custom converter.
        public DateTime ScannedAt { get => _scannedAt; set => Set(ref _scannedAt, value); }

        #endregion ScannedAt

        #region Scanner

        private string _scanner = default(string);
        /// <summary>
        /// The scanner which is used for this section.
        /// </summary>
        [JsonProperty("scanner")]
        public string Scanner { get => _scanner; set => Set(ref _scanner, value); }

        #endregion Scanner

        #region Thumb

        private string _thumb = default(string);
        /// <summary>
        /// The URL to the thumbnail for this section.
        /// </summary>
        [JsonProperty("thumb")]
        public string Thumb { get => _thumb; set => Set(ref _thumb, value); }

        #endregion Thumb

        #region Title

        private string _title = default(string);
        /// <summary>
        /// The title of this section.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion Title

        #region Type

        private LibrarySectionType _type = default(LibrarySectionType);
        /// <summary>
        /// The type of this section.
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(LibrarySectionTypeConverter))]
        public LibrarySectionType Type { get => _type; set => Set(ref _type, value); }

        #endregion Type

        #region UpdatedAt

        private DateTime _updatedAt = default(DateTime);
        /// <summary>
        /// The date and time when this section was last updated.
        /// </summary>
        // [JsonProperty("updatedAt")]      //TODO: Use custom converter.
        public DateTime UpdatedAt { get => _updatedAt; set => Set(ref _updatedAt, value); }

        #endregion UpdatedAt

        #region UUID

        private Guid _uuid = default(Guid);
        /// <summary>
        /// The unique identifier for this section.
        /// </summary>
        [JsonProperty("uuid")]
        public Guid UUID { get => _uuid; set => Set(ref _uuid, value); }

        #endregion UUID
    }
}
