﻿using Common.Enums;
using DG.Tweening;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.ObjectPooling;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Helpers;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers.Views
{
    public class GenerateLevelView : MonoBehaviour, IView
    {
        [SerializeField]
        private Camera gameCamera;
        
        [SerializeField]
        private Transform mapPivot;
        
        [SerializeField]
        private RectTransform topPanel;
        
        [SerializeField]
        private RectTransform progressBar;
        
        private GenerateLevelModel _generateLevelModel;

        public void Bind(IModel model, IController controller)
        {
            _generateLevelModel = model as GenerateLevelModel;
            _generateLevelModel!.StartPosition = mapPivot.position;
            _generateLevelModel.TopPanelPosition = topPanel.transform.position;
            progressBar.DOSizeDelta(new Vector2(260, 50), 5f);
        }
        
        public void RenderChanges()
        {
            RenderLevelMap();
        }

        private void RenderLevelMap()
        {
            foreach (var block in _generateLevelModel.Blocks)
            {
                if (block.BlockType == BlockTypes.Empty)
                {
                    continue;
                }

                var objectPool = (BlockPoolManager)ObjectPools.Instance.PoolManagers[typeof(BlockPoolManager)];
                var blockMono = objectPool.GetObject();
                blockMono.SetBlockConfig(block);
                blockMono.transform.position = ResizeHelper.ResizePosition(block.Position, gameCamera);
                blockMono.transform.localScale = ResizeHelper.ResizeScale(_generateLevelModel.CellSize, gameCamera, blockMono.SpriteRenderer);
            }
        }
    }
}