﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Boosts.Interfaces;
using Scenes.SceneGame.ScenePools;
using Scripts.Core.ObjectPooling;
using Scripts.ScriptableObjects;
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
        private const float ExecuteDelay = 0.05f;
        
        public override void SetBlockConfig(Block block, Action blockDestroyEvent)
        {
            base.SetBlockConfig(block, blockDestroyEvent);

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

        public override void BlockHit(int damage = 1)
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

            PlayBlockHitAnim();
            ChangeSprite();
        }
        
        IEnumerator Execute(int damage)
        {
            yield return new WaitForSeconds(ExecuteDelay);
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