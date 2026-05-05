using Application.World;
using UnityEngine;
using UnityPresentation.Common;

namespace UnityPresentation.World
{
    public sealed class SpawnAreaView : MonoBehaviour
    {
        private WorldLayout _layout;

        public void Apply(WorldLayout layout)
        {
            _layout = layout;

            transform.position = UnityVectorMapper.ToVector3(layout.SpawnBounds.Center);
            transform.localScale = new Vector3(
                layout.SpawnBounds.Size.X,
                layout.SpawnBounds.Size.Y,
                1f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(
                transform.position,
                transform.localScale);
        }
    }
}
