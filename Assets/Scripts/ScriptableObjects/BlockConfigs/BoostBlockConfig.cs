using System;
using System.Collections.Generic;
using Common.Enums;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using UnityEngine;

namespace ScriptableObjects.BlockConfigs
{
    [Serializable]
    public struct BoostColor
    {
        [SerializeField]
        private BoostTypes boostType;
        
        [SerializeField]
        private Color boostColor;

        public BoostTypes BoostType
        {
            get => boostType;
            set => boostType = value;
        }

        public Color Color => boostColor;
    }
    
    [CreateAssetMenu(fileName = "New BoostBlockConfig", menuName = "Create Boost Block Config")]
    public class BoostBlockConfig : BaseBlockConfig
    {
        [SerializeField]
        private List<BoostColor> boostColor;

        public List<BoostColor> BoostColor => boostColor;
    }
}