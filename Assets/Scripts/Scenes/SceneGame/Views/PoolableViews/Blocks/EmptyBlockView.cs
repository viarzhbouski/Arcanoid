using Scenes.SceneGame.Boosts.Interfaces;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks
{
    public class EmptyBlockView : BaseBlockView
    {
        public override void SetBoost(IHasBoost boost)
        {
            return;
        }

        public override void DestroyBlock()
        {
            return;
        }
    }
}