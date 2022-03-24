using UnityEngine;

namespace Core.Statics
{
    public class GameProgress
    {
        private const string LastPack = "lastPack";
        private const string LastLevel = "lastLevel";

        public static int GetLastPack() => PlayerPrefs.GetInt(LastPack, 0);
        
        public static int GetLastLevel() => PlayerPrefs.GetInt(LastLevel, 0);

        public static void SetLastPack(int pack) => PlayerPrefs.SetInt(LastPack, pack);

        public static void SetLastLevel(int level) => PlayerPrefs.SetInt(LastLevel, level);
    }
}