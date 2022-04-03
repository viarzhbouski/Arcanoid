using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views;
using UnityEngine;

namespace Scenes.SceneGame.Controllers
{
    public class PlatformController : IController, IHasUpdate
    {
        private readonly PlatformModel _platformModel;
        private readonly PlatformView _platformView;

        public PlatformController(IView view)
        {
            _platformModel = new PlatformModel();
            _platformView = view as PlatformView;
            
            _platformView!.Bind(_platformModel, this);
            _platformModel.OnChangeHandler(ControllerOnChange);
            _platformModel.Speed = AppConfig.Instance.BallAndPlatform.PlatformSpeed;
            _platformModel.Size = 1f;
        }
        
        public void UpdateController()
        {
            Move();
        }
        
        public Vector2 GetPlatformBallStartPosition()
        {
            return _platformModel.PlatformBallStartPosition;
        }

        public bool IsStarted(bool? isStarted = null)
        {
            if (isStarted.HasValue)
            {
                _platformModel.IsStarted = isStarted.Value;
            }
            
            return _platformModel.IsStarted;
        }
        
        public void ResizePlatform(float extraSize)
        {
            _platformModel.ExtraSize = extraSize;
            _platformModel.SizeNeedChange = true;
        }

        public void ControllerOnChange()
        {
            _platformView.RenderChanges();
        }
        
        public void SetPlatformExtraSpeed(float speed)
        {
            _platformModel.ExtraSpeed = speed;
        }
        
        private void SetInputPosition(Vector2 inputPosition)
        {
            _platformModel.IsHold = true;
            _platformModel.TapPosition = inputPosition;
        }

        public void PastePlatformOnStartPosition()
        {
            _platformModel.Position = _platformModel.StartPosition;
            _platformModel.PlatformOnStart = false;
            _platformModel.SizeNeedChange = true;
            _platformModel.ExtraSize = 0;
            _platformModel.ExtraSpeed = 0;
        }

        private void Move()
        {
            if (Input.touchCount > 0 && AppPopups.Instance.ActivePopups == 0)
            {
                SetInputPosition(Input.GetTouch(0).position);
            }
            else if (Input.GetMouseButton(0) && AppPopups.Instance.ActivePopups == 0)
            {
                SetInputPosition(Input.mousePosition);
            }
            else if (_platformModel.IsHold)
            {
                _platformModel.IsHold = false;
                
                if (!_platformModel.IsStarted && AppPopups.Instance.ActivePopups == 0)
                {
                    _platformModel.IsStarted = true;
                }
            }

            _platformModel.OnChange?.Invoke();
        }
    }
}
