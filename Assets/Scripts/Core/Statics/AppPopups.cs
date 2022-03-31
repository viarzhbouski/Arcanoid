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
        
        public static AppPopups Instance;

        public int ActivePopups { get; set; }
        
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

            ActivePopups++;
            var popupObject = Instantiate(popup, canvas);
            popupObject.Open();

            return (T)popupObject;
        }
    }
}