using Domain.Animals;
using Domain.Common;
using UnityEngine;
using UnityPresentation.Common;

namespace UnityPresentation.Views
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class AnimalView : MonoBehaviour, IEntityView
    {
        public AnimalModel Model { get; private set; }

        public GameVector2 Position =>
            UnityVectorMapper.ToGameVector2(transform.position);

        public bool HasModel => Model != null;

        public void Bind(AnimalModel model)
        {
            Model = model;
            SetPosition(model.Position);
        }

        public void Unbind()
        {
            Model = null;
        }

        public void Render()
        {
            if (Model == null)
                return;

            SetPosition(Model.Position);
        }

        public void SetPosition(GameVector2 position)
        {
            transform.position = UnityVectorMapper.ToVector3(position);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
