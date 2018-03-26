using System.Collections.Generic;
using Tenplex.Models;

namespace Tenplex.Comparers
{
    class TrackComparer : IEqualityComparer<Track>
    {
        public bool Equals(Track x, Track y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Track obj)
        {
            return obj.GetHashCode();
        }
    }
}
