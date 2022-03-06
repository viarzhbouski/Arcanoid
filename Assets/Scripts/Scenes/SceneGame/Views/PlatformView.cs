using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers.Views
{
    public class PlatformView : MonoBehaviour, IView
    {
        private PlatformModel _platformModel;

        public void Bind(IModel model)
        {
            _platformModel = model as PlatformModel;
            _platformModel!.Position = transform.position;
        }
        
        public void RenderChanges()
        {
            transform.position = _platformModel.Position;
        }
    }
}