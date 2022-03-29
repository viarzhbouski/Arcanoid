using Common.Enums;
using Core.SceneLoader;

namespace Core.Statics
{
    public class AppSceneLoader
    {
        public static AppSceneLoader Instance;
        private readonly BaseScene _currentScene;
        
        public AppSceneLoader(BaseScene currentScene)
        {
            Instance = this;
            _currentScene = currentScene;
        }

        public void LoadScene(GameScenes gameScene) => _currentScene.LoadScene(gameScene);
    }
}