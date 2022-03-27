using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New PopupsConfig", menuName = "Create Popups Config")]
    public class PopupsConfig : ScriptableObject
    {
        [SerializeField]
        private float pausePopupDelayAfterContinue;

        public float PausePopupDelayAfterContinue => pausePopupDelayAfterContinue;
    }
}