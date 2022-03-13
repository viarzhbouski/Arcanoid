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

        public void Bind(IModel model)
        {
            _ballModel = model as BallModel;
        }
        
        public void RenderChanges()
        {
            PushBall();
        }

        private void PushBall()
        {
            ballRigidbody.AddForce(Vector2.up * _ballModel.Speed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.name.Contains("block"))
            {
                var objectPool = (BlockPoolManager)ObjectPools.Instance.PoolManagers[typeof(BlockPoolManager)];
                var block = collision.collider.gameObject.GetComponent<BlockMono>();
                objectPool.DestroyObject(block);
            }
        }
    }
}