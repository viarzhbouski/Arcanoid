using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New PackConfig", menuName = "Create Pack Config")]
    public class PackConfig : ScriptableObject
    {
        [SerializeField]
        private Sprite image;

        [SerializeField]
        private List<TextAsset> levels;
        
        [SerializeField]
        private LocaleFields localeField;
        
        public Sprite Image => image;
        
        public List<TextAsset> Levels => levels;
        
        public LocaleFields LocaleField => localeField;
        
        private void OnEnable()
        {
            var packName = Enum.GetName(typeof(LocaleFields), localeField);
            levels = Resources.LoadAll<TextAsset>($"Packs/{packName}").ToList();;
        }
    }
}