using Scenes.SceneGame.Boosts.Interfaces;
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
            var bonusBoostViewObject = Object.Instantiate(bonusBoostView, _blockPosition, Quaternion.identity);
            bonusBoostViewObject.Init(_bonusBoost);
        }
    }
}