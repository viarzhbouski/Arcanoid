using Common.Enums;
using UnityEngine;

namespace Core.Statics
{
    public class GameCache
    {
        private const string CurrentLocalization = "currentLocalization";
        private const string LastPack = "lastPack";
        private const string LastLevel = "lastLevel";

        public static int GetLastPack() => PlayerPrefs.GetInt(LastPack, 0);
        
        public static int GetLastLevel() => PlayerPrefs.GetInt(LastLevel, 0);
        
        public static LocaleLanguages GetCurrentLocalization() => (LocaleLanguages)PlayerPrefs.GetInt(CurrentLocalization);

        public static void SetLastPack(int pack) => PlayerPrefs.SetInt(LastPack, pack);

        public static void SetLastLevel(int level) => PlayerPrefs.SetInt(LastLevel, level);
        
        public static void SetLocalization(LocaleLanguages localeLanguage) => PlayerPrefs.SetInt(CurrentLocalization, (int)localeLanguage);
    }
}