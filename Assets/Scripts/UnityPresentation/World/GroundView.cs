using Application.World;
using UnityEngine;
using UnityPresentation.Common;

namespace UnityPresentation.World
{
    public sealed class GroundView : MonoBehaviour
    {
        public void Apply(WorldLayout layout)
        {
            transform.position = UnityVectorMapper.ToVector3(layout.WorldBounds.Center);
            transform.localScale = new Vector3(
                layout.WorldBounds.Size.X,
                layout.WorldBounds.Size.Y,
                1f);
        }
    }
}
