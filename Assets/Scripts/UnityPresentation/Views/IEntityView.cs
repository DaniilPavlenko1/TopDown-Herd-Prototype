using Domain.Common;

namespace UnityPresentation.Views
{
    public interface IEntityView
    {
        GameVector2 Position { get; }

        void SetPosition(GameVector2 position);
        void SetActive(bool isActive);
    }
}
