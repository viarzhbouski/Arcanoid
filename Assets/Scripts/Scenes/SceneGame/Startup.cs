using System.Collections.Generic;
using MonoModels;
using Scripts.Core;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.ObjectPooling;
using Scripts.Scenes.SceneGame.Controllers.Views;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class Startup : MonoBehaviour
    {
        [SerializeField]
        private PlatformView platformView;
        
        [SerializeField]
        private BallView ballView;
        
        [SerializeField]
        private GenerateLevelView generateLevelView;

        public void Init(MonoConfiguration monoConfiguration)
        {
            monoConfiguration.AddController(new PlatformController(platformView));
            monoConfiguration.AddController(new BallController(ballView));
            monoConfiguration.AddController(new GenerateLevelController(generateLevelView));
        }
    }
}