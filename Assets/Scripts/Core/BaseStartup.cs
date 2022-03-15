using System.Collections.Generic;
using Scripts.Core.Interfaces;
using Scripts.Core.ObjectPooling;
using Scripts.Scenes.SceneGame.Controllers;
using Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Core
{
    public abstract class BaseStartup : MonoBehaviour
    {
        public abstract void InitializeStartup(MonoConfiguration monoConfiguration, MainConfig mainConfig);
    }
}