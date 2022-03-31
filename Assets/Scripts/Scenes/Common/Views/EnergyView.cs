using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.Common.Models;
using TMPro;
using UnityEngine;

namespace Scenes.Common.Views
{
    public class EnergyView : MonoBehaviour, IView
    {
        [SerializeField]
        private TMP_Text energyText;
        [SerializeField]
        private TMP_Text energyMaxText;
        
        private EnergyModel _energyModel;
        
        public void Bind(IModel model, IController controller)
        {
            _energyModel = model as EnergyModel;
            energyMaxText.text = AppConfig.Instance.EnergyConfig.MaxEnergy.ToString();
        }

        public void RenderChanges()
        {
            energyText.text = _energyModel.Energy.ToString();
        }

        private void OnApplicationQuit()
        {
            GameCache.SetLastSessionTime();
            GameCache.SetCurrentEnergy(_energyModel.Energy);
        }
    }
}