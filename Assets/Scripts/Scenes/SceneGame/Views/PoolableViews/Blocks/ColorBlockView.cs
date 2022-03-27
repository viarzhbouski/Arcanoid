using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.ObjectPooling;
using Core.Statics;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.ScenePools;
using UnityEngine;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks
{
    public class ColorBlockView : BaseBlockView
    {
        [SerializeField]
        private List<Sprite> damageSprites;

        private IHasBoost _boost;
        private Queue<Sprite> _spriteQueue;
        private int _damageForChangeSprite;
        private int _damageSum;
        
        public override void SetBlockConfig(BlockInfo block, Action destroyBlockEvent)
        {
            _boost = null;
            base.SetBlockConfig(block, destroyBlockEvent);

            if (Block.HealthPoints > damageSprites.Count)
            {
                _damageForChangeSprite = Block.HealthPoints / damageSprites.Count;
            }
            else
            {
                _damageForChangeSprite = 1;
            }

            _spriteQueue = new Queue<Sprite>();
            _damageSum = 0;
            
            foreach (var damageSprite in damageSprites)
            {
                _spriteQueue.Enqueue(damageSprite);
            }

            blockSpriteRenderer.sprite = _spriteQueue.Dequeue();
        }        

        public override void SetBoost(IHasBoost boost)
        {
            _boost = boost;
        }

        public override void BlockHit(int damage = 1, bool countBlock = true, bool destroyImmediately = false)
        {
            if (_boost == null)
            {
                SetBlockDamage(damage);
            }

            else
            {
                StartCoroutine(Execute(damage));
            }
        }

        private void SetBlockDamage(int damage)
        {
            Block.HealthPoints -= damage;
            _damageSum += damage;

            base.BlockHit(damage);
            ChangeSprite();
        }
        
        IEnumerator Execute(int damage)
        {
            yield return new WaitForSeconds(AppConfig.Instance.BoostsConfig.ChainBombExecuteDelay);
            SetBlockDamage(damage);
            _boost.ExecuteBoost();
        }

        private void ChangeSprite()
        {
            if (_damageSum % _damageForChangeSprite == 0 && _spriteQueue.Any())
            {
                blockSpriteRenderer.sprite = _spriteQueue.Dequeue();
            }
        }
        
        public override void DestroyBlock()
        {
            ObjectPools.Instance.GetObjectPool<ColorBlockPool>()
                .DestroyPoolObject(this);
        }
    }
}