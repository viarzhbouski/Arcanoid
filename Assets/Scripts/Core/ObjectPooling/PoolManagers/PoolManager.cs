using MonoModels;
using UnityEngine;

namespace Scripts.Core.ObjectPooling
{
    public abstract class PoolManager : MonoBehaviour
    {
        [SerializeField] 
        protected int poolSize;

        public abstract void InitPool();
    }
}