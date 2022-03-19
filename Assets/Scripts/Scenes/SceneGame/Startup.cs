using Managers;
using Scripts.Core;
using Scripts.Scenes.SceneGame.Controllers.Views;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class Startup : BaseStartup
    {
        [SerializeField]
        private BordersView bordersView;
        
        [SerializeField]
        private PlatformView platformView;
        
        [SerializeField]
        private BallView ballView;
        
        [SerializeField]
        private GenerateLevelView generateLevelView;

        [SerializeField]
        private LifesView lifesView;

        public override void InitializeStartup(MonoConfiguration monoConfiguration, MainConfig mainConfig)
        {
            var bordersController = new BordersController(bordersView);
            var lifesController = new LifesController(lifesView, mainConfig);
            var ballController = new BallController(ballView, lifesController, mainConfig);
            var platformController = new PlatformController(platformView, ballController, mainConfig);
            var generateLevelController = new GenerateLevelController(generateLevelView, mainConfig);

            monoConfiguration.AddController(bordersController);
            monoConfiguration.AddController(platformController);
            monoConfiguration.AddController(lifesController);
            monoConfiguration.AddController(ballController);
            monoConfiguration.AddController(generateLevelController);
        }
    }
}