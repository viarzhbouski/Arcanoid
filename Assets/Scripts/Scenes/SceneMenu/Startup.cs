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
        
        public override void InitializeStartup(MonoConfiguration monoConfiguration)
        {
            monoConfiguration.AddController(new MenuController(menuView));
        }
    }
}