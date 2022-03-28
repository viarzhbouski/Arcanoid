using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public abstract class BaseStartup : MonoBehaviour
    {
        public abstract void InitializeStartup(MonoConfiguration monoConfiguration);
    }
}