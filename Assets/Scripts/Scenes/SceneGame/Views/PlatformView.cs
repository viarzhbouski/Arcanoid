using Core.Interfaces.MVC;
using Core.Statics;
using DG.Tweening;
using Scenes.SceneGame.Models;
using UnityEngine;

namespace Scenes.SceneGame.Views
{
    public class PlatformView : MonoBehaviour, IView
    {
        [SerializeField]
        private Rigidbody2D platformRigidbody2D;
        [SerializeField]
        private Camera platformCamera;
        [SerializeField]
        private Transform platformBallStartPosition;
        [SerializeField]
        private SpriteRenderer platformIndicator;
        
        private PlatformModel _platformModel;

        public void Bind(IModel model, IController controller)
        {
            _platformModel = model as PlatformModel;
            _platformModel!.Position = transform.position;
            _platformModel.StartPosition = _platformModel.Position;
            SetPlatformBallStartPosition();
        }
        
        public void RenderChanges()
        {
            SetPlatformPosition();
            SetPlatformBallStartPosition();
            SetPlatformSize();
        }

        private void SetPlatformSize()
        {
            if (!_platformModel.SizeNeedChange)
            {
                return;
            }

            transform.DOKill();
            transform.DOScaleX(_platformModel.PlatformSize, 1f);

            _platformModel.SizeNeedChange = false;
        }

        private void SetPlatformBallStartPosition()
        {
            _platformModel.PlatformBallStartPosition = platformBallStartPosition.position;
        }

        private void SetPlatformPosition()
        {
            if (!_platformModel.PlatformOnStart)
            {
                _platformModel.TapPosition = null;
                platformRigidbody2D.velocity = Vector2.zero;
                transform.position = _platformModel.StartPosition;
                _platformModel.PlatformOnStart = true;
            }
            
            if (_platformModel.TapPosition.HasValue)
            {
                var tapPosition = platformCamera.ScreenToWorldPoint(_platformModel.TapPosition!.Value);
                var tapPositionX = new Vector3(tapPosition.x, Vector2.zero.y);
                var positionX = new Vector3(transform.position.x, Vector2.zero.y);
                var mouseDir = tapPositionX - positionX;
                platformRigidbody2D.velocity = mouseDir.normalized * _platformModel.PlatformSpeed * Mathf.Clamp(mouseDir.magnitude, 0, 1);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Ball") || other.collider.CompareTag("Boost"))
            {
                platformIndicator.DOKill();
                platformIndicator.DOColor(AppConfig.Instance.BallAndPlatform.PlatformCollisionColor, 0.2f).SetEase(Ease.InBounce).onComplete += () =>
                {
                    platformIndicator.DOColor(AppConfig.Instance.BallAndPlatform.PlatformNonCollisionColor, 0.2f)
                        .SetEase(Ease.OutBounce);
                };
            }
        }
    }
}