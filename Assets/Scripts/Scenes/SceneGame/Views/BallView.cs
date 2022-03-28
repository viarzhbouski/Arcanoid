using Common.Enums;
using Core.Interfaces.MVC;
using Core.ObjectPooling;
using Core.Statics;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using UnityEngine;

namespace Scenes.SceneGame.Views
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
            if (AppPopups.Instance.ActivePopups > 0)
            {
                ballTrail.enabled = false;
                if (ballRigidbody.bodyType == RigidbodyType2D.Dynamic)
                {
                    _movementVectorBeforePause = ballRigidbody.velocity;
                    ballRigidbody.bodyType = RigidbodyType2D.Static;
                }
            }
            else
            {
                if (ballRigidbody.bodyType == RigidbodyType2D.Static)
                {
                    ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
                    ballRigidbody.velocity = _movementVectorBeforePause;
                }
                
                if (!_ballModel.IsStarted)
                {
                    ballRigidbody.velocity = Vector2.zero;
                    UpdateBallPosition();
                }
                else
                {
                    ballTrail.enabled = true;
                    PushBall();
                }
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
                ballRigidbody.velocity = Vector2.up * _ballModel.BallSpeed;
            }
            else
            {
                ballRigidbody.velocity = _ballModel.BallSpeed * ballRigidbody.velocity.normalized;
            }
        }

        private void SpawnBallCollisionEffect()
        {
            var ballCollisionEffectPoolManager = ObjectPools.Instance.GetObjectPool<BallCollisionEffectPool>();;
            var ballCollisionEffectMono = ballCollisionEffectPoolManager.GetObject();
            ballCollisionEffectMono.transform.position = transform.position;
            ballCollisionEffectPoolManager.DestroyPoolObject(ballCollisionEffectMono);
        }

        private void CorrectBallMovement()
        {
            var reversedPrevVector = _prevMovementVector * new Vector2(-1, -1);
            var ballVector = ballRigidbody.velocity;
            var currentAngle = Vector2.Angle(reversedPrevVector, ballVector);

            if (currentAngle < _ballModel.MinBounceAngle)
            {
                var angleVector = Quaternion.Euler(new Vector2(_ballModel.MinBounceAngle, 0));
                var x = ballVector.x * Mathf.Cos(angleVector.x) - ballVector.y * Mathf.Sin(angleVector.x);
                var y = ballVector.y * Mathf.Cos(angleVector.x) + ballVector.x * Mathf.Sin(angleVector.x);
                var newBallVector = new Vector2(x, y);
                ballRigidbody.velocity = newBallVector;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var platformView = collision.gameObject.GetComponent<PlatformView>();
            
            if (platformView)
            {
                return;
            }

            if (_ballModel.BallCanDestroyAllBlocks && collision.collider is BoxCollider2D)
            {
                SpawnBallCollisionEffect();
                var blockView = collision.gameObject.GetComponent<BaseBlockView>();

                if (blockView != null)
                {
                    SpawnBallCollisionEffect();
                    blockView.BlockHit(AppConfig.Instance.BallAndPlatform.BallDamage, blockView.BlockType != BlockTypes.Granite, true);
                    ballRigidbody.velocity = _prevMovementVector;
                }
            }
            else
            {
                CorrectBallMovement();

                var blockView = collision.gameObject.GetComponent<BaseBlockView>();

                if (blockView != null)
                {
                    SpawnBallCollisionEffect();
                    blockView.BlockHit(AppConfig.Instance.BallAndPlatform.BallDamage);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            _ballController.BallOutOfGameField();
            ballTrail.enabled = false;
        }
    }
}