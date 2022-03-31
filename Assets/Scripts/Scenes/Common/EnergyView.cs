using System;
using Core.Statics;
using TMPro;
using UnityEngine;

namespace Scenes.Common
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text energyText;
        [SerializeField]
        private TMP_Text energyMaxText;
        
        private float _currentTime;
        private float _seconds;
        private int _energy;

        private void Start()
        {
            _seconds = (float)TimeSpan.FromMinutes(AppConfig.Instance.EnergyConfig.Minutes).TotalSeconds;
            _currentTime = _seconds;
            energyMaxText.text = AppConfig.Instance.EnergyConfig.MaxEnergy.ToString();
            
     
            var lastSession = GameCache.GetLastSessionTime();
            var currentSession = DateTime.UtcNow;
            var minutes = (currentSession - lastSession).TotalMinutes;
            var energy = minutes > AppConfig.Instance.EnergyConfig.Minutes ? Math.Floor(minutes * AppConfig.Instance.EnergyConfig.EnergyPerPeriod) : 0;
            var currentEnergy = (int)energy + GameCache.GetCurrentEnergy();
            
            _energy = currentEnergy > AppConfig.Instance.EnergyConfig.MaxEnergy ? AppConfig.Instance.EnergyConfig.MaxEnergy : currentEnergy;
            energyText.text = _energy.ToString();
        }

        private void Update()
        {
            if (_currentTime <= 0)
            {
                _energy++;
                _currentTime = _seconds;
                energyText.text = _energy.ToString();
                GameCache.SetCurrentEnergy(_energy);
            }
            
            _currentTime -= Time.deltaTime;
        }

        private void OnApplicationQuit()
        {
            GameCache.SetLastSessionTime();
            GameCache.SetCurrentEnergy(_energy);
        }
    }
}