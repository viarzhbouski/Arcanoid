using System;
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
        
        public Sprite Image => image;
        public string Mame => name;
        public TextAsset[] Levels => levels;
    }
}