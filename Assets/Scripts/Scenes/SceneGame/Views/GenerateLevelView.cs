using Common.Enums;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.ObjectPooling;
using Scripts.Scenes.SceneGame.Controllers.Models;
using Scripts.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        private TMP_Text levelNumber;
        
        [SerializeField]
        private Image packIcon;

        private GenerateLevelModel _generateLevelModel;

        public void Bind(IModel model, IController controller)
        {
            _generateLevelModel = model as GenerateLevelModel;
            _generateLevelModel!.StartPosition = mapPivot.position;
            _generateLevelModel.TopPanelPosition = topPanel.transform.position;
        }
        
        public void RenderChanges()
        {
            RenderLevelMap();
        }

        private void RenderLevelMap()
        {
            SetLevelUI();
            
            foreach (var block in _generateLevelModel.Blocks)
            {
                if (block.BlockType == BlockTypes.Empty)
                {
                    continue;
                }

                var objectPool = ObjectPools.Instance.GetObjectPool<BlockPoolManager>();
                var blockMono = objectPool.GetObject();
                blockMono.SetBlockConfig(block);
                blockMono.transform.position = ResizeHelper.ResizePosition(block.Position, gameCamera);
                blockMono.transform.localScale = ResizeHelper.ResizeScale(_generateLevelModel.CellSize, gameCamera, blockMono.SpriteRenderer);
            }
        }

        private void SetLevelUI()
        {
            levelNumber.text = _generateLevelModel.LevelNumber;
            packIcon.sprite = _generateLevelModel.PackIcon;
        }
    }
}