using System.Collections;
using Core.ObjectPooling.Pools;
using Scenes.SceneGame.Views.PoolableViews.Effects;
using UnityEngine;

namespace Scenes.SceneGame.ScenePools
{
    public class BallCollisionEffectPool : Pool<BallCollisionEffectView>
    {
        [SerializeField]
        private float destroyDelay = 0.5f;

        public override void DestroyPoolObject(BallCollisionEffectView obj) => StartCoroutine(DestroyByDelay(obj));

        IEnumerator DestroyByDelay(BallCollisionEffectView obj)
        {
            yield return new WaitForSeconds(destroyDelay);
            ObjectPool.DestroyPoolObject(obj);
        }
    }
}