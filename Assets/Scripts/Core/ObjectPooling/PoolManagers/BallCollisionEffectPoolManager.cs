using System.Collections;
using MonoModels;
using UnityEngine;

namespace Scripts.Core.ObjectPooling
{
    public class BallCollisionEffectPoolManager : PoolManager
    {
        [SerializeField]
        private BallCollisionEffectMono ballCollisionEffectPrefab;

        [SerializeField]
        private float destroyDelay = 0.5f;

        private ObjectPool<BallCollisionEffectMono> _objectPool;
        
        public override void InitPool()
        {
            _objectPool = new ObjectPool<BallCollisionEffectMono>(ballCollisionEffectPrefab, transform, poolSize);
            _objectPool.InitPool();
        }

        public BallCollisionEffectMono GetObject() => _objectPool.GetObject();
        
        public void DestroyObject(BallCollisionEffectMono obj) => StartCoroutine(DestroyByDelay(obj));

        IEnumerator DestroyByDelay(BallCollisionEffectMono obj)
        {
            yield return new WaitForSeconds(destroyDelay);
            _objectPool.DestroyObject(obj);
        }
    }
}