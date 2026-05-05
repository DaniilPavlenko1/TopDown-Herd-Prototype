using UnityEngine;
using UnityPresentation.Bootstrap;

namespace UnityPresentation.Diagnostics
{
    public static class SceneReferenceValidator
    {
        public static void Validate(SceneReferences references)
        {
            Debug.Assert(references != null, "SceneReferences is not assigned.");

            if (references == null)
                return;

            Debug.Assert(references.MainCamera != null, "Main Camera is not assigned.");
            Debug.Assert(references.CameraView != null, "CameraView is not assigned.");
            Debug.Assert(references.GroundView != null, "GroundView is not assigned.");
            Debug.Assert(references.YardView != null, "YardView is not assigned.");
            Debug.Assert(references.SpawnAreaView != null, "SpawnAreaView is not assigned.");
            Debug.Assert(references.HeroView != null, "HeroView is not assigned.");
            Debug.Assert(references.AnimalPrefab != null, "Animal prefab is not assigned.");
            Debug.Assert(references.AnimalsContainer != null, "Animals container is not assigned.");
            Debug.Assert(references.ScoreView != null, "ScoreView is not assigned.");
        }
    }
}
