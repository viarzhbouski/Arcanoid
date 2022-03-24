using Boosts.Interfaces;
using Common.Enums;
using DG.Tweening;
using Scenes.SceneGame.ScenePools;
using Scripts.Core.ObjectPooling;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks
{
    public abstract class BaseBlockView : MonoBehaviour, IPoolable
    {
        [SerializeField]
        private protected SpriteRenderer blockSpriteRenderer;
        protected Block Block;
        
        public SpriteRenderer BlockSpriteRenderer => blockSpriteRenderer;
        
        public BlockTypes BlockType => Block.BlockType;
        
        public BoostTypes? BoostType => Block.BoostType;
        
        public bool CanDestroy => Block.BlockType != BlockTypes.Granite && Block.HealthPoints <= 0;

        public virtual void SetBlockConfig(Block block)
        {
            Block = block;
            blockSpriteRenderer.color = block.Color;
        }

        public abstract void SetBoost(IHasBoost boost);
        
        public virtual void BlockHit()
        {
            PlayBlockHitAnim();
        }

        protected virtual void PlayBlockHitAnim()
        {
            transform.DOShakePosition(0.05f, 0.5f).SetEase(Ease.OutBounce);

            if (CanDestroy)
            {
                var objectPool = ObjectPools.Instance.GetObjectPool<BlockDestroyEffectPool>();
                var blockDestroyEffect = objectPool.GetObject();
                blockDestroyEffect.transform.position = transform.position;
                objectPool.DestroyPoolObject(blockDestroyEffect);
            }
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}