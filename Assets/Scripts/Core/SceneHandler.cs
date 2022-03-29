﻿using System.Collections.Generic;
using Core.ObjectPooling.Pools;
using Core.SceneLoader;
using Core.Statics;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField]
        private MainConfig mainConfig;
        [SerializeField]
        private BaseStartup startUp;
        [SerializeField]
        private List<PoolProvider> poolProviders;
        [SerializeField]
        private BaseScene currentScene;
        
        private MonoConfiguration _monoConfiguration;
        private AppConfig _appConfig;
        private AppSceneLoader _appSceneLoader;
        
        private void Awake()
        {
            _monoConfiguration = new MonoConfiguration();
            _monoConfiguration.InitPools(poolProviders);
            _appConfig = new AppConfig(mainConfig);
            _appSceneLoader = new AppSceneLoader(currentScene);
            startUp.InitializeStartup(_monoConfiguration);
        }

        private void Start() => _monoConfiguration.Start();
        
        private void Update() => _monoConfiguration.Update();
        
        private void FixedUpdate() => _monoConfiguration.FixedUpdate();
    }
}