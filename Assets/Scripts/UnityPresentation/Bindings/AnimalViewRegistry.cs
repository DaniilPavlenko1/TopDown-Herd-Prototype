using System.Collections.Generic;
using Domain.Animals;
using UnityPresentation.Pooling;
using UnityPresentation.Views;

namespace UnityPresentation.Bindings
{
    public sealed class AnimalViewRegistry
    {
        private readonly AnimalViewPool _pool;
        private readonly Dictionary<AnimalModel, AnimalViewBinder> _binders = new();

        public IReadOnlyCollection<AnimalViewBinder> Binders => _binders.Values;

        public AnimalViewRegistry(AnimalViewPool pool)
        {
            _pool = pool;
        }

        public void Add(AnimalModel model)
        {
            if (_binders.ContainsKey(model))
                return;

            AnimalView view = _pool.Get();
            _binders.Add(model, new AnimalViewBinder(model, view));
        }

        public bool TryGetView(AnimalModel model, out AnimalView view)
        {
            if (_binders.TryGetValue(model, out AnimalViewBinder binder))
            {
                view = binder.View;
                return true;
            }

            view = null;
            return false;
        }

        public void Remove(AnimalModel model)
        {
            if (!_binders.TryGetValue(model, out AnimalViewBinder binder))
                return;

            AnimalView view = binder.View;

            binder.Dispose();
            _binders.Remove(model);

            _pool.Release(view);
        }

        public void Tick()
        {
            foreach (AnimalViewBinder binder in _binders.Values)
            {
                binder.Tick();
            }
        }

        public void Clear()
        {
            foreach (AnimalViewBinder binder in _binders.Values)
            {
                _pool.Release(binder.View);
            }

            _binders.Clear();
        }
    }
}
