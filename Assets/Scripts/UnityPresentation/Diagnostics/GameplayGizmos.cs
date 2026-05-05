using Application.World;
using UnityEngine;
using UnityPresentation.Common;

namespace UnityPresentation.Diagnostics
{
    public sealed class GameplayGizmos : MonoBehaviour
    {
        private WorldLayout _layout;
        private bool _hasLayout;

        public void SetLayout(WorldLayout layout)
        {
            _layout = layout;
            _hasLayout = true;
        }

        private void OnDrawGizmos()
        {
            if (!_hasLayout)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                UnityVectorMapper.ToVector3(_layout.WorldBounds.Center),
                new Vector3(_layout.WorldBounds.Size.X, _layout.WorldBounds.Size.Y, 1f));

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(
                UnityVectorMapper.ToVector3(_layout.SpawnBounds.Center),
                new Vector3(_layout.SpawnBounds.Size.X, _layout.SpawnBounds.Size.Y, 1f));

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(
                UnityVectorMapper.ToVector3(_layout.YardBounds.Center),
                new Vector3(_layout.YardBounds.Size.X, _layout.YardBounds.Size.Y, 1f));
        }
    }
}
