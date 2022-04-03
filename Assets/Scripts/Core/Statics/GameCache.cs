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
        private const string TotalEnergy = "totalEnergy";
        private const string NextEnergyTime = "nextEnergyTime";
        private const string EnergyLastAddedTime = "lastAddedTime";

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

        public static void SetEnergy(int energy) => PlayerPrefs.SetInt(TotalEnergy, energy);
        public static int GetEnergy() => PlayerPrefs.GetInt(TotalEnergy, AppConfig.Instance.EnergyConfig.MaxEnergy);
        public static void SetNextEnergyTime(DateTime date) => PlayerPrefs.SetString(NextEnergyTime, date.ToString(CultureInfo.CurrentCulture));
        public static DateTime GetNextEnergyTime() => StringToDate(PlayerPrefs.GetString(NextEnergyTime));
        public static void SetEnergyLastAddedTime(DateTime date) => PlayerPrefs.SetString(EnergyLastAddedTime, date.ToString(CultureInfo.CurrentCulture));
        public static DateTime GetEnergyLastAddedTime() => StringToDate(PlayerPrefs.GetString(EnergyLastAddedTime));
        
        private static DateTime StringToDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return DateTime.UtcNow;
            }
            
            return DateTime.Parse(date);
        }
    }
}