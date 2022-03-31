using Core.Statics;
using DG.Tweening;
using UnityEngine;

namespace Core.Popup
{
    public abstract class BasePopupView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform popupRectTransform;

        public RectTransform PopupRectTransform => popupRectTransform;

        public abstract void Open();
        
        protected abstract void Close(bool destroyAfterClose = false);
        
        protected virtual void OpenAnim()
        {
            var popupObjectTransform = gameObject.transform;
            
            popupObjectTransform.localScale = Vector3.zero;
            popupObjectTransform.DOScale(Vector3.one, 0.25f);
        }

        protected virtual void CloseAnim(bool destroyAfterClose)
        {
            var popupObjectTransform = gameObject.transform;

            popupObjectTransform.DOScale(Vector3.zero, 0.25f).onComplete = () =>
            {
                if (destroyAfterClose)
                {
                    Destroy(gameObject);
                }
            };
        }

        private void OnDestroy()
        {
            AppPopups.Instance.ActivePopups--;
        }
    }
}