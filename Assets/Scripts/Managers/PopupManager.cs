using UnityEngine;

namespace Managers
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField]
        private Transform canvas;
        
        [SerializeField]
        private GameObject popupPrefab;

        public static PopupManager Instance;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void AAA()
        {
            Debug.Log("assdadas");
        }
    }
}