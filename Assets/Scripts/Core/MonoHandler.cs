using System.Collections.Generic;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;

namespace Scripts.Core
{
    public class MonoHandler
    {
        private List<IHasStart> _starts = new List<IHasStart>();
        private List<IHasUpdate> _updates = new List<IHasUpdate>();
        private List<IHasFixedUpdate> _fixedUpdates = new List<IHasFixedUpdate>();

        public void AddController(IController controller)
        {
            if (controller is IHasStart start)
            {
                _starts.Add(start);
            }
            
            if (controller is IHasUpdate update)
            {
                _updates.Add(update);
            }
            
            if (controller is IHasFixedUpdate fixedUpdate)
            {
                _fixedUpdates.Add(fixedUpdate);
            }
        }

        public void Start()
        {
            foreach (var start in _starts)
            {
                start.StartController();
            }
        }
        
        public void Update()
        {
            foreach (var update in _updates)
            {
                update.UpdateController();
            }
        }
        
        public void FixedUpdate()
        {
            foreach (var fixedUpdate in _fixedUpdates)
            {
                fixedUpdate.FixedUpdateController();
            }
        }
    }
}