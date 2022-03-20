using MonoModels;
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

        public void Bind(IModel model, IController controller)
        {
            _ballModel = model as BallModel;
            _ballController = controller as BallController;
        }
        
        public void RenderChanges()
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
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            CorrectBallMovement();
            var blockMono = collision.collider.gameObject.GetComponent<BlockMono>();
            
            if (blockMono != null)
            {
                SpawnBallCollisionEffect();
                blockMono.Damage();
                
                if (!blockMono.CanDestroy)
                {
                    return;
                }

                var blockObjectPool = ObjectPools.Instance.GetObjectPool<BlockPoolManager>();
                blockObjectPool.DestroyObject(blockMono);
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
                var angleVector = Quaternion.Euler(new Vector2(_ballModel.MinBounceAngle, 0));
                var x = ballVector.x * Mathf.Cos(angleVector.x) - ballVector.y * Mathf.Sin(angleVector.x);
                var y = ballVector.y * Mathf.Cos(angleVector.x) + ballVector.x * Mathf.Sin(angleVector.x);
                var newBallVector = new Vector2(x, y);
                ballRigidbody.velocity = newBallVector;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            _ballController.BallOutOfGameField();
            ballTrail.enabled = false;
        }
    }
}