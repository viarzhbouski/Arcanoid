using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.Views.PoolableViews.Blocks.BonusBoost;
using UnityEngine;

namespace Scenes.SceneGame.Boosts
{
    public class CaptiveBallBoost : IHasBoost
    {
        public void ExecuteBoost(BonusBoostView bonusBoost)
        {
            SpawnBalls();
        }

        private void SpawnBalls()
        {
            var boostConfig = AppConfig.Instance.BoostsConfig;
            
            for (var i = 0; i < boostConfig.BallCount; i++)
            {
                var captiveBallView = Object.Instantiate(boostConfig.BallPrefab);
                captiveBallView.Init();
            }
        }
    }
}