using System;
using System.Collections;
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
        private bool _boostExecuted;

        public override void SetBlockConfig(BlockInfo block, Action destroyBlockEvent)
        {
            Block = block;
            DestroyBlockEvent = destroyBlockEvent;
            BlockSpriteRenderer.sprite = block.Sprite;
            _boostExecuted = false;
        }

        public override void SetBoost(IHasBoost boost)
        {
            _boost = boost;
        }

        public override void DestroyBlock()
        {
            AppObjectPools.Instance.GetObjectPool<BoostBlockPool>()
                .DestroyPoolObject(this);
        }

        public override bool BlockHit(int damage = 1, bool countBlock = true, bool destroyImmediately = false)
        {
            if (_boost is Boosts.BonusBoost)
            {
                StartCoroutine(Execute(damage, countBlock, destroyImmediately));
            }
            else
            {
                if (destroyImmediately)
                {
                    StartCoroutine(Execute(damage, countBlock, destroyImmediately));
                    return true;
                }

                if (gameObject.activeSelf)
                {
                    StartCoroutine(ExecuteWithDelay(damage, countBlock, destroyImmediately));
                }
            }
            
            return CanDestroy;
        }

        IEnumerator Execute(int damage, bool countBlock, bool destroyImmediately)
        {
            while (AppPopups.Instance.HasActivePopups)
            {
                yield return new WaitForSeconds(0);
            }
            
            if (!_boostExecuted)
            {
                base.BlockHit(damage, countBlock, destroyImmediately);
                _boost.ExecuteBoost(bonusBoost);
                _boostExecuted = true;
            }

            yield return null;
        }
        
        IEnumerator ExecuteWithDelay(int damage, bool countBlock, bool destroyImmediately)
        {
            yield return new WaitForSeconds(AppConfig.Instance.BoostsConfig.BombExecuteDelay);
            StartCoroutine(Execute(damage, countBlock, destroyImmediately));
        }
    }
}