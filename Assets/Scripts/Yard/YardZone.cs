using System;
using Animals;
using UnityEngine;

namespace Yard
{
    [RequireComponent(typeof(Collider2D))]
    public class YardZone : MonoBehaviour
    {
        public event Action<AnimalController> AnimalEntered;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out AnimalController animal))
            {
                AnimalEntered?.Invoke(animal);
            }
        }
    }
}