using System.Collections.Generic;
using Scripts.Core.ObjectPooling;
using Scripts.Scenes.SceneGame.Controllers;
using UnityEngine;

namespace Scripts.Core
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField]
        private Startup startUp;
        [SerializeField]
        private List<PoolManager> poolManagers;
        
        private MonoConfiguration _monoConfiguration;
        
        private void Awake()
        {
            _monoConfiguration = new MonoConfiguration();
            _monoConfiguration.InitPools(poolManagers);
            startUp.Init(_monoConfiguration);
        }

        private void Start() => _monoConfiguration.Start();
        
        private void Update() => _monoConfiguration.Update();
        
        private void FixedUpdate() => _monoConfiguration.FixedUpdate();
    }
}