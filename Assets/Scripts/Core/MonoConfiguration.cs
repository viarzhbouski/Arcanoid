using System;
using System.Collections.Generic;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.ObjectPooling;

namespace Scripts.Core
{
    public class MonoConfiguration
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

        public void InitPools(List<PoolManager> poolManagers)
        {
            var objectPools = new ObjectPools();
            foreach (var poolManager in poolManagers)
            {
                poolManager.InitPool();
                objectPools.PoolManagers[poolManager.GetType()] = poolManager;
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