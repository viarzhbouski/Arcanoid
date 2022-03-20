using UnityEngine;

namespace MonoModels
{
    public class BasePopupView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform popupRectTransform;

        public RectTransform PopupRectTransform => popupRectTransform;
    }
}