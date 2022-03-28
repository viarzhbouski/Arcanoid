using Core;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.Views;
using ScriptableObjects;
using UnityEngine;

namespace Scenes.SceneGame
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

        public override void InitializeStartup(MonoConfiguration monoConfiguration)
        {
            monoConfiguration.AddController(new GenerateLevelController(generateLevelView));
            monoConfiguration.AddController(new LevelProgressController(levelProgressView));
            monoConfiguration.AddController(new BordersController(bordersView));
            monoConfiguration.AddController(new LifesController(lifesView));
            monoConfiguration.AddController(new BallController(ballView));
            monoConfiguration.AddController(new PlatformController(platformView));
            monoConfiguration.AddController(new PauseGameController(pauseGameView));
        }
    }
}