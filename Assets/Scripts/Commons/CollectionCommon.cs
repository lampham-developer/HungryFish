using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commons.CollectionCommon
{
    public static class CollectionCommon
    {

        public static V GetValue<K,V>(this Dictionary<K,V> dictionary, K key)
        {
            V value = default(V);
            dictionary.TryGetValue(key,out value);
            return value;
        }

    }
}