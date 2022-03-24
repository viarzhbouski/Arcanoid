using System.Collections;
using Boosts.Interfaces;
using Scenes.SceneGame.ScenePools;
using Scripts.Core.ObjectPooling;
using UnityEngine;

namespace Scenes.SceneGame.Views.PoolableViews.Blocks
{
    public class BoostBlockView : BaseBlockView
    {
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
            base.PlayBlockHitAnim();
            _boost.ExecuteBoost();
        }
    }
}