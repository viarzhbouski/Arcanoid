using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using UnityEngine;
using Newtonsoft.Json;

namespace Core.Statics
{
    public static class Localization
    {
        private static Dictionary<string, string> _locale;

        static Localization()
        {
            LoadLocalization();
        }

        private static void LoadLocalization()
        {
            var fileName = AppConfig.Instance.Localizations.FirstOrDefault(e => e.LocaleLanguage == GameCache.GetCurrentLocalization())?.LocaleFileName;
            
            if (!string.IsNullOrEmpty(fileName))
            {
                var localesJson = Resources.Load<TextAsset>($"Localization/{fileName}").text;
                _locale = JsonConvert.DeserializeObject<Dictionary<string, string>>(localesJson);
            }
        }
        
        public static string GetFieldText(string key)
        {
            if (!_locale.Any() || !_locale.ContainsKey(key))
            {
                return "LOCALE ERROR!";
            }
            
            return _locale[key];
        }

        public static LocaleLanguages ToogleLocalization(LocaleLanguages prevLanguage)
        {
            var langs = Enum.GetValues(typeof(LocaleLanguages));
            var newLocalizationLang = (LocaleLanguages)langs.GetValue(0);
            for (var i = 0; i < langs.Length; i++)
            {
                var lang = (LocaleLanguages)langs.GetValue(i);
                if (lang == prevLanguage)
                {
                    var nextId = i + 1;
                    if (nextId < langs.Length)
                    {
                        newLocalizationLang = (LocaleLanguages)langs.GetValue(nextId);
                        GameCache.SetLocalization(newLocalizationLang);
                        LoadLocalization();
                        return newLocalizationLang;
                    }
                    
                    break;
                }
            }
            
            GameCache.SetLocalization(newLocalizationLang);
            LoadLocalization();
            return newLocalizationLang; 
        }
    }
}