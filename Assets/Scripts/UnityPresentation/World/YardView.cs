using Application.World;
using UnityEngine;
using UnityPresentation.Common;

namespace UnityPresentation.World
{
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class YardView : MonoBehaviour
    {
        private BoxCollider2D _collider;

        private void Awake()
        {
            EnsureCollider();
        }

        public void Apply(WorldLayout layout)
        {
            EnsureCollider();

            transform.position = UnityVectorMapper.ToVector3(layout.YardBounds.Center);

            transform.localScale = new Vector3(
                layout.YardBounds.Size.X,
                layout.YardBounds.Size.Y,
                1f);

            _collider.isTrigger = true;
            _collider.size = Vector2.one;
            _collider.offset = Vector2.zero;
        }
        
        private void EnsureCollider()
        {
            if (_collider == null)
                _collider = GetComponent<BoxCollider2D>();
        }
    }
}
