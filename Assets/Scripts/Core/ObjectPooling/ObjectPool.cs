using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Core.ObjectPooling
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Stack<T> _poolStack = new Stack<T>();
        private T _objectPrefab;
        private Transform _objectTransform;
        private int _poolSize;

        public ObjectPool(T objectPrefab, Transform objectTransform, int poolSize)
        {
            _objectPrefab = objectPrefab;
            _objectTransform = objectTransform;
            _poolSize = poolSize;
        }

        public void InitPool()
        {
            for (var i = 0; i < _poolSize; i++)
            {
                SpawnObject();
            }
        }

        public T GetObject()
        {
            var poolObject = _poolStack.Any() ? _poolStack.Pop() 
                                                : SpawnObject();

            poolObject.gameObject.SetActive(true);

            return poolObject;
        }
        
        public void DestroyObject(T obj)
        {
            _poolStack.Push(obj);
            obj.gameObject.SetActive(false);
        }

        private T SpawnObject()
        {
            var spawnedObject = Object.Instantiate(_objectPrefab, _objectTransform);
            spawnedObject.gameObject.SetActive(false);
            _poolStack.Push(spawnedObject);
            
            return spawnedObject;
        }
    }
}