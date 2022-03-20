using System;
using System.Collections.Generic;

namespace Scripts.Core.ObjectPooling
{
    public class ObjectPools
    {
        public static ObjectPools Instance;
        
        public Dictionary<Type, PoolManager> PoolManagers { get; private set; }

        public ObjectPools()
        {
            if (Instance == null)
            {
                PoolManagers = new Dictionary<Type, PoolManager>();
                Instance = this;
            }
        }

        public T GetObjectPool<T>() where T : PoolManager
        {
            return (T)Instance.PoolManagers[typeof(T)];
        }
    }
}