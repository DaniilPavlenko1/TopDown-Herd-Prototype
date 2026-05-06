using System.Collections.Generic;
using Application.Common;

namespace Application.Gameplay
{
    public sealed class GameplayLifetime
    {
        private readonly List<IInitializable> _initializables = new();
        private readonly List<ITickable> _tickables = new();
        private readonly List<IDisposableService> _disposables = new();

        public void AddInitializable(IInitializable initializable)
        {
            if (initializable != null)
                _initializables.Add(initializable);
        }

        public void AddTickable(ITickable tickable)
        {
            if (tickable != null)
                _tickables.Add(tickable);
        }

        public void AddDisposable(IDisposableService disposable)
        {
            if (disposable != null)
                _disposables.Add(disposable);
        }

        public void Initialize()
        {
            foreach (IInitializable initializable in _initializables)
                initializable.Initialize();
        }

        public void Tick(float deltaTime)
        {
            foreach (ITickable tickable in _tickables)
                tickable.Tick(deltaTime);
        }

        public void Dispose()
        {
            for (int i = _disposables.Count - 1; i >= 0; i--)
                _disposables[i].Dispose();

            _disposables.Clear();
            _tickables.Clear();
            _initializables.Clear();
        }
    }
}
