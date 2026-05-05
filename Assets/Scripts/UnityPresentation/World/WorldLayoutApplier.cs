using Application.World;

namespace UnityPresentation.World
{
    public sealed class WorldLayoutApplier
    {
        private readonly CameraView _cameraView;
        private readonly GroundView _groundView;
        private readonly YardView _yardView;
        private readonly SpawnAreaView _spawnAreaView;

        public WorldLayoutApplier(
            CameraView cameraView,
            GroundView groundView,
            YardView yardView,
            SpawnAreaView spawnAreaView)
        {
            _cameraView = cameraView;
            _groundView = groundView;
            _yardView = yardView;
            _spawnAreaView = spawnAreaView;
        }

        public void Apply(WorldLayout layout)
        {
            _cameraView.Apply(layout);
            _groundView.Apply(layout);
            _yardView.Apply(layout);
            _spawnAreaView.Apply(layout);
        }
    }
}
