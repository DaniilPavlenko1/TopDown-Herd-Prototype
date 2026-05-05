using Domain.Common;
using UnityEngine;
using UnityPresentation.Common;

namespace UnityPresentation.Views
{
    public sealed class HeroView : MonoBehaviour, IEntityView
    {
        public GameVector2 Position =>
            UnityVectorMapper.ToGameVector2(transform.position);

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
