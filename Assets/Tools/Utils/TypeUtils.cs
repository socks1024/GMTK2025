using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tools.Utils
{
    public static class TypeUtils
    {
        public static IEnumerable<PropertyInfo> GetSerializePropertyInfos(this Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        public static IEnumerable<FieldInfo> GetSerializeFieldInfos(this Type type)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.Public);
        }

        private static List<Assembly> _currentAssemblies;
        public static List<Assembly> CurrentAssemblies
        {
            get
            {
                if (_currentAssemblies is null)
                    _currentAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

                return _currentAssemblies;
            }
        }

        #region Get Derived Types

        public static List<Type> GetDerivedStructs(Type targetType)
        {
            var ret = new List<Type>();
            foreach (var assembly in CurrentAssemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsValueType && !type.IsAbstract && targetType.IsAssignableFrom(type))
                    {
                        ret.Add(type);
                    }
                }
            }

            return ret;
        }

        public static List<Type> GetDerivedClasses(Type targetType)
        {
            var ret = new List<Type>();
            foreach (var assembly in CurrentAssemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsClass && !type.IsAbstract && targetType.IsAssignableFrom(type))
                    {
                        ret.Add(type);
                    }
                }
            }

            return ret;
        }

        public static List<Type> GetDerivedInterfaces(Type targetType)
        {
            var ret = new List<Type>();
            foreach (var assembly in CurrentAssemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsInterface && !type.IsAbstract && targetType.IsAssignableFrom(type))
                    {
                        ret.Add(type);
                    }
                }
            }

            return ret;
        }

        public static List<Type> GetDerivedClassesFromGenericInterfaces(Type interfaceType)
        {
            var ret = new List<Type>();
            foreach (var currentAssembly in CurrentAssemblies)
            {
                var types = currentAssembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsClass && type.GetInterfaces().Any(i =>
                            i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
                    {
                        ret.Add(type);
                    }
                }
            }

            return ret;
        }

        public static List<Type> GetDerivedClassesFromGenericClass(Type targetType)
        {
            var ret = new List<Type>();
            foreach (var assembly in CurrentAssemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsClass && !type.IsAbstract && type.BaseType.IsGenericType &&
                        targetType.IsAssignableFrom(type.BaseType.GetGenericTypeDefinition()))
                    {
                        ret.Add(type);
                    }
                }
            }

            return ret;
        }

        #endregion
        
        public static T CloneSerializeFields<T>(this T target)
        {
            var type = typeof(T);

            var ret = (object)Activator.CreateInstance<T>();

            var fields = type.GetSerializeFieldInfos();

            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsGenericType)
                {
                    if (typeof(IList).IsAssignableFrom(fieldInfo.FieldType))
                    {
                        fieldInfo.SetValue
                        (
                            ret,
                            ListUtils.CloneList
                            (
                                (IList)fieldInfo.GetValue(target),
                                fieldInfo.FieldType.GenericTypeArguments[0]
                            )
                        );
                    }
                    else if (typeof(IDictionary).IsAssignableFrom(fieldInfo.FieldType))
                    {
                        fieldInfo.SetValue
                        (
                            ret,
                            DictionaryUtils.CloneDictionary
                            (
                                (IDictionary)fieldInfo.GetValue(target),
                                fieldInfo.FieldType.GenericTypeArguments[0],
                                fieldInfo.FieldType.GenericTypeArguments[1]
                            )
                        );
                    }
                }
                else
                {
                    fieldInfo.SetValue(ret, fieldInfo.GetValue(target));
                }
            }

            return (T)ret;
        }
    }
}