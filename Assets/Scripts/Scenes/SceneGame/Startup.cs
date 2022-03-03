using Scripts.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class Startup : MonoBehaviour
    {
        private readonly List<BaseController> _controllers = new List<BaseController>();
        private MonoHandler _monoHandler;

        private void Start()
        {
            Init();
            _monoHandler.Start(_controllers);
        }

        public void Init()
        {
            _monoHandler = new MonoHandler();
            var setting = new ControllerSettings();

            AddController(new PlatformController());
        }

        private void AddController(BaseController controller)
        {
            _controllers.Add(controller);
        }

        private void Update() => _monoHandler.Update(_controllers);

        private void FixedUpdate() => _monoHandler.FixedUpdate(_controllers);
    }
}