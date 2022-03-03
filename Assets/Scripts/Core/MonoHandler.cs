using System.Collections.Generic;
using Scripts.Core.Interfaces;

namespace Scripts.Core
{
    public class MonoHandler
    {
        public void Start(List<BaseController> controllers)
        {
            foreach (var item in controllers)
            {
                if (item is IHasStart)
                {
                    ((IHasStart)item).StartController();
                }
            }
        } 
        
        public void Update(List<BaseController> controllers)
        {
            foreach (var item in controllers)
            {
                if (item is IHasUpdate)
                {
                    ((IHasUpdate)item).UpdateController();
                }
            }
        } 
        
        public void FixedUpdate(List<BaseController> controllers)
        {
            foreach (var item in controllers)
            {
                if (item is IHasFixedUpdate)
                {
                    ((IHasFixedUpdate)item).FixedUpdateController();
                }
            }
        }
    }
}