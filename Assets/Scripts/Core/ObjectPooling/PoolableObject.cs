using Common.Enums;
using UnityEngine;

namespace Scripts.Core.ObjectPooling
{
    public class PoolableObject : MonoBehaviour
    {
        [SerializeField] 
        private ObjectType objectType;
        
        [SerializeField] 
        private int itemsCount;
        
        public int ItemsCount
        {
            get => itemsCount;
            set => itemsCount = value;
        }
        
        public ObjectType ObjectType
        {
            get => objectType;
            set => objectType = value;
        }
    }
}