using Common.Enums;
using Core.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Statics
{
    public class GameCache
    {
        private const string CurrentLocalization = "currentLocalization";
        private const string CurrentGameProgress = "currentGameProgress";

        public static GameProgress GetCurrentGameProgress()
        {
            var json = PlayerPrefs.GetString(CurrentGameProgress);
            
            if (string.IsNullOrEmpty(json))
            {
                return new GameProgress();
            }
            
            return JsonConvert.DeserializeObject<GameProgress>(json);
        }
        
        public static LocaleLanguages GetCurrentLocalization() => (LocaleLanguages)PlayerPrefs.GetInt(CurrentLocalization);

        public static void SetCurrentGameProgress(GameProgress currentGameProgress)
        {
            var json = JsonConvert.SerializeObject(currentGameProgress);
            PlayerPrefs.SetString(CurrentGameProgress, json);
        }
        
        public static void SetLocalization(LocaleLanguages localeLanguage) => PlayerPrefs.SetInt(CurrentLocalization, (int)localeLanguage);
    }
}