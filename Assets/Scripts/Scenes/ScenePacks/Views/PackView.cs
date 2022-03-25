using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.ScenePacks.Views
{
    public class PackView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text packNameUI;
        
        [SerializeField]
        private TMP_Text levelProgressUI;
        
        [SerializeField]
        private Image packImageUI;
        
        [SerializeField]
        private Button packButtonUI;

        public Image PackImageUI => packImageUI;
        
        public TMP_Text PackNameUI => packNameUI;
        
        public TMP_Text LevelProgressUI => levelProgressUI;
        
        public Button PackButtonUI => packButtonUI;
    }
}