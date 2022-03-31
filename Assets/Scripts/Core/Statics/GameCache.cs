using System;
using System.Globalization;
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
        private const string LastSessionTime = "lastSessionTime";
        private const string CurrentEnergy = "currentEnergy";

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
        
        public static DateTime GetLastSessionTime()
        {
            var lastSession = PlayerPrefs.GetString(LastSessionTime);
            if (string.IsNullOrEmpty(lastSession))
            {
                return DateTime.UtcNow;
            }

            return DateTime.Parse(lastSession);
        }

        public static int GetCurrentEnergy() => PlayerPrefs.GetInt(CurrentEnergy);
        
        public static void SetLastSessionTime()
        {
            var currentSession = DateTime.UtcNow.ToString(CultureInfo.CurrentCulture);
            PlayerPrefs.SetString(LastSessionTime, currentSession);
        }

        public static void SetCurrentEnergy(int energy) => PlayerPrefs.SetInt(CurrentEnergy, energy);
    }
}