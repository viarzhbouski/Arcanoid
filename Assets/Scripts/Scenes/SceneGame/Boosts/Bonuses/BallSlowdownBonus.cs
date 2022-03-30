using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Controllers;
using UnityEngine;

namespace Scenes.SceneGame.Boosts.Bonuses
{
    public class BallSlowdownBonus : IHasBonusBoost
    {
        private BallController _ballController;
        
        public float BonusWorkingDelay { get; }
        
        public Color BonusColor { get; }

        public BallSlowdownBonus(Color bonusColor)
        {
            BonusWorkingDelay = AppConfig.Instance.BoostsConfig.BallSlowdownWorkingDelay;
            BonusColor = bonusColor;
        }

        public void ApplyBonusBoost()
        {
            _ballController = AppControllers.Instance.GetController<BallController>();
            _ballController.SetBallExtraSpeed(AppConfig.Instance.BoostsConfig.BallSlowdownSpeed);
        }
        
        public void CancelBonusBoost()
        {
            _ballController.SetBallExtraSpeed(0);
        }
    }
}