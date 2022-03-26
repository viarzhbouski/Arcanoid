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
            BonusWorkingDelay = 3f;
            BonusColor = bonusColor;
        }

        public void ApplyBonusBoost()
        {
            _ballController = AppControllers.Instance.GetController<BallController>();
            _ballController.SetBallExtraSpeed(-7f);
        }
        
        public void CancelBonusBoost()
        {
            _ballController.SetBallExtraSpeed(0);
        }
    }
}