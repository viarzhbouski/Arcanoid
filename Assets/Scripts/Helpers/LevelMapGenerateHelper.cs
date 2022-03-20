using System.IO;
using Common.Enums;
using Scripts.Core.Models;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

namespace Scripts.Helpers
{
    public class LevelMapGenerateHelper: EditorWindow
    {
        private BlockTypes[,] _blocks;
        private const string Path = "./Assets/Levels/";
        
        [SerializeField]
        private int width;
        [SerializeField]
        private int height;

        [MenuItem("Window/Map generator")]
        private static void Init()
        {
            ((LevelMapGenerateHelper)GetWindow(typeof(LevelMapGenerateHelper))).Show();
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

            var model = new LevelMap
            {
                Height = height,
                Width = width
            };
            
            // for (var i = 0; i < height; i++)
            // {
            //     for (var j = 0; j < width; j++)
            //     {
            //         model.Blocks.Add((int)_blocks[i, j]);
            //     }
            // }
            
            var files = Directory.GetFiles(Path);
            var json = JsonConvert.SerializeObject(model);
            File.WriteAllText($"{Path}level_{files.Length}.json", json);
        }
    }
}