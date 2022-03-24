using System.Collections.Generic;
using Core.ObjectPooling.Pools;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        private MonoConfiguration _monoConfiguration;
        
        private void Awake()
        {
            _monoConfiguration = new MonoConfiguration();
            _monoConfiguration.InitPools(poolProviders);
            startUp.InitializeStartup(_monoConfiguration, mainConfig);
            SceneManager.GetActiveScene();
        }

        private void Start() => _monoConfiguration.Start();
        
        private void Update() => _monoConfiguration.Update();
        
        private void FixedUpdate() => _monoConfiguration.FixedUpdate();
    }
}