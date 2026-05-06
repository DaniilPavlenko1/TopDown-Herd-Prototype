using Domain.Hero;
using UnityPresentation.Views;

namespace UnityPresentation.Bindings
{
    public sealed class HeroViewBinder : IViewBinder
    {
        private readonly HeroModel _model;
        private readonly HeroView _view;

        public HeroViewBinder(HeroModel model, HeroView view)
        {
            _model = model;
            _view = view;

            _view.SetPosition(_model.Position);
        }

        public void Tick()
        {
            _view.SetPosition(_model.Position);
        }

        public void Dispose()
        {
        }
    }
}
