using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.ScenePools;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks
{
    public class GraniteBlockView : BaseBlockView
    {
        public override void SetBoost(IHasBoost boost)
        {
            return;
        }
        
        public override void DestroyBlock()
        {
            var objectPool = AppObjectPools.Instance.GetObjectPool<BlockDestroyEffectPool>();
            var blockDestroyEffect = objectPool.GetObject();
            blockDestroyEffect.transform.position = transform.position;
            objectPool.DestroyPoolObject(blockDestroyEffect);
            AppObjectPools.Instance.GetObjectPool<GraniteBlockPool>()
                .DestroyPoolObject(this);
        }
    }
}