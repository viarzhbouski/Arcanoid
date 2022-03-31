using Common.Enums;
using Core.SceneLoader;

namespace Core.Statics
{
    public class AppSceneLoader
    {
        public static AppSceneLoader Instance;
        private readonly BaseScene _currentScene;

        public delegate void Load();
        public event Load SceneOnLoad;
        
        public AppSceneLoader(BaseScene currentScene)
        {
            Instance = this;
            _currentScene = currentScene;
        }

        public void LoadScene(GameScenes gameScene)
        {
            SceneOnLoad?.Invoke();
            _currentScene.LoadScene(gameScene);
        }
    }
}