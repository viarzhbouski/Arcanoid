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
                var energy = minutes > AppConfig.Instance.EnergyConfig.Minutes ? (int)Math.Floor(minutes * AppConfig.Instance.EnergyConfig.EnergyPerPeriod) : 0;
                var currentEnergy = GameCache.GetCurrentEnergy();
                if (currentEnergy < AppConfig.Instance.EnergyConfig.MaxEnergy)
                {
                    energy += currentEnergy;
                    currentEnergy = energy < AppConfig.Instance.EnergyConfig.MaxEnergy
                        ? energy
                        : AppConfig.Instance.EnergyConfig.MaxEnergy;
                }
                
                DataRepository.IsStarted = true;
                DataRepository.CurrentEnergy = currentEnergy;
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
        
        public void EncreaseEnergy()
        {
            DataRepository.CurrentEnergy++;
            if (DataRepository.CurrentEnergy >= AppConfig.Instance.EnergyConfig.MaxEnergy)
            {
                DataRepository.CurrentTime = _seconds;
                UpdateTimer();
            }
            
            SetEnergy();
        }

        public void SetEnergy()
        {
            energyValue.transform.DOKill();
            energyValue.transform.DOPunchScale(new Vector2(1.01f, 1.01f), 0.1f).onComplete += () =>
            {
                if (energyValue.transform.localScale != Vector3.one)
                {
                    energyValue.transform.DOKill();
                    energyValue.transform.DOScale(Vector2.one, 0.2f);
                }
            };
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