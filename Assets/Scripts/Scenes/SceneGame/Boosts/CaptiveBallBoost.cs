using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views.PoolableViews.Blocks.BonusBoost;
using UnityEngine;

namespace Scenes.SceneGame.Boosts
{
    public class CaptiveBallBoost : IHasBoost
    {
        private readonly Transform _boostBlockTransform;
        
        public CaptiveBallBoost(Transform boostBlockTransform)
        {
            _boostBlockTransform = boostBlockTransform;
        }
        public void ExecuteBoost(BonusBoostView bonusBoost)
        {
            SpawnBalls();
        }

        private void SpawnBalls()
        {
            var boostConfig = AppConfig.Instance.BoostsConfig;
            
            for (var i = 0; i < boostConfig.BallCount; i++)
            {
                var ballPool = AppObjectPools.Instance.GetObjectPool<CaptiveBallPool>();
                var ballObject = ballPool.GetObject();
                ballObject.transform.position = _boostBlockTransform.position;
                ballObject.Init();
                Debug.Log(ballObject);
            }
        }
    }
}