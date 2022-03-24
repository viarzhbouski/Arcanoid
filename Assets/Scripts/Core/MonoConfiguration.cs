using System;
using System.Collections.Generic;
using Scripts.Core.Interfaces;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.ObjectPooling;
using UnityEngine;

namespace Scripts.Core
{
    public class MonoConfiguration
    {
        private readonly List<IHasStart> _starts = new List<IHasStart>();
        private readonly List<IHasUpdate> _updates = new List<IHasUpdate>();
        private readonly List<IHasFixedUpdate> _fixedUpdates = new List<IHasFixedUpdate>();
        private readonly AppContext _appContext;
        
        public MonoConfiguration()
        {
            _appContext = new AppContext();
            _starts = new List<IHasStart>();
            _updates = new List<IHasUpdate>();
            _fixedUpdates = new List<IHasFixedUpdate>();
        }

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
            
            AppContext.Context.AddController(controller);
        }

        public void InitPools(List<PoolProvider> poolProviders)
        {
            var objectPools = new ObjectPools();
            foreach (var poolProvider in poolProviders)
            {
                poolProvider.Init();
                objectPools.PoolProviders[poolProvider.GetType()] = poolProvider;
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