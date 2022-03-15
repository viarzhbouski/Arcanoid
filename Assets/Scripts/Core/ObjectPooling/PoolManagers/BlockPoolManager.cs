using MonoModels;
using UnityEngine;

namespace Scripts.Core.ObjectPooling
{
    public class BlockPoolManager : PoolManager
    {
        [SerializeField]
        private BlockMono prefab;

        private ObjectPool<BlockMono> _objectPool;
        
        public override void InitPool()
        {
            _objectPool = new ObjectPool<BlockMono>(prefab, transform, poolSize);
            _objectPool.InitPool();
        }

        public BlockMono GetObject() => _objectPool.GetObject();
        
        public void DestroyObject(BlockMono obj) => _objectPool.DestroyObject(obj);
    }
}