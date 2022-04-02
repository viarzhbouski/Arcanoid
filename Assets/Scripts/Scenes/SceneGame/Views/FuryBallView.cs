using Common.Enums;
using Core.Statics;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using UnityEngine;

namespace Scenes.SceneGame.Views
{
    public class FuryBallView : MonoBehaviour
    {
        public bool CanDestroyBlocks { get; set; } 
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Block") && CanDestroyBlocks)
            {
                SpawnBallCollisionEffect();
                var blockView = other.gameObject.GetComponent<BaseBlockView>();
                if (blockView != null)
                {
                    SpawnBallCollisionEffect();
                    blockView.BlockHit(int.MaxValue, blockView.BlockType != BlockTypes.Granite, true);
                }
            }
        }
        
        private void SpawnBallCollisionEffect()
        {
            var ballCollisionEffectPoolManager = AppObjectPools.Instance.GetObjectPool<BallCollisionEffectPool>();;
            var ballCollisionEffectMono = ballCollisionEffectPoolManager.GetObject();
            ballCollisionEffectMono.transform.position = transform.position;
            ballCollisionEffectPoolManager.DestroyPoolObject(ballCollisionEffectMono);
        }
    }
}