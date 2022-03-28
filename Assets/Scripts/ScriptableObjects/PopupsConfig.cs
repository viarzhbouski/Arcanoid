using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New PopupsConfig", menuName = "Create Popups Config")]
    public class PopupsConfig : ScriptableObject
    {
        [Header("PAUSE POPUP")]
        [Space]
        [SerializeField]
        private float pausePopupDelayAfterContinue;
        
        [Header("WIN POPUP")]
        [Space]
        [SerializeField]
        private float winPopupProgressBarSpeed;
        [SerializeField]
        private float winPopupProgressBarDelay;
        [SerializeField]
        private float winPopupDelay;
        [SerializeField]
        private float winPopupButtonsScaleSpeed;

        public float PausePopupDelayAfterContinue => pausePopupDelayAfterContinue;
        
        public float WinPopupProgressBarSpeed => winPopupProgressBarSpeed;
        
        public float WinPopupProgressBarDelay => winPopupProgressBarDelay;
        
        public float WinPopupDelay => winPopupDelay;
        
        public float WnPopupButtonsScaleSpeed => winPopupButtonsScaleSpeed;
    }
}