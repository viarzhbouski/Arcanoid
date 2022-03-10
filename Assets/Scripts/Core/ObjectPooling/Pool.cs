using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.ObjectPooling
{
    public class Pool
    {
        public Transform Transform { get; private set; }

        public Queue<GameObject> Objects;

        public Pool(Transform transform)
        {
            Transform = transform;
            Objects = new Queue<GameObject>();
        }
    }
}