using System.Collections.Generic;

namespace ProjectPSX.Util
{

    static class CollectionExtensions
    {

        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key) 
            => dict.TryGetValue(key, out var value) ? value : default(TValue);

    }

}
