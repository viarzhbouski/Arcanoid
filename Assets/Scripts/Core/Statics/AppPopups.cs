using System;
using System.Collections.Generic;
using System.Linq;
using Core.Popup;
using UnityEngine;

namespace Core.Statics
{
    public class AppPopups : MonoBehaviour
    {
        [SerializeField]
        private RectTransform canvas;

        [SerializeField]
        private List<BasePopupView> popups;

        private readonly Dictionary<Type, BasePopupView> _activePopups = new Dictionary<Type, BasePopupView>();
        public static AppPopups Instance;

        public bool HasActivePopups => _activePopups.Count > 0;
        
        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        
        public T OpenPopup<T>() where T : BasePopupView
        {
            var popup = popups.FirstOrDefault(e => e is T);
            if (popup == null)
            {
                throw new NullReferenceException($"Popup {typeof(T)} not found");
            }
            
            var popupObject = Instantiate(popup, canvas);
            _activePopups.Add(typeof(T), popupObject);

            popupObject.Open();
            popupObject.PopupOnClose += RemovePopupFromDict;
            return (T)popupObject;
        }

        private void RemovePopupFromDict(Type popupType)
        {
            _activePopups.Remove(popupType);
        }
        
        public void ClosePopup<T>(bool destroyAfterClose = false) where T : BasePopupView
        {
            if (!_activePopups.ContainsKey(typeof(T)))
            {
                throw new NullReferenceException($"Popup {typeof(T)} not found");
            }
            
            var popupObject = _activePopups[typeof(T)];
            popupObject.Close(destroyAfterClose);
            RemovePopupFromDict(typeof(T));
        }
    }
}