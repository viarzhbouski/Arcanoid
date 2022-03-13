using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scenes.SceneMenu
{
    public class Startup : BaseStartup
    {
        private MainConfig _mainConfig;
        
        public override void InitializeStartup(MonoConfiguration monoConfiguration, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            throw new System.NotImplementedException();
        }
    }
}