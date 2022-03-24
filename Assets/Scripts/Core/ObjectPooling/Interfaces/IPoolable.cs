using UnityEngine;

namespace Core.ObjectPooling.Interfaces
{
    public interface IPoolable
    {
        public GameObject GetGameObject();
    }
}