using System.Collections.Generic;
using UnityEngine;

namespace Animals
{
    public class AnimalPool
    {
        private readonly AnimalController _prefab;
        private readonly Transform _container;
        private readonly Queue<AnimalController> _pool = new();

        public AnimalPool(AnimalController prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public AnimalController Get()
        {
            if (_pool.Count > 0)
            {
                AnimalController animal = _pool.Dequeue();
                animal.gameObject.SetActive(true);
                return animal;
            }

            return Object.Instantiate(_prefab, _container);
        }

        public void Release(AnimalController animal)
        {
            animal.gameObject.SetActive(false);
            _pool.Enqueue(animal);
        }
    }
}