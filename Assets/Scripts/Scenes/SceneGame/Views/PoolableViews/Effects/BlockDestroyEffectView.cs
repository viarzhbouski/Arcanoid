using Scripts.Core.ObjectPooling;
using UnityEngine;

namespace Scenes.SceneGame.Views.PoolableViews.Effects
{
    public class BlockDestroyEffectView : MonoBehaviour, IPoolable
    {
        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}