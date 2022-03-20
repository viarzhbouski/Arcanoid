using System.Collections;
using MonoModels;
using UnityEngine;

namespace Scripts.Core.ObjectPooling
{
    public class BallCollisionEffectPoolManager : PoolManager
    {
        [SerializeField]
        private BallCollisionEffectView ballCollisionEffectPrefab;

        [SerializeField]
        private float destroyDelay = 0.5f;

        private ObjectPool<BallCollisionEffectView> _objectPool;
        
        public override void InitPool()
        {
            _objectPool = new ObjectPool<BallCollisionEffectView>(ballCollisionEffectPrefab, transform, poolSize);
            _objectPool.InitPool();
        }

        public BallCollisionEffectView GetObject() => _objectPool.GetObject();
        
        public void DestroyObject(BallCollisionEffectView obj) => StartCoroutine(DestroyByDelay(obj));

        IEnumerator DestroyByDelay(BallCollisionEffectView obj)
        {
            yield return new WaitForSeconds(destroyDelay);
            _objectPool.DestroyObject(obj);
        }
    }
}