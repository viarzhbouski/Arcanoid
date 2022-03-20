using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MonoModels;
using UnityEngine;

namespace Managers
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField]
        private RectTransform canvas;

        [SerializeField]
        private List<BasePopupView> popups;
        
        public static PopupManager Instance;

        private GameObject _activePopup;
        
        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void ShowPopup<T>() where T : BasePopupView
        {
            if (_activePopup != null)
            {
                return;
            }
            
            var popup = popups.FirstOrDefault(e => e is T);
            if (popup != null)
            {
                var popupObject = Instantiate(popup, canvas);
                var popupObjectTransform = popupObject.transform;
                popupObjectTransform.localScale = Vector3.zero;
                popupObjectTransform.DOScale(Vector3.one, 0.25f);
                _activePopup = popupObject.gameObject;
            }
        }
    }
}