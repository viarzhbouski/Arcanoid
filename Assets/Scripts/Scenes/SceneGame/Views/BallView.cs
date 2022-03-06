using Scripts.Core.Interfaces.MVC;
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
    }
}