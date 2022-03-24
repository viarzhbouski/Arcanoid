using UnityEngine;

namespace Scripts.Core.ObjectPooling
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

        public virtual T GetObject() => ObjectPool.GetObject();

        public virtual void DestroyPoolObject(T obj) => ObjectPool.DestroyPoolObject(obj);
        
        public virtual void ClearPool() => ObjectPool.ClearPool();
    }
}