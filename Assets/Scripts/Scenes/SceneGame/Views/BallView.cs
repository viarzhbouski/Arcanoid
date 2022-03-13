using Common.Enums;
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
            PushBall();
        }

        private void PushBall()
        {
            ballRigidbody.AddForce(Vector2.up * _ballModel.Speed);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            _ballController.BallOutOfGameField();
            // if (collision.collider.name.Contains("block"))
            // {
            //     var objectPool = (BlockPoolManager)ObjectPools.Instance.PoolManagers[typeof(BlockPoolManager)];
            //     var block = collision.collider.gameObject.GetComponent<BlockMono>();
            //     objectPool.DestroyObject(block);
            // }
        }
    }
}