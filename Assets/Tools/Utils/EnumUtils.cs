using System;
using System.Collections.Generic;

namespace Tools.Utils
{
    public static class EnumUtils
    {
        public static IEnumerable<string> GetEnumNames(Type type)
        {
            var list = new List<string>();
            foreach (var value in Enum.GetValues(type))
            {
                list.Add(value.ToString());
            }

            return list;
        }

        public static T GetRandomEnumValue<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(new Random().Next(values.Length));
        }

        public static List<T> GetRandomEnumValues<T>(int length) where T : Enum
        {
            List<T> arrows = new();
            for (int i = 0; i < length; i++)
            {
                arrows.Add(GetRandomEnumValue<T>());
            }
            return arrows;
        }
    }
}