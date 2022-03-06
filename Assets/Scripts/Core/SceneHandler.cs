using Scripts.Scenes.SceneGame.Controllers;
using UnityEngine;

namespace Scripts.Core
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField]
        private Startup startUp;
        private MonoHandler _monoHandler;
        
        private void Awake()
        {
            _monoHandler = new MonoHandler();
            startUp.Init(_monoHandler);
        }

        private void Start() => _monoHandler.Start();
        
        private void Update() => _monoHandler.Update();
        
        private void FixedUpdate() => _monoHandler.FixedUpdate();
    }
}