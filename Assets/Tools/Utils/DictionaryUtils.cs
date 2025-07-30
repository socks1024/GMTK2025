using System;
using System.Collections;
using System.Collections.Generic;

namespace Tools.Utils
{
    public static class DictionaryUtils
    {
        public static IDictionary<TKey, TValue> CloneDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dic)
        {
            var ret = (IDictionary<TKey, TValue>)Activator.CreateInstance(typeof(Dictionary<TKey, TValue>));

            foreach (var o in dic)
            {
                ret.Add(o.Key, o.Value);
            }

            return ret;
        }

        public static IDictionary CloneDictionary(IDictionary dic, Type keyType, Type valType)
        {
            var ret = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(keyType, valType));

            foreach (DictionaryEntry o in dic)
            {
                ret.Add(o.Key, o.Value);
            }

            return ret;
        }
    }
}