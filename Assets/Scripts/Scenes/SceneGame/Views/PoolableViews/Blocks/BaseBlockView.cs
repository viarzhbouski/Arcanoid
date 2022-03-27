using System;
using Common.Enums;
using Core.ObjectPooling;
using Core.ObjectPooling.Interfaces;
using DG.Tweening;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.ScenePools;
using UnityEngine;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks
{
    public abstract class BaseBlockView : MonoBehaviour, IPoolable
    {
        [SerializeField]
        private protected SpriteRenderer blockSpriteRenderer;

        private Action _destroyBlockEvent;
        
        protected BlockInfo Block;
        
        public SpriteRenderer BlockSpriteRenderer => blockSpriteRenderer;
        
        public BlockTypes BlockType => Block.BlockType;
        
        public BoostTypes? BoostType => Block.BoostType;
        
        public Color BlockColor => blockSpriteRenderer.color;
        
        public bool CanDestroy => Block.BlockType != BlockTypes.Granite && Block.HealthPoints <= 0;
        
        public virtual void SetBlockConfig(BlockInfo block, Action destroyBlockEvent)
        {
            Block = block;
            blockSpriteRenderer.color = block.Color;
            _destroyBlockEvent = destroyBlockEvent;
        }

        public abstract void SetBoost(IHasBoost boost);
        
        public abstract void DestroyBlock();
        
        public virtual void BlockHit(int damage = 1, bool countBlock = true, bool destroyImmediately = false)
        {
            if (destroyImmediately)
            {
                BlockHitHandle(countBlock);
                return;
            }
            
            BlockHitAnim();

            if (CanDestroy)
            {
                BlockHitHandle(countBlock);
            }
        }
        
        public virtual void BlockHitAnim()
        {
            transform.DOKill();
            transform.DOShakePosition(0.05f, 0.5f).SetEase(Ease.OutBounce);
        }
        
        private void BlockHitHandle(bool countBlock)
        {
            if (countBlock)
            {
                _destroyBlockEvent.Invoke();
            }

            var objectPool = ObjectPools.Instance.GetObjectPool<BlockDestroyEffectPool>();
            var blockDestroyEffect = objectPool.GetObject();
            blockDestroyEffect.transform.position = transform.position;
            objectPool.DestroyPoolObject(blockDestroyEffect);
            DestroyBlock();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}