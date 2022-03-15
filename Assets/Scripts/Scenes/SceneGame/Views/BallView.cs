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
            Debug.Log(111);
            ballRigidbody.AddForce(Vector2.up * _ballModel.Speed);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var block = collision.collider.gameObject.GetComponent<BlockMono>();
            
            if (block != null)
            {
                var objectPool = (BlockPoolManager)ObjectPools.Instance.PoolManagers[typeof(BlockPoolManager)];
                objectPool.DestroyObject(block);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            _ballController.BallOutOfGameField();
        }
    }
}