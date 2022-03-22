using Scenes.SceneGame.Views;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.ObjectPooling;
using Scripts.Scenes.SceneGame.Controllers.Models;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers.Views
{
    public class BallView : MonoBehaviour, IView
    {
        [SerializeField]
        private Rigidbody2D ballRigidbody;
        [SerializeField]
        private TrailRenderer ballTrail;
        
        private BallModel _ballModel;
        private BallController _ballController;

        private Vector2 _prevMovementVector;
        private Vector2 _movementVectorBeforePause;
        
        public void Bind(IModel model, IController controller)
        {
            _ballModel = model as BallModel;
            _ballController = controller as BallController;
        }
        
        public void RenderChanges()
        {
            SetBallState();

            if (!_ballModel.BallIsStopped)
            {
                _prevMovementVector = ballRigidbody.velocity;
                if (!_ballModel.IsStarted)
                {
                    UpdateBallPosition();
                }
                else
                {
                    PushBall();
                }
            }
        }

        private void SetBallState()
        {
            if (_ballModel.BallIsStopped && ballRigidbody.bodyType == RigidbodyType2D.Dynamic)
            {
                _movementVectorBeforePause = ballRigidbody.velocity;
                ballRigidbody.bodyType = RigidbodyType2D.Static;
            }
            if (!_ballModel.BallIsStopped && ballRigidbody.bodyType == RigidbodyType2D.Static)
            {
                ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
                ballRigidbody.velocity = _movementVectorBeforePause;
            }
        }

        private void UpdateBallPosition()
        {
            ballRigidbody.velocity = Vector2.zero;
            transform.position = _ballModel.BallPosition;
        }

        private void PushBall()
        {
            if (!ballTrail.enabled)
            {
                ballTrail.enabled = true;
            }
            
            if (ballRigidbody.velocity.magnitude == 0f)
            {
                ballRigidbody.velocity = Vector2.up * _ballModel.Speed;
            }
            else
            {
                ballRigidbody.velocity = _ballModel.Speed * ballRigidbody.velocity.normalized;
            }
        }

        private void SpawnBallCollisionEffect()
        {
            var ballCollisionEffectPoolManager = ObjectPools.Instance.GetObjectPool<BallCollisionEffectPoolManager>();;
            var ballCollisionEffectMono = ballCollisionEffectPoolManager.GetObject();
            ballCollisionEffectMono.transform.position = transform.position;
            ballCollisionEffectPoolManager.DestroyObject(ballCollisionEffectMono);
        }

        private void CorrectBallMovement()
        {
            var reversedPrevVector = _prevMovementVector * new Vector2(-1, -1);
            var ballVector = ballRigidbody.velocity;
            var currentAngle = Vector2.Angle(reversedPrevVector, ballVector);

            if (currentAngle < _ballModel.MinBounceAngle)
            {
                var angleVector = Quaternion.Euler(new Vector2(_ballModel.MinBounceAngle * 2, 0));
                var x = ballVector.x * Mathf.Cos(angleVector.x) - ballVector.y * Mathf.Sin(angleVector.x);
                var y = ballVector.y * Mathf.Cos(angleVector.x) + ballVector.x * Mathf.Sin(angleVector.x);
                var newBallVector = new Vector2(x, y);
                ballRigidbody.velocity = newBallVector;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var platformView = collision.collider.gameObject.GetComponent<PlatformView>();
            
            if (platformView)
            {
                return;
            }
            
            CorrectBallMovement();
            
            var blockView = collision.collider.gameObject.GetComponent<BlockView>();
            
            if (blockView != null)
            {
                SpawnBallCollisionEffect();
                blockView.Damage();
                
                if (!blockView.CanDestroy)
                {
                    return;
                }

                var blockObjectPool = ObjectPools.Instance.GetObjectPool<BlockPoolManager>();
                blockObjectPool.DestroyObject(blockView);
                _ballController.BallDestroyBlock();
            }
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            _ballController.BallOutOfGameField();
            ballTrail.enabled = false;
        }
    }
}