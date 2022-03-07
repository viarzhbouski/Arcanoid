using System;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Services;
using UnityEngine;
using UnityEngine.Tilemaps;

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
            var a= MapConvertService.MapConvert();

            foreach (var u in a.Layers)
            {
                foreach (var x in u.Data)
                {
                    Debug.Log(x);
                }
            }
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
            if (collision.collider.name == "Tilemap")
            {
                
                //var tilemap = collision.collider.GetComponent<Tilemap>();
                //var pos = tilemap.WorldToCell(collision.rigidbody.position);
                //tilemap.SetTile(Vector3Int.zero, null);
            }
        }
    }
}