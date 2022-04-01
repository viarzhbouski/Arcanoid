using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views.PoolableViews.Blocks.BonusBoost;
using UnityEngine;

namespace Scenes.SceneGame.Boosts
{
    public class BonusBoost : IHasBoost
    {
        private readonly IHasBonusBoost _bonusBoost;
        private readonly Vector2 _blockPosition;

        public BonusBoost(IHasBonusBoost bonusBoost, Vector2 blockPosition)
        {
            _bonusBoost = bonusBoost;
            _blockPosition = blockPosition;
        }
        
        public void ExecuteBoost(BonusBoostView bonusBoostView)
        {
            var bonusBoostPool = AppObjectPools.Instance.GetObjectPool<BonusBoostPool>();
            var bonusBoostViewObject = bonusBoostPool.GetObject();
            bonusBoostViewObject.transform.position = _blockPosition;
            bonusBoostViewObject.Init(_bonusBoost);
        }
    }
}