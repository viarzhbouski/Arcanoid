using Scripts.Core.ObjectPooling;
using UnityEngine;

namespace MonoModels
{
    public class BallCollisionEffectView : MonoBehaviour, IPoolable
    {
        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}