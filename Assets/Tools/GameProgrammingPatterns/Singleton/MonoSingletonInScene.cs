using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.GameProgrammingPatterns.Singleton
{
    public class MonoSingletonInScene<T> : MonoBehaviour where T : MonoSingletonInScene<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                Object @lock = m_lock;
                lock (@lock)
                {
                    if (instance == null)
                    {
                        instance = FindAnyObjectByType<T>();

                        if (instance == null )
                        {
                            // instance = new GameObject(typeof(T) + "SingletonManager").AddComponent<T>();
                            // instance.Init();
                            // Debug.LogError("No " + typeof(T) + " in this scene, please create the game object first.");
                        }

                        // DontDestroyOnLoad(instance.gameObject);
                    }
                }
                
                return instance;
            }
        }

        private static Object m_lock = new Object();

        public static bool IsValid => MonoSingletonInScene<T>.instance != null;

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this as T;
            Init();
            //DontDestroyOnLoad(instance.gameObject);
        }

        public static void InitSubsystemRegistration()
        {
            MonoSingletonInScene<T>.instance = default(T);
            MonoSingletonInScene<T>.m_lock = new Object();
        }

        protected virtual void OnDestroy()
        {
            MonoSingletonInScene<T>.instance = default(T);
        }

        protected virtual void Init()
        {

        }
    }
}