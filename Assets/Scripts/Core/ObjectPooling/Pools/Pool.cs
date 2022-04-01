using Core.ObjectPooling.Interfaces;
using UnityEngine;

namespace Core.ObjectPooling.Pools
{
    public class Pool<T> : PoolProvider where T : IPoolable
    {
        [SerializeField]
        public int poolSize;
        
        [SerializeField]
        private T prefab;
        
        protected ObjectPool<T> ObjectPool;

        public override void Init()
        {
            ObjectPool = new ObjectPool<T>(prefab, transform, poolSize);
            ObjectPool.InitPool();
        }

        public T GetObject() => ObjectPool.GetObject();

        public virtual void DestroyPoolObject(T obj) => ObjectPool.DestroyPoolObject(obj);
        
        public void ClearPool() => ObjectPool.ClearPool();
    }
}