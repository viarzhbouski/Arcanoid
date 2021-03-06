using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Controllers;
using UnityEngine;

namespace Scenes.SceneGame.Boosts.Bonuses
{
    public class BlackLabelBonus : IHasBonusBoost
    {
        private LifesController _lifesController;
        
        public float BonusWorkingDelay { get; }
        
        public Color BonusColor { get; }

        public BlackLabelBonus(Color bonusColor)
        {
            BonusWorkingDelay = 0f;
            BonusColor = bonusColor;
        }

        public void ApplyBonusBoost()
        {
            _lifesController = AppControllers.Instance.GetController<LifesController>();
            _lifesController.DecreaseLife();
        }
        
        public void CancelBonusBoost() {}
    }
}