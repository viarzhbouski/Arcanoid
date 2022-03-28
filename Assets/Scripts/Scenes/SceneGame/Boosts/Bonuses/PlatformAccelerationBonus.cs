using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Controllers;
using UnityEngine;

namespace Scenes.SceneGame.Boosts.Bonuses
{
    public class PlatformAccelerationBonus : IHasBonusBoost
    {
        private PlatformController _platformController;
        
        public float BonusWorkingDelay { get; }
        
        public Color BonusColor { get; }

        public PlatformAccelerationBonus(Color bonusColor)
        {
            BonusWorkingDelay = AppConfig.Instance.BoostsConfig.PlatformAccelerationWorkingDelay;
            BonusColor = bonusColor;
        }

        public void ApplyBonusBoost()
        {
            _platformController = AppControllers.Instance.GetController<PlatformController>();
            _platformController.SetPlatformExtraSpeed(AppConfig.Instance.BoostsConfig.PlatformAccelerationSpeed);
        }
        
        public void CancelBonusBoost()
        {
            _platformController.SetPlatformExtraSpeed(0);
        }
    }
}