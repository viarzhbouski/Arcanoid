using System;
using System.Collections;
using Core.ObjectPooling;
using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views.PoolableViews.Blocks.BonusBoost;
using UnityEngine;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks
{
    public class BoostBlockView : BaseBlockView
    {
        [SerializeField]
        private BonusBoostView bonusBoost;
        
        private IHasBoost _boost;

        public override void SetBlockConfig(BlockInfo block, Action destroyBlockEvent)
        {
            Block = block;
            DestroyBlockEvent = destroyBlockEvent;
            BlockSpriteRenderer.sprite = block.Sprite;
        }

        public override void SetBoost(IHasBoost boost)
        {
            _boost = boost;
        }

        public override void DestroyBlock()
        {
            ObjectPools.Instance.GetObjectPool<BoostBlockPool>()
                .DestroyPoolObject(this);
        }

        public override bool BlockHit(int damage = 1, bool countBlock = true, bool destroyImmediately = false)
        {
            if (_boost is Boosts.BonusBoost)
            {
                Execute(damage, countBlock, destroyImmediately);
            }
            else
            {
                StartCoroutine(ExecuteWithDelay(damage, countBlock, destroyImmediately));
            }
            
            return CanDestroy;
        }

        private void Execute(int damage, bool countBlock, bool destroyImmediately)
        {
            base.BlockHit(damage, countBlock, destroyImmediately);
            _boost.ExecuteBoost(bonusBoost);
        }
        
        IEnumerator ExecuteWithDelay(int damage, bool countBlock, bool destroyImmediately)
        {
            yield return new WaitForSeconds(AppConfig.Instance.BoostsConfig.BombExecuteDelay);
            Execute(damage, countBlock, destroyImmediately);
        }
    }
}