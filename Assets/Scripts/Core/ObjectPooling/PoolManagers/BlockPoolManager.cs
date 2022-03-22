using Scenes.SceneGame.Views;
using UnityEngine;

namespace Scripts.Core.ObjectPooling
{
    public class BlockPoolManager : PoolManager
    {
        [SerializeField]
        private BlockView prefab;

        private ObjectPool<BlockView> _objectPool;
        
        public override void InitPool()
        {
            _objectPool = new ObjectPool<BlockView>(prefab, transform, poolSize);
            _objectPool.InitPool();
        }

        public override void ClearPool() => _objectPool.ClearPool();

        public BlockView GetObject() => _objectPool.GetObject();
        
        public void DestroyObject(BlockView obj) => _objectPool.DestroyObject(obj);
    }
}