using System;
using System.Collections.Generic;
using Common.Enums;
using UnityEngine;

namespace ScriptableObjects.BlockConfigs
{
    [Serializable]
    public struct BoostSprite
    {
        [SerializeField]
        private BoostTypes boostType;
        
        [SerializeField]
        private Sprite sprite;

        public BoostTypes BoostType
        {
            get => boostType;
            set => boostType = value;
        }

        public Sprite Sprite => sprite;
    }
    
    [CreateAssetMenu(fileName = "New BoostBlockConfig", menuName = "Create Boost Block Config")]
    public class BoostBlockConfig : BaseBlockConfig
    {
        [SerializeField]
        private List<BoostSprite> boostSprites;

        public List<BoostSprite> BoostSprites => boostSprites;
    }
}