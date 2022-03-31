using System;
using System.Collections;
using Core.Statics;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Scenes.Common
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text energyValue;
        [SerializeField]
        private TMP_Text timerText;
        
        private int _seconds;

        private void Start()
        {
            _seconds = (int)TimeSpan.FromMinutes(AppConfig.Instance.EnergyConfig.Minutes).TotalSeconds;

            if (!DataRepository.IsStarted)
            {
                var lastSession = GameCache.GetLastSessionTime();
                var currentSession = DateTime.UtcNow;
                var minutes = (currentSession - lastSession).TotalMinutes;
                var energy = minutes > AppConfig.Instance.EnergyConfig.Minutes ? Math.Floor(minutes * AppConfig.Instance.EnergyConfig.EnergyPerPeriod) : 0;
                var currentEnergy = (int)energy + GameCache.GetCurrentEnergy();
                var maxEnergy = AppConfig.Instance.EnergyConfig.MaxEnergy;
                
                DataRepository.IsStarted = true;
                DataRepository.CurrentEnergy = currentEnergy < maxEnergy ? currentEnergy 
                                                                         : maxEnergy;
                DataRepository.CurrentTime = _seconds;
            }
            
            energyValue.text = $"{DataRepository.CurrentEnergy}/{AppConfig.Instance.EnergyConfig.MaxEnergy}";
            
            UpdateTimer();
            StartCoroutine(Countdown());
        }

        IEnumerator Countdown()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (DataRepository.CurrentEnergy < AppConfig.Instance.EnergyConfig.MaxEnergy)
                {
                    DataRepository.CurrentTime--;

                    UpdateTimer();
                    
                    if (DataRepository.CurrentTime == 0)
                    {
                        DataRepository.CurrentEnergy++;
                        DataRepository.CurrentTime = _seconds;
                        SetEnergy();
                    }
                }
            }
        }
        
        public void EncreaseEnergyForWin()
        {
            DataRepository.CurrentEnergy += AppConfig.Instance.EnergyConfig.CompleteLevelEnergy;
            SetEnergy();
        }

        public void SetEnergy()
        {
            energyValue.transform.DOKill();
            energyValue.transform.DOPunchScale(new Vector2(1.1f, 1.1f), 0.2f);
            energyValue.text = $"{DataRepository.CurrentEnergy}/{AppConfig.Instance.EnergyConfig.MaxEnergy}";
        }

        private void OnApplicationQuit()
        {
            GameCache.SetLastSessionTime();
            GameCache.SetCurrentEnergy(DataRepository.CurrentEnergy);
        }

        private void UpdateTimer()
        {
            var time = TimeSpan.FromSeconds(DataRepository.CurrentTime);
            timerText.text = time.ToString(@"mm\:ss");
        }
    }
}