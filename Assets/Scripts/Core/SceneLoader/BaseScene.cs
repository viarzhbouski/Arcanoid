using Common.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Core.SceneLoader
{
    public abstract class BaseScene : MonoBehaviour
    {
        [SerializeField]
        protected CanvasScaler sceneCanvasScaler;
        
        public abstract void LoadScene(GameScenes gameScene);
    }
}