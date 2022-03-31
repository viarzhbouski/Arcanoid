using Core;
using Scenes.Common.Controllers;
using Scenes.Common.Views;
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
        [SerializeField]
        private EnergyView energyView;
        
        public override void InitializeStartup(MonoConfiguration monoConfiguration)
        {
            monoConfiguration.AddController(new MenuController(menuView));
            monoConfiguration.AddController(new EnergyController(energyView));
        }
    }
}