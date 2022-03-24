using Core.ObjectPooling;
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
            ObjectPools.Instance.GetObjectPool<GraniteBlockPool>()
                .DestroyPoolObject(this);
        }
    }
}