using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Views.PoolableViews.Blocks.BonusBoost;
using UnityEngine;

namespace Scenes.SceneGame.Boosts
{
    public class BonusBoost : IHasBoost
    {
        private IHasBonusBoost _bonusBoost;

        public BonusBoost(IHasBonusBoost bonusBoost)
        {
            _bonusBoost = bonusBoost;
        }
        
        public void ExecuteBoost(BonusBoostView bonusBoost)
        {
            var bonusObject = Object.Instantiate(bonusBoost);
            bonusObject.Init(_bonusBoost);
        }
    }
}