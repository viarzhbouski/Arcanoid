using System.Collections;
using Core.ObjectPooling;
using Scenes.SceneGame.Boosts.Interfaces;
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
        private const float ExecuteDelay = 0.05f;
        
        public override void SetBoost(IHasBoost boost)
        {
            _boost = boost;
        }

        public override void DestroyBlock()
        {
            ObjectPools.Instance.GetObjectPool<BoostBlockPool>()
                .DestroyPoolObject(this);
        }

        public override void BlockHit(int damage = 1)
        {
            StartCoroutine(Execute());
        }

        IEnumerator Execute()
        {
            yield return new WaitForSeconds(ExecuteDelay);
            base.BlockHit();
            _boost.ExecuteBoost(bonusBoost);
        }
    }
}