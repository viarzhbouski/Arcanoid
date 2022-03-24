using System.Collections.Generic;
using System.Linq;
using Core.Popup;
using DG.Tweening;
using UnityEngine;

namespace Core.Statics
{
    public class AppPopups : MonoBehaviour
    {
        [SerializeField]
        private RectTransform canvas;

        [SerializeField]
        private List<BasePopupView> popups;
        
        public static AppPopups Instance;
        
        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public T ShowPopup<T>() where T : BasePopupView
        {
            var popup = popups.First(e => e is T);
            var popupObject = Instantiate(popup, canvas);
            var popupObjectTransform = popupObject.transform;
            
            popupObjectTransform.localScale = Vector3.zero;
            popupObjectTransform.DOScale(Vector3.one, 0.25f);
            
            return (T)popupObject;
        }
        
        public void ClosePopup<T>(T popup) where T : BasePopupView
        {
            popup.PopupRectTransform.DOScale(Vector3.zero, 0.25f).onComplete = () => { Destroy(popup.gameObject); };
        }
    }
}