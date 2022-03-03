using UnityEngine;
using Scripts.Core.Interfaces;
using Scripts.Core;

namespace Scripts.Scenes.SceneGame.Controllers
{
    public class PlatformController : BaseController, IHasStart, IHasUpdate, IHasFixedUpdate
    {
        
        public void StartController()
        {
            Debug.Log("Start");
        }
        
        public void UpdateController()
        {
            Debug.Log("Update");
        }

        public void FixedUpdateController()
        {
            Debug.Log("FixedUpdate");
        }

        private void Move()
        {
            
        }
    }
}
