using Core;
using Scenes.SceneMenu.Controllers;
using Scenes.SceneMenu.Views;
using ScriptableObjects;
using UnityEngine;

namespace Scenes.SceneMenu
{
    public class Startup : BaseStartup
    {
        [SerializeField]
        private MenuView menuView;
        
        private MainConfig _mainConfig;
        
        public override void InitializeStartup(MonoConfiguration monoConfiguration, MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            monoConfiguration.AddController(new MenuController(menuView, mainConfig));
        }
    }
}