using System;
using Common.Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public struct Pack
    {
        [SerializeField]
        private Sprite image;
        
        [SerializeField]
        private string name;
        
        [SerializeField]
        private TextAsset[] levels;
        
        [SerializeField]
        private LocaleFields localeField;
        
        public Sprite Image => image;
        public string Name => name;
        public TextAsset[] Levels => levels;
        
        public LocaleFields LocaleField => localeField;
    }
}