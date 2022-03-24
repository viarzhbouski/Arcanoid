using UnityEngine;

namespace Core.Popup
{
    public class BasePopupView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform popupRectTransform;

        public RectTransform PopupRectTransform => popupRectTransform;
    }
}