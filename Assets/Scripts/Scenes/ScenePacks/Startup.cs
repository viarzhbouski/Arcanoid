using Core;
using Scenes.ScenePacks.Controllers;
using Scenes.ScenePacks.Views;
using ScriptableObjects;
using UnityEngine;

namespace Scenes.ScenePacks
{
    public class Startup : BaseStartup
    {
        [SerializeField]
        private PackListView packListView;

        private MainConfig _mainConfig;
        
        public override void InitializeStartup(MonoConfiguration monoConfiguration, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            monoConfiguration.AddController(new PackListController(packListView, mainConfig));
        }
    }
}