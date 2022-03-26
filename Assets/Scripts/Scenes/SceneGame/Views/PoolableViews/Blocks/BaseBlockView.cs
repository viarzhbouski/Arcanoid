using System;
using System.Linq;
using Common.Enums;
using Core.ObjectPooling;
using Core.ObjectPooling.Interfaces;
using DG.Tweening;
using Scenes.SceneGame.Boosts.Interfaces;
using Scenes.SceneGame.ScenePools;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks
{
    public abstract class BaseBlockView : MonoBehaviour, IPoolable
    {
        [SerializeField]
        private protected SpriteRenderer blockSpriteRenderer;

        private Action _destroyBlockEvent;
        
        protected Block Block;
        
        public SpriteRenderer BlockSpriteRenderer => blockSpriteRenderer;
        
        public BlockTypes BlockType => Block.BlockType;
        
        public BoostTypes? BoostType => Block.BoostType;
        
        public Color BlockColor => blockSpriteRenderer.color;
        
        public bool CanDestroy => Block.BlockType != BlockTypes.Granite && Block.HealthPoints <= 0;
        
        public virtual void SetBlockConfig(Block block, Action destroyBlockEvent)
        {
            Block = block;
            
            if (block.BlockType != BlockTypes.Boost)
            {
                blockSpriteRenderer.color = block.Colors.Length == 1
                    ? block.Colors.First()
                    : block.Colors[Random.Range(0, block.Colors.Length - 1)];
            }
            else
            {
                blockSpriteRenderer.color =
                    block.Colors[(int) block.BoostType!.Value - Enum.GetValues(typeof(BlockTypes)).Length];
            }
            
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