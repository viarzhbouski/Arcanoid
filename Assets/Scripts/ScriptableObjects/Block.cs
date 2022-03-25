using System;
using Common.Enums;
using Scenes.SceneGame.Views.PoolableViews.Blocks;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public struct Block
    {
        [SerializeField]
        private int healthPoints;
        [SerializeField]
        private BlockTypes blockType;
        [SerializeField]
        private Color[] colors;
        [SerializeField]
        private BaseBlockView blockPrefab;
        public BoostTypes? BoostType { get; set; }

        public int HealthPoints
        {
            get => healthPoints; 
            set => healthPoints = value;
        }
        
        public BlockTypes BlockType
        {
            get => blockType; 
            set => blockType = value;
        }
        
        public Color[] Colors 
        {
            get => colors; 
            set => colors = value;
        }
        
        public BaseBlockView BlockPrefab 
        {
            get => blockPrefab; 
            set => blockPrefab = value;
        }

        public Vector2 Position { get; set; }
    }
}