using Core.Interfaces.MVC;
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
        private SpriteRenderer ballSpriteRenderer;
        [SerializeField]
        private ParticleSystem furyBallEffect;
        [SerializeField]
        private ParticleSystem ballEffect;
        [SerializeField]
        private FuryBallView furyBallView;

        private BallModel _ballModel;
        private BallController _ballController;
        private Vector2 _movementVectorBeforePause;
        private bool _isFuryBall;

        public bool IsCaptive { get; set; }
        
        public void Bind(IModel model, IController controller)
        {
            _ballModel = model as BallModel;
            _ballController = controller as BallController;
            _isFuryBall = false;
            furyBallEffect.gameObject.SetActive(false);
            ballSpriteRenderer.color = AppConfig.Instance.BallAndPlatform.BallColor;
        }

        public void RenderChanges()
        {
            BallAtGameField();
            ChangeBallSprite();

            if (AppPopups.Instance.HasActivePopups)
            {
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
                
                if (!_ballModel.IsStarted && !IsCaptive)
                {
                    if (!_isFuryBall && ballEffect.gameObject.activeSelf)
                    {
                        ballEffect.gameObject.SetActive(false);
                    }
                    
                    ballRigidbody.velocity = Vector2.zero;
                    UpdateBallPosition();
                }
                else
                {
                    if (!_isFuryBall && !ballEffect.gameObject.activeSelf)
                    {
                        ballEffect.gameObject.SetActive(true);
                    }
                    
                    PushBall();
                }
            }
        }

        private void ChangeBallSprite()
        {
            if (_ballModel.BallCanDestroyAllBlocks && !_isFuryBall)
            {
                ballSpriteRenderer.sprite = AppConfig.Instance.BallAndPlatform.FuryBallSprite;
                ballSpriteRenderer.color = AppConfig.Instance.BallAndPlatform.FuryBallColor;
                furyBallEffect.gameObject.SetActive(true);
                ballEffect.gameObject.SetActive(false);
                _isFuryBall = true;
                furyBallView.CanDestroyBlocks = true;
            }
            
            if (!_ballModel.BallCanDestroyAllBlocks && _isFuryBall)
            {
                ballSpriteRenderer.sprite = AppConfig.Instance.BallAndPlatform.BallSprite;
                ballSpriteRenderer.color = AppConfig.Instance.BallAndPlatform.BallColor;
                furyBallEffect.gameObject.SetActive(false);
                ballEffect.gameObject.SetActive(true);
                _isFuryBall = false;
                furyBallView.CanDestroyBlocks = false;
            }
        }
        
        private void UpdateBallPosition()
        {
            ballRigidbody.velocity = Vector2.zero;
            transform.position = _ballModel.BallPosition;
        }

        public void PushBall()
        {
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
            var ballCollisionEffectPoolManager = AppObjectPools.Instance.GetObjectPool<BallCollisionEffectPool>();;
            var ballCollisionEffectMono = ballCollisionEffectPoolManager.GetObject();
            ballCollisionEffectMono.transform.position = transform.position;
            ballCollisionEffectPoolManager.DestroyPoolObject(ballCollisionEffectMono);
        }

        private void CorrectBallMovement()
        {
            if (ballRigidbody.bodyType == RigidbodyType2D.Static)
            {
                return;
            }

            var ballDirection = ballRigidbody.velocity.normalized;
            var ballDirectionSignY =  Mathf.Sign(ballDirection.y);
            var directionVertical = ballDirectionSignY * Vector2.up;
            var currentAngleVertical = Vector2.Angle(directionVertical, ballRigidbody.velocity.normalized);
            
            var ballDirectionSignX =  Mathf.Sign(ballRigidbody.velocity.normalized.x);
            var directionHorizontal = ballDirectionSignX * Vector2.right;
            var currentAngleHorizontal = Vector2.Angle(directionHorizontal, ballRigidbody.velocity.normalized);
            var angle = Quaternion.Euler(0, 0, _ballModel.MinBounceAngle); 
            
            if (currentAngleVertical < _ballModel.MinBounceAngle)
            {
                ballRigidbody.velocity = angle * directionVertical * _ballModel.BallSpeed;
            }
            else if (currentAngleHorizontal < _ballModel.MinBounceAngle)
            {
                ballRigidbody.velocity = angle * directionHorizontal * _ballModel.BallSpeed;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var platformView = collision.gameObject.GetComponent<PlatformView>();
            
            if (platformView)
            {
                return;
            }

            CorrectBallMovement();

            var blockView = collision.gameObject.GetComponent<BaseBlockView>();

            if (blockView != null)
            {
                SpawnBallCollisionEffect();
                var blockIsDestroyed = blockView.BlockHit(AppConfig.Instance.BallAndPlatform.BallDamage);
                if (blockIsDestroyed)
                {
                    _ballModel.Speed += AppConfig.Instance.BallAndPlatform.BallSpeedEncrease;
                }
            }
        }

        private void BallAtGameField()
        {
            if (!_ballModel.IsStarted)
            {
                return;
            }

            if (TransformHelper.ObjectAtGamefield(transform.position))
            {
                return;
            }
            
            if (!IsCaptive)
            {
                _ballController.BallOutOfGameField();
            }
            else
            {
                _ballController.RemoveCaptiveBall(this.GetComponent<CaptiveBallView>());
            }
        }
    }
}