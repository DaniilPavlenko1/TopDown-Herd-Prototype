using System.Collections.Generic;

namespace Application.Common
{
    public sealed class CompositeDisposableService : IDisposableService
    {
        private readonly List<IDisposableService> _disposables;

        public CompositeDisposableService(params IDisposableService[] disposables)
        {
            _disposables = new List<IDisposableService>(disposables);
        }

        public void Dispose()
        {
            for (int i = _disposables.Count - 1; i >= 0; i--)
                _disposables[i]?.Dispose();

            _disposables.Clear();
        }
    }
}
