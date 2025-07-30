using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tools.Utils
{
    public static class ListUtils
    {
        public static T GetRandomListElement<T>(this List<T> list)
        {
            return list[new Random().Next(list.Count)];
        }

        public static void InvertedForEach<T>(this List<T> list, Action<T> action)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                action.Invoke(list[i]);
            }
        }

        public static IList<T> CloneList<T>(this IList<T> list)
        {
            var ret = (IList<T>)Activator.CreateInstance(typeof(List<T>));

            foreach (var o in list)
            {
                ret.Add(o);
            }

            return ret;
        }

        public static IList CloneList(IList list, Type elementType)
        {
            var ret = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));

            foreach (var o in list)
            {
                ret.Add(o);
            }

            return ret;
        }

        public static bool AddIfNotExist<T>(this List<T> list, T element)
        {
            if (list.Contains(element)) return false;
            list.Add(element);
            return true;
        }

        public static bool AddIfNotExist<T>(this List<T> list, T element, Func<T, T, bool> equalFunc)
        {
            if (list.Any((ele) => equalFunc != null && equalFunc.Invoke(ele, element))) return false;
            list.Add(element);
            return true;
        }
    }
}