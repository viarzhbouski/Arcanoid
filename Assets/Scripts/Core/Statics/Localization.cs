using System;
using System.Linq;
using Common.Enums;
using Core.Models;
using UnityEngine;
using Newtonsoft.Json;

namespace Core.Statics
{
    public static class Localization
    {
        private static Locale _locale;

        static Localization()
        {
            LoadLocalization();
        }

        private static void LoadLocalization()
        {
            var localesJson =  Resources.Load<TextAsset>("locales").text;
            _locale = JsonConvert.DeserializeObject<Locale>(localesJson);
        }
        
        public static string GetFieldText(LocaleFields localeField)
        {
            var currentLocalization = GameCache.GetCurrentLocalization();
            var localeContent = _locale.LocaleContents.First(e => e.LocaleLanguage == currentLocalization);
            var localeFieldContent = localeContent.LocaleFieldContents.First(e => e.LocaleField == localeField);

            return localeFieldContent.Text;
        }

        public static LocaleLanguages ToogleLocalization(LocaleLanguages prevLanguage)
        {
            var langs = Enum.GetValues(typeof(LocaleLanguages));

            for (var i = 0; i < langs.Length; i++)
            {
                var lang = (LocaleLanguages)langs.GetValue(i);
                if (lang == prevLanguage)
                {
                    var nextId = i + 1;
                    if (nextId < langs.Length)
                    {
                        return (LocaleLanguages)langs.GetValue(nextId);
                    }
                    
                    break;
                }
            }
            
            return (LocaleLanguages)langs.GetValue(0); 
        }
    }
}