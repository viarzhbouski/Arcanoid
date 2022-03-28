using Scenes.SceneGame.Views.PoolableViews.Blocks.BonusBoost;
using UnityEngine;

namespace Scenes.SceneGame.Boosts.Interfaces
{
    public interface IHasBoost
    {
        public void ExecuteBoost(BonusBoostView bonusBoost = null);
    }
}