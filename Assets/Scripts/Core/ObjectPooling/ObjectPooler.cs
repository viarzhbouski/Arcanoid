using System.Collections.Generic;
using Common.Enums;
using UnityEngine;

namespace Scripts.Core.ObjectPooling
{
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler Instance;
        
        private Dictionary<ObjectType, Pool> _pools;
        private List<PoolableObject> _poolableObjects;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        
        public void InitPool(List<PoolableObject> poolableObjects)
        {
            _pools = new Dictionary<ObjectType, Pool>();
            _poolableObjects = poolableObjects;
            
            var empty = new GameObject();

            foreach (var item in _poolableObjects)
            {
                var container = Instantiate(empty, transform, false);
                container.name = item.ObjectType.ToString();
            
                _pools[item.ObjectType] = new Pool(container.transform);
            
                for (var i = 0; i < item.ItemsCount; i++)
                {
                    var go = InstantiateObject(item.ObjectType, container.transform);
                    _pools[item.ObjectType].Objects.Enqueue(go);
                }
            }
            
            Destroy(empty);
        }

        private GameObject InstantiateObject(ObjectType objectType, Transform parent)
        {
            var poolableObject = Instantiate(_poolableObjects.Find(x => x.ObjectType == objectType), parent);
            var go = poolableObject.gameObject;
            
            go.SetActive(false);
            
            return go;
        }
        
        public GameObject GetObject(ObjectType objectType)
        {
            var go = _pools[objectType].Objects.Count > 0
                ? _pools[objectType].Objects.Dequeue()
                : InstantiateObject(objectType, _pools[objectType].Transform);
            
            go.SetActive(true);
        
            return go;
        }
        
        public void DestroyObject(GameObject obj)
        {
            _pools[obj.GetComponent<PoolableObject>().ObjectType].Objects.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}