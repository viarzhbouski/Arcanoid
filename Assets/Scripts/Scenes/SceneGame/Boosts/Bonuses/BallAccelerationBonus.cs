using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Controllers;
using UnityEngine;

namespace Scenes.SceneGame.Boosts.Bonuses
{
    public class BallAccelerationBonus : IHasBonusBoost
    {
        private BallController _ballController;
        
        public float BonusWorkingDelay { get; }
        
        public Color BonusColor { get; }

        public BallAccelerationBonus(Color bonusColor)
        {
            BonusWorkingDelay = AppConfig.Instance.BoostsConfig.BallAccelerationWorkingDelay;
            BonusColor = bonusColor;
        }

        public void ApplyBonusBoost()
        {
            _ballController = AppControllers.Instance.GetController<BallController>();
            _ballController.SetBallExtraSpeed(AppConfig.Instance.BoostsConfig.BallAccelerationSpeed);
        }
        
        public void CancelBonusBoost()
        {
            _ballController.SetBallExtraSpeed(0);
        }
    }
}