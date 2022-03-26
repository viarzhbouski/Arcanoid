using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Controllers;
using UnityEngine;

namespace Scenes.SceneGame.Boosts.Bonuses
{
    public class PlatformSizeEncreaseBonus : IHasBonusBoost
    {
        private PlatformController _platformController;
        
        public float BonusWorkingDelay { get; }
        
        public Color BonusColor { get; }

        public PlatformSizeEncreaseBonus(Color bonusColor)
        {
            BonusWorkingDelay = 10f;
            BonusColor = bonusColor;
        }

        public void ApplyBonusBoost()
        { 
            _platformController = AppControllers.Instance.GetController<PlatformController>();
            _platformController.ResizePlatform(0.75f);
        }
        
        public void CancelBonusBoost()
        {
            _platformController.ResizePlatform(0);
        }
    }
}