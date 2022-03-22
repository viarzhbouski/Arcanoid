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
        
        [SerializeField]
        private PauseGameView pauseGameView;

        public override void InitializeStartup(MonoConfiguration monoConfiguration, MainConfig mainConfig)
        {
            monoConfiguration.AddController(new GenerateLevelController(generateLevelView, mainConfig));
            monoConfiguration.AddController(new LevelProgressController(levelProgressView, mainConfig));
            monoConfiguration.AddController(new BordersController(bordersView, mainConfig));
            monoConfiguration.AddController(new LifesController(lifesView, mainConfig));
            monoConfiguration.AddController(new BallController(ballView, mainConfig));
            monoConfiguration.AddController(new PlatformController(platformView, mainConfig));
            monoConfiguration.AddController(new PauseGameController(pauseGameView, mainConfig));
        }
    }
}