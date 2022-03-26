using UnityEngine;

namespace Scenes.SceneGame.Boosts.Interfaces
{
    public interface IHasBonusBoost
    {
        public float BonusWorkingDelay { get; }
        
        public Color BonusColor { get; }
        
        public void ApplyBonusBoost();
        
        public void CancelBonusBoost();
    }
}