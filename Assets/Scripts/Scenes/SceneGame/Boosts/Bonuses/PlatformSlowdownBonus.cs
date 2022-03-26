using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Controllers;
using UnityEngine;

namespace Scenes.SceneGame.Boosts.Bonuses
{
    public class PlatformSlowdownBonus : IHasBonusBoost
    {
        private PlatformController _platformController;
        
        public float BonusWorkingDelay { get; }
        
        public Color BonusColor { get; }

        public PlatformSlowdownBonus(Color bonusColor)
        {
            BonusWorkingDelay = 3f;
            BonusColor = bonusColor;
        }

        public void ApplyBonusBoost()
        {
            _platformController = AppControllers.Instance.GetController<PlatformController>();
            _platformController.SetPlatformExtraSpeed(-10f);
        }
        
        public void CancelBonusBoost()
        {
            _platformController.SetPlatformExtraSpeed(0);
        }
    }
}