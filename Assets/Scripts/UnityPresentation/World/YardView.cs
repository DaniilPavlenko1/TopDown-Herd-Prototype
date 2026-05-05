using Application.World;
using UnityEngine;
using UnityPresentation.Common;

namespace UnityPresentation.World
{
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class YardView : MonoBehaviour
    {
        private BoxCollider2D _collider;

        public void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
        }

        public void Apply(WorldLayout layout)
        {
            transform.position = UnityVectorMapper.ToVector3(layout.YardBounds.Center);

            transform.localScale = new Vector3(
                layout.YardBounds.Size.X,
                layout.YardBounds.Size.Y,
                1f);

            _collider.size = Vector2.one;
            _collider.offset = Vector2.zero;
        }
    }
}
