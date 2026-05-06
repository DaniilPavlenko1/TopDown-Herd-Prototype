using System.Collections.Generic;
using UnityEngine;
using UnityPresentation.Views;

namespace UnityPresentation.Pooling
{
    public sealed class AnimalViewPool
    {
        private readonly AnimalView _prefab;
        private readonly Transform _container;
        private readonly Queue<AnimalView> _pool = new();

        public AnimalViewPool(
            AnimalView prefab,
            Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public AnimalView Get()
        {
            AnimalView view;

            if (_pool.Count > 0)
            {
                view = _pool.Dequeue();
            }
            else
            {
                view = Object.Instantiate(_prefab, _container);
            }

            view.SetActive(true);
            return view;
        }

        public void Release(AnimalView view)
        {
            if (view == null)
                return;

            view.Unbind();
            view.SetActive(false);
            _pool.Enqueue(view);
        }

        public void Clear()
        {
            while (_pool.Count > 0)
            {
                AnimalView view = _pool.Dequeue();

                if (view != null)
                    Object.Destroy(view.gameObject);
            }
        }
    }
}
