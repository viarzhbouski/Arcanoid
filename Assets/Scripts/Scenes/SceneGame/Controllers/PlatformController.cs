using UnityEngine;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Scenes.SceneGame.Controllers.Views;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class PlatformController : IController, IHasUpdate
    {
        private readonly PlatformModel _platformModel;
        private readonly PlatformView _platformView;

        public PlatformController(IView view)
        {
            _platformModel = new PlatformModel();
            _platformView = view as PlatformView;
            
            _platformView!.Bind(_platformModel);
            _platformModel.OnChangeHandler(ControllerOnChange);
        }
        
        public void UpdateController()
        {
            Move();
        }
        
        private void Move()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                var position = _platformModel.Position;
                position.x -= 0.01f;
                _platformModel.Position = position;
                _platformModel.OnChange?.Invoke();
            }
            
            if (Input.GetKey(KeyCode.RightArrow))
            {
                var position = _platformModel.Position;
                position.x += 0.01f;
                _platformModel.Position = position;
                _platformModel.OnChange?.Invoke();
            }
        }

        public void ControllerOnChange()
        {
            _platformView.RenderChanges();
        }
    }
}
