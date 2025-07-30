using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Optimize.ObjectPool
{
    public class ObjectPool
    {
        private Queue<GameObject> _objectsQueue = new();

        private GameObject _templateObject;

        public ObjectPool(GameObject templateObject)
        {
            _templateObject = templateObject;
        }

        public GameObject GetObject()
        {
            return GetObject(null);
        }

        public GameObject GetObject(Transform transform)
        {
            GameObject obj;

            if (_objectsQueue.Count > 0)
            {
                obj = _objectsQueue.Dequeue();
                obj.transform.SetParent(transform);
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = Object.Instantiate(_templateObject, transform);
            }

            return obj;
        }

        public void RecycleObject(GameObject obj)
        {
            obj.gameObject.SetActive(false);

            obj.transform.SetParent(null);

            _objectsQueue.Enqueue(obj);
        }
    }

    public class ObjectPool<T> where T : MonoBehaviour, IPoolableBehaviour
    {
        private Queue<T> _objectsQueue = new();

        private T _templateObject;

        public ObjectPool(T templateObject)
        {
            _templateObject = templateObject;
        }

        public T GetObject()
        {
            return GetObject(null);
        }

        public T GetObject(Transform transform)
        {
            T obj;

            if (_objectsQueue.Count > 0)
            {
                obj = _objectsQueue.Dequeue();
                obj.transform.SetParent(transform);
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = Object.Instantiate(_templateObject, transform);
            }

            obj.OnGet(this);

            return obj;
        }

        public void RecycleObject(T obj)
        {
            obj.OnRecycle();

            obj.gameObject.SetActive(false);

            obj.transform.SetParent(null);

            _objectsQueue.Enqueue(obj);
        }
    }

    public interface IPoolableBehaviour
    {
        public void OnGet<T>(ObjectPool<T> pool) where T : MonoBehaviour, IPoolableBehaviour;

        public void OnRecycle();
    }
}