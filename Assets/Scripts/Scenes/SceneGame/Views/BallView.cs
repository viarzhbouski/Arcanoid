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
        private BallModel _ballModel;
        private BallController _ballController;
        

        public void Bind(IModel model, IController controller)
        {
            _ballModel = model as BallModel;
            _ballController = controller as BallController;
        }
        
        public void RenderChanges()
        {
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
            var blockMono = collision.collider.gameObject.GetComponent<BlockMono>();
            
            if (blockMono != null)
            {
                SpawnBallCollisionEffect();
                blockMono.Damage();
                if (!blockMono.CanDestroy)
                {
                    return;
                }
                
                var blockObjectPool = (BlockPoolManager)ObjectPools.Instance.PoolManagers[typeof(BlockPoolManager)];
                blockObjectPool.DestroyObject(blockMono);
            }
        }

        private void SpawnBallCollisionEffect()
        {
            var ballCollisionEffectPoolManager = (BallCollisionEffectPoolManager)ObjectPools.Instance.PoolManagers[typeof(BallCollisionEffectPoolManager)];
            var ballCollisionEffectMono = ballCollisionEffectPoolManager.GetObject();
            ballCollisionEffectMono.transform.position = transform.position;
            ballCollisionEffectPoolManager.DestroyObject(ballCollisionEffectMono);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            _ballController.BallOutOfGameField();
        }
    }
}