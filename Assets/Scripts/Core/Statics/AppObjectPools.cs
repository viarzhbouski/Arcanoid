using System;
using System.Collections.Generic;
using Core.ObjectPooling.Pools;

namespace Core.Statics
{
    public class AppObjectPools
    {
        public static AppObjectPools Instance;
        
        public Dictionary<Type, PoolProvider> PoolProviders { get; private set; }

        public AppObjectPools()
        {
            if (Instance == null)
            {
                PoolProviders = new Dictionary<Type, PoolProvider>();
                Instance = this;
            }
        }

        public T GetObjectPool<T>() where T : PoolProvider
        {
            return (T)Instance.PoolProviders[typeof(T)];
        }
    }
}