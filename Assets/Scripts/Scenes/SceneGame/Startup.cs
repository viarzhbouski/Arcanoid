using Scripts.Core;
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
        
        public void Init(MonoHandler monoHandler)
        {
            monoHandler.AddController(new PlatformController(platformView));
            monoHandler.AddController(new BallController(ballView));
        }
    }
}