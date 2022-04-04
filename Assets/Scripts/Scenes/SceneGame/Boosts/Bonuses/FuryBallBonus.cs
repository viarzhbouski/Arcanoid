using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Controllers;
using UnityEngine;

namespace Scenes.SceneGame.Boosts.Bonuses
{
    public class FuryBallBonus : IHasBonusBoost
    {
        private BallController _ballController;
        
        public float BonusWorkingDelay { get; }
        
        public Color BonusColor { get; }

        public FuryBallBonus(Color bonusColor)
        {
            BonusWorkingDelay = AppConfig.Instance.BoostsConfig.FuryBallWorkingDelay;
            BonusColor = bonusColor;
        }

        public void ApplyBonusBoost()
        {
            _ballController = AppControllers.Instance.GetController<BallController>();
            _ballController.SetBallCanDestroyAllBlocks(true);
        }
        
        public void CancelBonusBoost()
        {
            _ballController.SetBallCanDestroyAllBlocks(false);
        }
    }
}