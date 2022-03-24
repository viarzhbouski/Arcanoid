﻿using Boosts.Interfaces;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks
{
    public class BoostBlockView : BaseBlockView
    {
        private IHasBoost _boost;

        public override void SetBoost(IHasBoost boost)
        {
            _boost = boost;
        }

        public override void BlockHit()
        {
            base.PlayBlockHitAnim();
            _boost.ExecuteBoost();
        }
    }
}