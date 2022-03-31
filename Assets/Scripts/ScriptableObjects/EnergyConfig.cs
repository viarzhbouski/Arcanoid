﻿using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New EnergyConfig", menuName = "Create Energy Config")]
    public class EnergyConfig : ScriptableObject
    {
        [SerializeField]
        private int minutes;
        [SerializeField]
        private int energyPerPeriod;
        [SerializeField]
        private int maxEnergy;
        
        public int Minutes => minutes;
        
        public int EnergyPerPeriod => energyPerPeriod;
        
        public int MaxEnergy => maxEnergy;
    }
}