using System.Collections.Generic;
using Boosts.Interfaces;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using UnityEngine;

namespace Boosts
{
    public class ColorChainBombBoost : IHasBoost
    {
        private readonly List<BaseBlockView> _neighbourBlocks;
        
        public ColorChainBombBoost(List<BaseBlockView> neighbourBlocks)
        {
            _neighbourBlocks = neighbourBlocks;
        }
        
        public void ExecuteBoost()
        {
            Debug.Log("ColorChainBoom");
        }
    }
}