using Domain.Animals;
using UnityPresentation.Views;

namespace UnityPresentation.Bindings
{
    public sealed class AnimalViewBinder : IViewBinder
    {
        private readonly AnimalModel _model;
        private readonly AnimalView _view;

        public AnimalModel Model => _model;
        public AnimalView View => _view;

        public AnimalViewBinder(AnimalModel model, AnimalView view)
        {
            _model = model;
            _view = view;

            _view.Bind(_model);
            _view.SetActive(true);
        }

        public void Tick()
        {
            _view.Render();
        }

        public void Dispose()
        {
            _view.Unbind();
            _view.SetActive(false);
        }
    }
}
