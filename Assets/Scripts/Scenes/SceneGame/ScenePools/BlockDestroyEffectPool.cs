using System.Collections;
using Scenes.SceneGame.Views.PoolableViews.Effects;
using Scripts.Core.ObjectPooling;
using UnityEngine;

namespace Scenes.SceneGame.ScenePools
{
    public class BlockDestroyEffectPool : Pool<BlockDestroyEffectView>
    {
        [SerializeField]
        private float destroyDelay = 0.5f;

        public override void DestroyPoolObject(BlockDestroyEffectView obj) => StartCoroutine(DestroyByDelay(obj));

        IEnumerator DestroyByDelay(BlockDestroyEffectView obj)
        {
            yield return new WaitForSeconds(destroyDelay);
            ObjectPool.DestroyPoolObject(obj);
        }
    }
}