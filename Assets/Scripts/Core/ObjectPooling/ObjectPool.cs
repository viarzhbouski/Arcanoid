using System;
using System.Collections.Generic;
using System.Linq;
using Core.ObjectPooling.Interfaces;
using Core.Statics;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.ObjectPooling
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
            T poolObject;
            
            if (_poolStack.Any())
            {
                poolObject = _poolStack.Pop();
            }
            else
            {
                SpawnObject();
                poolObject = _poolStack.Pop();
            }
            
            poolObject.GetGameObject()
                      .SetActive(true);
            
            return poolObject;
        }

        public void ClearPool()
        {
            for (var i = 0; i < _objectTransform.childCount; i++)
            {
                var poolObject = _objectTransform.GetChild(i);
                poolObject.gameObject.SetActive(false);
                var obj = poolObject.GetComponent<T>();
                if (!_poolStack.Contains(obj))
                {
                    _poolStack.Push(poolObject.GetComponent<T>());
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
            var obj = spawnedObject.GetComponent<T>();
            
            spawnedObject.SetActive(false);
            _poolStack.Push(obj);
            return obj;
        }
    }
}