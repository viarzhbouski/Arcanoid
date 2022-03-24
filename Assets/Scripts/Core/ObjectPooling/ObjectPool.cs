using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Core.ObjectPooling
{
    public class ObjectPool<T> where T : IPoolable
    {
        private readonly Stack<T> _poolStack = new Stack<T>();
        private readonly T _objectPrefab;
        private readonly Transform _objectTransform;
        private readonly int _poolSize;

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
        
            poolObject.GetGameObject()
                      .SetActive(true);
            
            return poolObject;
        }
        
        public void ClearPool()
        {
            for (var i = 0; i < _objectTransform.childCount; i++)
            {
                var poolObject = _objectTransform.GetChild(i);
                if (poolObject.gameObject.activeSelf)
                {
                    poolObject.gameObject.SetActive(false);
                }
            }
        }
        
        public void DestroyPoolObject(T obj)
        {
            _poolStack.Push(obj);
            obj.GetGameObject()
                .SetActive(false);
        }
        
        private T SpawnObject()
        {
            var spawnedObject = Object.Instantiate(_objectPrefab.GetGameObject(), _objectTransform);
            spawnedObject.SetActive(false);

            var obj = spawnedObject.GetComponent<T>();
            _poolStack.Push(obj);
            
            return obj;
        }
    }
}