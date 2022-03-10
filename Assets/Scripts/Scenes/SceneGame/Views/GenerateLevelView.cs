using Common.Enums;
using MonoModels;
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
        private Camera camera;
        
        [SerializeField]
        private BlockMono blockPrefab;
        
        [SerializeField]
        private Transform mapPivot;
        
        private GenerateLevelModel _generateLevelModel;

        public void Bind(IModel model)
        {
            _generateLevelModel = model as GenerateLevelModel;
            _generateLevelModel!.StartPosition = mapPivot.position;
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

                var blockMono = ObjectPooler.Instance.GetObject(ObjectType.Block);

                    //var blockMono = Instantiate(blockPrefab, block.Position, Quaternion.identity, mapPivot);
                
                blockMono.transform.position = ResizeHelper.ResizePosition(block.Position.x, block.Position.y, camera);
                blockMono.transform.localScale = ResizeHelper.ResizeScale(_generateLevelModel.CellSize.x,_generateLevelModel.CellSize.y, camera, blockMono.GetComponent<SpriteRenderer>());
            }
        }
    }
}