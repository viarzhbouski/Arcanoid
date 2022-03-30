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

        protected Action DestroyBlockEvent;
        protected BlockInfo Block;
        private bool _counted;
        
        public SpriteRenderer BlockSpriteRenderer => blockSpriteRenderer;
        
        public BlockTypes BlockType => Block.BlockType;
        
        public BoostTypes? BoostType => Block.BoostType;
        
        public Color BlockColor => blockSpriteRenderer.color;
        
        public bool CanDestroy => Block.BlockType != BlockTypes.Granite && Block.HealthPoints <= 0;
        
        public virtual void SetBlockConfig(BlockInfo block, Action destroyBlockEvent)
        {
            Block = block;
            blockSpriteRenderer.color = block.Color;
            DestroyBlockEvent = destroyBlockEvent;
        }

        public abstract void SetBoost(IHasBoost boost);
        
        public abstract void DestroyBlock();
        
        public virtual bool BlockHit(int damage = 1, bool countBlock = true, bool destroyImmediately = false)
        {
            if (destroyImmediately)
            {
                BlockHitHandle(countBlock);
                return true;
            }
            
            BlockHitAnim();
            
            if (CanDestroy)
            {
                BlockHitHandle(countBlock);
            }
            
            return CanDestroy;
        }
        
        public virtual void BlockHitAnim()
        {
            transform.DOKill();
            transform.DOShakePosition(0.05f, 0.5f).SetEase(Ease.OutBounce);
        }

        private void BlockHitHandle(bool countBlock)
        {
            if (countBlock && !_counted)
            {
                DestroyBlockEvent.Invoke();
                _counted = true;
            }

            var objectPool = ObjectPools.Instance.GetObjectPool<BlockDestroyEffectPool>();
            var blockDestroyEffect = objectPool.GetObject();
            blockDestroyEffect.transform.position = transform.position;
            objectPool.DestroyPoolObject(blockDestroyEffect);
            DestroyBlock();
        }

        private void OnEnable()
        {
            _counted = false;
        }
        
        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}