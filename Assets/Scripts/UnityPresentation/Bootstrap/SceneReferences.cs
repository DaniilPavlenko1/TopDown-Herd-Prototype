using UnityEngine;
using UnityPresentation.Diagnostics;
using UnityPresentation.UI;
using UnityPresentation.Views;
using UnityPresentation.World;

namespace UnityPresentation.Bootstrap
{
    public sealed class SceneReferences : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CameraView cameraView;

        [Header("World")]
        [SerializeField] private GroundView groundView;
        [SerializeField] private YardView yardView;
        [SerializeField] private SpawnAreaView spawnAreaView;

        [Header("Hero")]
        [SerializeField] private HeroView heroView;

        [Header("Animals")]
        [SerializeField] private AnimalView animalPrefab;
        [SerializeField] private Transform animalsContainer;

        [Header("UI")]
        [SerializeField] private ScoreView scoreView;

        [Header("Diagnostics")]
        [SerializeField] private GameplayGizmos gameplayGizmos;

        public Camera MainCamera => mainCamera;
        public CameraView CameraView => cameraView;
        public GroundView GroundView => groundView;
        public YardView YardView => yardView;
        public SpawnAreaView SpawnAreaView => spawnAreaView;
        public HeroView HeroView => heroView;
        public AnimalView AnimalPrefab => animalPrefab;
        public Transform AnimalsContainer => animalsContainer;
        public ScoreView ScoreView => scoreView;
        public GameplayGizmos GameplayGizmos => gameplayGizmos;
    }
}
