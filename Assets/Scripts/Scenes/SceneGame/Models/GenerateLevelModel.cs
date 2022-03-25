using System;
using Core.Interfaces.MVC;
using ScriptableObjects;
using UnityEngine;

namespace Scenes.SceneGame.Models
{
    public class GenerateLevelModel : IModel
    {
        public string LevelNumber { get; set; }
        
        public Sprite PackIcon { get; set; }
        
        public Vector2 CellSize { get; set; }
        
        public Vector2 TopPanelPosition { get; set; }
        
        public Vector2 StartPosition { get; set; }

        public Block[,] Blocks { get; set; }
        
        public Action DestroyBlockEvent { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}