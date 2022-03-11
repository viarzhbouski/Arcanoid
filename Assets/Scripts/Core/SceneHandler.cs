using Scripts.Scenes.SceneGame.Controllers;
using UnityEngine;

namespace Scripts.Core
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField]
        private Startup startUp;
        private MonoConfiguration _monoConfiguration;
        
        private void Awake()
        {
            _monoConfiguration = new MonoConfiguration();
            startUp.Init(_monoConfiguration);
        }

        private void Start() => _monoConfiguration.Start();
        
        private void Update() => _monoConfiguration.Update();
        
        private void FixedUpdate() => _monoConfiguration.FixedUpdate();
    }
}