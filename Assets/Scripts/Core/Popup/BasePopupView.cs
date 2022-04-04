using System;
using DG.Tweening;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Popup
{
    public abstract class BasePopupView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform popupRectTransform;
        
       // public UnityAction PopupOnClose { get; set; }
       
        public delegate void PopupClose(Type popupType);

        public event PopupClose PopupOnClose;
        
        public RectTransform PopupRectTransform => popupRectTransform;

        public abstract void Open();
        
        public abstract void Close(bool destroyAfterClose = false);
        
        protected void OpenAnim()
        {
            var popupObjectTransform = gameObject.transform;
            
            popupObjectTransform.localScale = Vector3.zero;
            popupObjectTransform.DOScale(Vector3.one, 0.25f);
        }

        protected void CloseAnim(bool destroyAfterClose)
        {
            var popupObjectTransform = gameObject.transform;

            popupObjectTransform.DOScale(Vector3.zero, 0.25f).onComplete = () =>
            {
                if (destroyAfterClose)
                {
                    Destroy(gameObject);
                }
                
                PopupOnClose?.Invoke(this.GetType());
            };
        }
    }
}