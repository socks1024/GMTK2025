using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.GameProgrammingPatterns.Singleton
{
    /// <summary>
    /// 非MonoScript单例基类
    /// </summary>
    /// <typeparam name="T">继承类的类型</typeparam>
    public abstract class BaseSingleton<T> where T : class, new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new T();
                        }
                    }
                }

                return instance;
            }
        }

        protected static readonly object instanceLock = new object();
        
    }
}