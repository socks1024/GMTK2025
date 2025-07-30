using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.GameProgrammingPatterns.Singleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                Object @lock = MonoSingleton<T>.m_lock;
                lock (@lock)
                {
                    if (instance == null)
                    {
                        instance = FindAnyObjectByType<T>();

                        if (instance == null )
                        {
                            instance = new GameObject(typeof(T) + "SingletonManager").AddComponent<T>();
                            instance.Init();
                        }

                        DontDestroyOnLoad(instance.gameObject);
                    }
                }
                
                return instance;
            }
        }

        private static Object m_lock = new Object();

        public static bool IsValid => MonoSingleton<T>.instance != null;

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }

        public static void InitSubsystemRegistration()
        {
            MonoSingleton<T>.instance = default(T);
            MonoSingleton<T>.m_lock = new Object();
        }

        protected void OnDestroy()
        {
            MonoSingleton<T>.instance = default(T);
        }

        protected virtual void Init()
        {

        }
    }
}