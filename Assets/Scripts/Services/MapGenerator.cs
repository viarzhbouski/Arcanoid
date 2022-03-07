using Common.Enums;
using UnityEditor;
using UnityEngine;

namespace Scripts.Services
{
    class HeroData:ScriptableObject
    {
        public string data;
    }
    public class MapGenerator: EditorWindow
    {
        private BlockTypes[,] _blocks;
        private int _prevWidth = -1;
        private int _prevHeight = -1;

        [SerializeField]
        private int width;
        [SerializeField]
        private int height;

        [MenuItem("Window/Map generator")]
        private static void Init()
        {
            ((MapGenerator)GetWindow(typeof(MapGenerator))).Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Grid Settings", EditorStyles.boldLabel);
            width = EditorGUILayout.IntField("Width", width);
            height = EditorGUILayout.IntField("Height", height);
            
            if (GUILayout.Button("Apply"))
            {
                _blocks = new BlockTypes[height, width];
            }

            if (_blocks == null)
            {
                return;
            }
            
            for (var i = 0; i < height; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                for (var j = 0; j < width; j++)
                {
                    _blocks[i, j] = (BlockTypes)EditorGUILayout.EnumPopup(_blocks[i, j]);
                }

                EditorGUILayout.EndHorizontal();
            }

            if (!GUILayout.Button("Generate Json Map"))
            {
                return;
            }

            var s = "";
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    s += (int)_blocks[i, j];
                }
            }

            
            var x = CreateInstance<HeroData>();
            x.data = s;
            AssetDatabase.CreateAsset(x, "Assets/Qwe.asset");
        }
    }
}