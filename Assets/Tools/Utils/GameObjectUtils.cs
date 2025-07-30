using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Utils
{
    public static class GameObjectUtils
    {
        /// <summary>
        /// 实例化预制体列表
        /// </summary>
        /// <typeparam name="T">引用预制体使用的类型</typeparam>
        /// <param name="prefabs">预制体列表</param>
        /// <returns>游戏物体列表</returns>
        public static List<T> MultipleInstatiate<T>(List<T> prefabs) where T : MonoBehaviour
        {
            List<T> list = new List<T>();
            foreach (T prefab in prefabs)
            {
                list.Add(MonoBehaviour.Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<T>());
            }
            return list;
        }

        /// <summary>
        /// 实例化预制体列表
        /// </summary>
        /// <typeparam name="T">引用预制体使用的类型</typeparam>
        /// <param name="prefabs">预制体列表</param>
        /// <param name="parent">父物体</param>
        /// <returns>游戏物体列表</returns>
        public static List<T> MultipleInstatiate<T>(List<T> prefabs, Transform parent) where T : MonoBehaviour
        {
            List<T> list = new List<T>();
            foreach (T prefab in prefabs)
            {
                list.Add(MonoBehaviour.Instantiate(prefab, parent).GetComponent<T>());
            }
            return list;
        }

        /// <summary>
        /// 实例化预制体列表
        /// </summary>
        /// <typeparam name="T">引用预制体使用的类型</typeparam>
        /// <param name="prefabs">预制体列表</param>
        /// <returns>游戏物体列表</returns>
        public static List<GameObject> MultipleInstatiate(List<GameObject> prefabs)
        {
            List<GameObject> list = new List<GameObject>();
            foreach (GameObject prefab in prefabs)
            {
                list.Add(MonoBehaviour.Instantiate(prefab, Vector3.zero, Quaternion.identity));
            }
            return list;
        }

        public static void InvertedDestroy<T>(this List<T> list) where T : MonoBehaviour
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Object.Destroy(list[i].gameObject);
            }

            list.Clear();
        }

        public static void InvertedDestroy(this List<GameObject> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Object.Destroy(list[i]);
            }

            list.Clear();
        }
    }
}