using UnityEngine;

namespace Scripts.Core.ObjectPooling
{
    public interface IPoolable
    {
        public GameObject GetGameObject();
    }
}