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

        [SerializeField]
        private LevelProgressView levelProgressView;

        public override void InitializeStartup(MonoConfiguration monoConfiguration, MainConfig mainConfig)
        {
            var generateLevelController = new GenerateLevelController(generateLevelView, mainConfig);
            var levelProgressController = new LevelProgressController(levelProgressView, generateLevelController, mainConfig);
            var bordersController = new BordersController(bordersView, mainConfig);
            var lifesController = new LifesController(lifesView, mainConfig);
            var ballController = new BallController(ballView, lifesController, levelProgressController, mainConfig);
            var platformController = new PlatformController(platformView, ballController, mainConfig);
            
            monoConfiguration.AddController(generateLevelController);
            monoConfiguration.AddController(levelProgressController);
            monoConfiguration.AddController(bordersController);
            monoConfiguration.AddController(platformController);
            monoConfiguration.AddController(lifesController);
            monoConfiguration.AddController(ballController);
        }
    }
}