﻿using Scenes.ScenePack.Controllers;
using Scenes.ScenePack.Views;
using Scripts.Core;
using Scripts.Core.Interfaces;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scenes.ScenePack
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