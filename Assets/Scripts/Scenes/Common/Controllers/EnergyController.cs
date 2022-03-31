using System;
using Core.Interfaces;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.Common.Models;
using Scenes.Common.Views;
using UnityEngine;

namespace Scenes.Common.Controllers
{
    public class EnergyController : IController, IHasStart, IHasUpdate
    {
        private readonly EnergyModel _energyModel;
        private readonly EnergyView _energyView;
        private float _currentTime;
        private float _seconds;
        
        public EnergyController(IView view)
        {
            _energyModel = new EnergyModel();
            _energyView = view as EnergyView;
            _energyView!.Bind(_energyModel, this);
            _energyModel.OnChangeHandler(ControllerOnChange);
            _seconds = AppConfig.Instance.EnergyConfig.Minutes;
            _currentTime = (float)TimeSpan.FromMinutes(_seconds).TotalSeconds;
        }
            
        public void ControllerOnChange()
        {
            _energyView.RenderChanges();
        }

        public void StartController()
        {
            var lastSession = GameCache.GetLastSessionTime();
            var currentSession = DateTime.UtcNow;
            var minutes = (currentSession - lastSession).TotalMinutes;
            var energy = minutes > AppConfig.Instance.EnergyConfig.Minutes ? Math.Floor(minutes * AppConfig.Instance.EnergyConfig.EnergyPerPeriod) : 0;
            var currentEnergy = (int) energy + GameCache.GetCurrentEnergy();
            _energyModel.Energy = currentEnergy > AppConfig.Instance.EnergyConfig.MaxEnergy ? AppConfig.Instance.EnergyConfig.MaxEnergy : currentEnergy;
            _energyModel.OnChange?.Invoke();
        }

        public void UpdateController()
        {
            if (_currentTime <= 0)
            {
                _energyModel.Energy++;
                _currentTime = (float)TimeSpan.FromMinutes(_seconds).TotalSeconds;
                _energyModel.OnChange?.Invoke();
            }
            
            _currentTime -= Time.deltaTime;
        }
    }
}