using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.ScenePack.Views
{
    public class PackView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text packNameUI;
        
        [SerializeField]
        private Image packImageUI;
        
        [SerializeField]
        private Button packButtonUI;

        public Image PackImageUI => packImageUI;
        
        public TMP_Text PackNameUI => packNameUI;
        
        public Button PackButtonUI => packButtonUI;
    }
}