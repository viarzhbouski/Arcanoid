using Common.Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New LocalizationConfig", menuName = "Create Localization Config")]
    public class LocalizationConfig : ScriptableObject
    {
        [SerializeField]
        private Sprite flag;
        
        [SerializeField]
        private LocaleLanguages localeLanguage;
        
        [SerializeField]
        private string localeFileName;

        public Sprite Flag => flag;
        
        public LocaleLanguages LocaleLanguage => localeLanguage;
        
        public string LocaleFileName => localeFileName;
    }
}