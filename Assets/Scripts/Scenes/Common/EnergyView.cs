using System;
using System.Collections;
using Core.Statics;
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
        [SerializeField]
        private RectTransform logoTransform;

        public RectTransform LogoTransform => logoTransform;
        public int CurrentEnergy { get; set; }
        private DateTime _nextEnergyTime;
        private DateTime _lastAddedTime;
        private int _restoreDuration;
        private bool _restoring;

        private void Start()
        {
            _restoreDuration = (int)TimeSpan.FromMinutes(AppConfig.Instance.EnergyConfig.Minutes).TotalSeconds;
            _restoring = false;
            Load();
            StartCoroutine(Countdown());
        }

        public void AddEnergy(int energy = 1)
        {
            CurrentEnergy += energy;

            if (CurrentEnergy >= AppConfig.Instance.EnergyConfig.MaxEnergy)
            {
                _nextEnergyTime = AddDuration(DateTime.UtcNow, _restoreDuration);
            }

            UpdateTimer();
            UpdateEnergy();
            Save();
        }
        
        public void UseEnergy(int energy = 1)
        {
            if (CurrentEnergy == 0)
            {
                return;
            }

            CurrentEnergy -= energy;
            UpdateEnergy();
            if (CurrentEnergy >= AppConfig.Instance.EnergyConfig.MaxEnergy)
            {
                Save();
                return;
            }

            if (!_restoring)
            {
                if (CurrentEnergy + 1 == AppConfig.Instance.EnergyConfig.MaxEnergy)
                {
                    _nextEnergyTime = AddDuration(DateTime.UtcNow, _restoreDuration);
                }
                
                StartCoroutine(Countdown());
            }
        }

        private IEnumerator Countdown()
        {
            UpdateTimer();
            UpdateEnergy();
            
            while (CurrentEnergy < AppConfig.Instance.EnergyConfig.MaxEnergy)
            {
                var currentTime = DateTime.UtcNow;
                var counter = _nextEnergyTime;
                var isAdding = false;
                
                while (currentTime > counter)
                {
                    if (CurrentEnergy < AppConfig.Instance.EnergyConfig.MaxEnergy)
                    {
                        CurrentEnergy++;
                        isAdding = true;
                        var timeToAdd = _lastAddedTime > counter ? _lastAddedTime : counter;
                        counter = AddDuration(timeToAdd, _restoreDuration);
                    }
                    else
                    {
                        break;
                    }
                }

                if (isAdding)
                {
                    _lastAddedTime = DateTime.UtcNow;
                    _nextEnergyTime = counter;
                }

                UpdateTimer();
                UpdateEnergy();
                Save();
                yield return null;
            }
        }

        private void UpdateTimer()
        {
            if (CurrentEnergy >= AppConfig.Instance.EnergyConfig.MaxEnergy)
            {
                _nextEnergyTime = AddDuration(DateTime.UtcNow, _restoreDuration);
            }
            
            var timer = _nextEnergyTime - DateTime.UtcNow;
            var value = $"{timer.Minutes:D2}:{timer.Seconds:D2}";

            timerText.text = value;
        }

        private void UpdateEnergy()
        {
            var value = $"{CurrentEnergy}/{AppConfig.Instance.EnergyConfig.MaxEnergy}";
            energyValue.text = value;
        }

        private DateTime AddDuration(DateTime time, int duration)
        {
            return time.AddSeconds(duration);
        }

        private void Load()
        {
            CurrentEnergy = GameCache.GetEnergy();
            _nextEnergyTime = GameCache.GetNextEnergyTime();
            _lastAddedTime = GameCache.GetEnergyLastAddedTime();

            if (CurrentEnergy >= AppConfig.Instance.EnergyConfig.MaxEnergy)
            {
                _nextEnergyTime = AddDuration(DateTime.UtcNow, _restoreDuration);
            }
        }

        private void Save()
        {
            GameCache.SetEnergy(CurrentEnergy);
            GameCache.SetNextEnergyTime(_nextEnergyTime);
            GameCache.SetEnergyLastAddedTime(_lastAddedTime);
        }
    }
}