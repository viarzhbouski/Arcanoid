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

        public override void InitializeStartup(MonoConfiguration monoConfiguration)
        {
            monoConfiguration.AddController(new PackListController(packListView));
        }
    }
}